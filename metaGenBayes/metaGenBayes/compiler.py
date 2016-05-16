import pyAgrum as gum
from config import Conf
  
class Compiler:
    ''' The **Compiler** will be an array of instructions that will later be use to generate codes in the different languages supported'''
    def __init__(self):  
        self.tab = []
        
    def createPotentialClique(self,cliq,varPot,val):
        '''The CPO instruction create the variable used for every potential mentioned in the code'''
        self.tab.append([Conf.CPO, cliq, varPot,val])
        
    def addVariablePotential(self,var,cliq):
        '''Instructions for the language to add tha *var* in the *cliq* potential. Note that all these arguments are given as strings in order to
        generate a code later'''
        self.tab.append(["ADV", var, cliq])      
    
    def addEvidencePotential(self,evid,cliq,index,value):
        '''Support for hard evidences is done.
        .. note:: 
        Evidences should be given as python dictionaries : {'a likelihood' : [its values], etc}. For an hard evidence, format supported
        is : {'a likelihood' : index} where index is the certain probability'''
        self.tab.append([Conf.ASE, evid, cliq, index, value])
    
    def fillPotential(self, cliq, value):
        '''Used to get all the values of a potential to 1 or 0, useful for the proper initializations in the different languages'''
        self.tab.append([Conf.FIL, cliq, value])

    def multiplicationCPT(self, cliq, cpt, varPot):
        '''MUC directive asks to compute a clique potential with all the probabilities of its variables'''
        self.tab.append([Conf.MUC, cliq, cpt, varPot])
        
    def multiplicationPotentials(self, cliq1, parcliq2, varPot1, varPot2):
        '''Instruction to compute two potentials : the variables of the potentials are passed as arguments for use in the Generators'''
        self.tab.append([Conf.MUL, cliq1, parcliq2, varPot1, varPot2])
        
    def marginalization(self, cliq1, seloncliq2,varPot1,varPot2):
        '''Marginalization of a clique, given another. Variables of used cliques are passed as parameters'''
        self.tab.append([Conf.MAR, cliq1, seloncliq2,varPot1,varPot2])
       
    def normalisation(self, cliq, targ):
        '''Instruction to normalize a clique, the last stage before the output'''
        self.tab.append([Conf.NOR, cliq, targ])
        
    def getTab(self):
        return self.tab

''' Meta code functions used to create the array of instructions: '''

def hardToSoftEvidences(bn, evs):
    '''Take the evidences input and convert the hard evidences in soft ones'''
    for i in evs:
        arr = []
        if(not(isinstance(evs.get(i), list))):
            print('turning '+str(i)+' into a soft evidence')
            print(bn.variable(bn.idFromName('n_6')).domainSize())
            arr = [0]*(bn.variable(bn.idFromName('n_6')).domainSize())
            arr[evs.get(i)] = 1.0
            evs[i] = arr

def labelPotential(jt,c):
    """Get the name of the potential for a clique c"""
    res=Conf.PHI
    for n in jt.clique(c):
        res += str(n)+"_"
    return res
    
def creationPotentialsAbsorp(bn, jt, absorp):
    """Fill the compiler array of instructions in order to create the potentials and add the corresponding variables"""
    for i in jt.ids():
        label = labelPotential(jt,i)+"c"+str(i)
        compilator.createPotentialClique(label,list(jt.clique(i)),1.0)
        #for j in jt.clique(i):
        #    compilator.addVariablePotential(str(j), label)
        #compilator.fillPotential(label,1)

def initPotentialsAbsorp(bn,jt, absorp):
    """Instructions for the initialization of the potentials"""    
    for i in bn.ids():
        cpt = 0
        for j in jt.ids():
            cpt += 1
            if(i in jt.clique(j)):
                b = 1
                for l in bn.parents(i):                  
                    if(not(l in jt.clique(j))):
                        b = 0
                        break
                if(b == 1):
                    compilator.multiplicationCPT(labelPotential(jt,j)+"c"+str(j),str(i),list(jt.clique(j)))
                    break
    
def creaIniOnePotDif(bn, jt, ca, cb, indexca, absorp):
    """Create a potential (for ca) for the diffusion"""
    labelAbs = labelPotential(jt,ca)
    labelDif = labelAbs+"to"+labelPotential(jt,cb)+"c"+str(ca)
    labelAbs += "c"+str(ca)
    varClique = list(jt.clique(ca))
    compilator.createPotentialClique(labelDif,varClique,1.0)
    #for j in varClique:
    #    compilator.addVariablePotential(str(j), labelDif)
    #compilator.fillPotential(labelDif,1)
    compilator.multiplicationPotentials(labelDif,labelAbs,varClique,varClique)

def creaIniOnePotTar(bn, jt, ca, absorp):
    """Create a potential which contains a target for the diffusion"""
    labelAbs = labelPotential(jt,ca)
    labelDif = labelAbs+"tar"
    labelAbs += "c"+str(ca)
    varClique = list(jt.clique(ca))
    compilator.createPotentialClique(labelDif, varClique,1.0)
    #for j in varClique:
    #    compilator.addVariablePotential(str(j), labelDif)
    #compilator.fillPotential(labelDif,1)
    compilator.multiplicationPotentials(labelDif,labelAbs,varClique,varClique)
    
def creaIniPotentialsDiffu(bn, jt, diffu, cliquesTar, targets, absorp):
    """Create all potentials for the diffusion, we must call this function before the absorption, and after the initialization of absorption's potentials"""
    R = len(diffu)
    for i in range(R):
        creaIniOnePotDif(bn, jt, diffu[i][0], diffu[i][1], i, absorp)
    for i in cliquesTar.values():
        creaIniOnePotTar(bn, jt, i, absorp)
        
def evsPotentials(bn, jt , evs, absorb):
    '''Instructions to create, fill and initialize the potentials of soft evidences''' 
    for i in evs:
        num = bn.idFromName(i)
        #compilator.createPotentialClique("EV_"+str(num),str(num),0)
        #compilator.addVariablePotential(str(num), "EV_"+str(num))
        compilator.addEvidencePotential(str(i), "EV_"+str(num), str(0), "evs.get("+str([num,i])+"[1])")
    for i in bn.ids():
        if(bn.variable(i).name() in evs):
            for j in jt.ids():
                if (i in jt.clique(j)):
                    b = 1
                    for l in bn.parents(i):
                        if(not(l in jt.clique(j))):
                            b = 0
                            break
                    if(b == 1):
                        varClique = list(jt.clique(j))
                        label = labelPotential(jt,j)+"c"+str(j)
                        compilator.multiplicationPotentials(label,"EV_"+str(i),varClique,[str(i)])
                        break
    
def neighbors(jt,c):
    """List of all the direct neighbors of a clique c in a junction tree jt"""
    ls = jt.edges()
    res = []
    for i in ls:
        if(i[0] == c):
            res.append(i[1])
        if(i[1] == c):
            res.append(i[0])
    return res

def nbneighbors(jt,c):
    return len(neighbors(jt,c))

def isTarget(bn,jt,target,n):
    '''Verifies if a clique n contains a target'''
    for i in target:
        if(bn.idFromName(i) in jt.clique(n)):
            return i
    return -1
    
def mainClique(bn,jt,target):
    """Gives the main clique of a junction where the information will be focused"""
    maxi = -1 #count how many neighbors the clique with the most neighbours and having at least a target has
    for i in jt.ids():
        for j in target:
            if(bn.idFromName(j) in jt.clique(i)):
                if(nbneighbors(jt,i) > maxi):
                    maxi = nbneighbors(jt,i)
                    res = i
    return res
    
def parcours(bn, jt, targetmp, n, r, absorp, diffu, cliquesTar):
    """Returns two lists for the absorption and the diffusion of the information in the junction tree"""
    ls = neighbors(jt,n)
    intersection = False
    if(r in ls):
        ls.remove(r)
    if(len(ls) == 0):
        return False
    for i in ls:
            tarnow = isTarget(bn,jt,targetmp,i)
            if(tarnow != -1):
                cliquesTar[tarnow] = i
                targetmp.remove(tarnow)
            tv = parcours(bn, jt,targetmp,i,n, absorp, diffu, cliquesTar)
            tar = tv or (tarnow != -1)
            absorp.append([i,n])
            if(tar):
                diffu.insert(0,[n,i])
                intersection = True
    return tar or intersection

def labelSeparator(jt, ca, cb): 
    """Returns the separator's label between ca and cb"""
    res=Conf.PSI
    for n in list(jt.clique(ca)):
        res += str(n)+"_"
    res+= "xx"
    for n in list(jt.clique(cb)):
        res += str(n)+"_"
    return res

def AinterB(la,lb):
    """Returns the intersection between lists la and lb"""
    res = []    
    for i in la:
        if(i in lb):
            res.append(i)
    return res
    
def sendMessAbsorp(bn, jt, ca, cb, indexca, absorp):
    """Updates the compiler array with inscrutions to send the message (absorption) from ca to cb"""
    np = labelSeparator(jt, ca, cb)+"c1_"+str(ca)+"c2_"+str(cb)
    varNp = AinterB(list(jt.clique(ca)),list(jt.clique(cb))) 
    compilator.createPotentialClique(np,varNp,0.0)
    #for i in varNp:
    #    compilator.addVariablePotential(str(i), np)
    compilator.marginalization(np, labelPotential(jt,ca)+"c"+str(ca),list(varNp),list(jt.clique(ca)))
    compilator.multiplicationPotentials(labelPotential(jt,cb)+"c"+str(cb), np,list(jt.clique(cb)),list(varNp))

def collectAroundCliq(bn, jt, ca, index, diffu):
    """Updates the compiler array to collect informations arround the cliq ca. index is the index of ca in diffu and diffutmp is the list of the first element of all elements of diffu until index"""
    neigh = neighbors(jt, ca)
    neigh.remove(diffu[index][1]) #we delete the destination
    if(index > 0 and diffu[index-1][1] == ca):
        neigh.remove(diffu[index-1][0]) #we delete the neighbor who has already given information
    for i in neigh:
        np = labelSeparator(jt, i, ca)+"c1_"+str(i)+"c2_"+str(ca)
        varNp = AinterB(list(jt.clique(i)),list(jt.clique(ca)))
        compilator.multiplicationPotentials(labelPotential(jt,ca)+"to"+labelPotential(jt,diffu[index][1])+"c"+str(ca),np, list(jt.clique(ca)), list(varNp))
            
def collectAroundCliqTar(bn, jt, ca, diffu):
    """Updates the compiler array to collect informations arround the cliq ca (which contains targets)"""
    neigh = neighbors(jt, ca)
    for i in neigh:
        np = labelSeparator(jt, i, ca)+"c1_"+str(i)+"c2_"+str(ca)
        varNp = AinterB(list(jt.clique(i)),list(jt.clique(ca)))
        compilator.multiplicationPotentials(labelPotential(jt,ca)+"tar",np, list(jt.clique(ca)), list(varNp))

def sendMessDiffu(bn, jt, ca, cb, index, diffu, cliquesTar):
    """Updates the compiler array with instructions to send the message (diffusion) from ca to cb"""
    collectAroundCliq(bn, jt, ca, index, diffu)
    np = labelSeparator(jt, ca, cb)+"c1_"+str(ca)+"c2_"+str(cb)
    varNp = AinterB(list(jt.clique(ca)),list(jt.clique(cb)))
    label = labelPotential(jt,ca)+"to"+labelPotential(jt,cb)+"c"+str(ca)
    compilator.createPotentialClique(np, varNp,0.0)
    #for i in varNp:
    #    compilator.addVariablePotential(str(i), np)
    compilator.marginalization( np, label, list(varNp), list(jt.clique(ca)))
    if(index < (len(diffu)-1) and diffu[index+1][0] == diffu[index][1]):
        compilator.multiplicationPotentials(labelPotential(jt,cb)+"to"+labelPotential(jt,diffu[index+1][1])+"c"+str(cb), np, list(jt.clique(cb)), list(varNp))

def deleteTarMainCliq(bn, jt, targetmp, rac):
    '''Remove, from the target list, the target in the main clique'''
    for i in jt.clique(rac):
        for j in targetmp:
            if(bn.idFromName(j) == i):
                targetmp.remove(j)
                
def inference(bn, jt, absorp, diffu, targets, targetmp, cliquesTar): 
    """Considering the targets of a bn, inference does the absorption and the diffusion of the information"""
    for i in range(len(absorp)):
        sendMessAbsorp(bn, jt, absorp[i][0], absorp[i][1], i, absorp)
    
    if(diffu):
        for i in range(len(diffu)):
            sendMessDiffu(bn, jt, diffu[i][0], diffu[i][1], i, diffu, cliquesTar)
        #We collect around cliques which contains targets
        for i in cliquesTar.values():
            collectAroundCliqTar(bn, jt, i, diffu)
    return diffu
    
def output(bn,jt,target, absorp, diffu, cliquesTar): 
    """Instructions for the last cliques to be normalized and return the results for respective targets"""    
    rac = mainClique(bn,jt,target)
    ls = list(target)
    #Targets who are still in the main clique
    for i in target:
        x = bn.idFromName(i)
        if(x in jt.clique(rac)):
            compilator.createPotentialClique("P_"+str(x),[str(x)],0.0)
            #compilator.addVariablePotential(str(x), "P_"+str(x))
            compilator.marginalization("P_"+str(x), labelPotential(jt,rac)+"c"+str(rac),[x],list(jt.clique(rac)))
            compilator.normalisation("P_"+str(x), bn.variable(x).name())
            ls.remove(i)
    #All the other targets
    for tar, cliq in cliquesTar.items():
        x = bn.idFromName(tar)
        compilator.createPotentialClique("P_"+str(x),[str(x)],0.0)
        #compilator.addVariablePotential(str(x), "P_"+str(x))
        compilator.marginalization("P_"+str(x), labelPotential(jt,cliq)+"tar",[x],list(jt.clique(cliq)))
        compilator.normalisation("P_"+str(x), bn.variable(x).name())
                
    
compilator=Compiler()
    
def compil(bn, targets, evs):
    """This function uses all the predefined functions above to fill the compiler array with instructions to get the targets of a bn according to evidences"""
    ie=gum.LazyPropagation(bn)
    jt = ie.junctionTree()
    #hardToSoftEvidences(bn, evs)
    absorp = [] #the list for the absorption
    diffu = [] #the list for the diffusion
    cliquesTar = {} #the dictionnary which contains the couples {target:clique}
    r = mainClique(bn, jt, targets)
    n = r
    targetmp1 = list(targets) #for parcours (this list is changed)
    deleteTarMainCliq(bn, jt, targetmp1, r)
    targetmp2 = list(targetmp1) #for inference (this list is not changed)
    parcours(bn, jt, targetmp1, n, r, absorp, diffu, cliquesTar)
    #Creation and initialization of potentials
    creationPotentialsAbsorp(bn, jt, absorp)
    initPotentialsAbsorp(bn, jt, absorp)
    evsPotentials(bn, jt, evs, absorp)
    creaIniPotentialsDiffu(bn, jt, diffu, cliquesTar, targets, absorp)
    
    #Absorption and diffusion
    inference(bn, jt, absorp, diffu, targets, targetmp2, cliquesTar)
    #Computing targets
    output(bn, jt, targets, absorp, diffu, cliquesTar)
    return compilator.getTab()
    
    