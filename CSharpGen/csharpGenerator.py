# Code generation API
from abstractGenerator import AbstractGenerator

class CSharpGenerator(AbstractGenerator):   
  
  def initCpts(self):
      res = "// Generation de \n"
      for i in self._bn.ids():
        list = str(self._bn.cpt(i).tolist())
        list.replace('[', '{', count = len(list))
        list.replace(']', '}', count = len(list))

        res += " "+CSharpGenerator.nameCpt(self._bn,i) +"= "+list+";\n"
        #res += " "+CSharpGenerator.nameCpt(self._bn,i) +"= "+str(self._bn.cpt(i).tolist())+";\n"
      return res
    
  def getLanguageVersion(self):
    return "CSharp .NET 3.5"
      
  def  creaPot(self, potentielName, potentielVariables,fillval):
    count = 1
    typeVariable = "double[" + ","*len(potentielVariables - 1) + "] "
    dim = "new double["
    loops = ""
    affectation = potentielName + "["
    for i in potentielVariables:
      dim += str(self._bn.variable(int(i)).domainSize()) + ","
      #dim = "array_fill(0,"+str(self._bn.variable(int(i)).domainSize())+","+dim

      loops += "\t"*(count-1) + "for ( int " + "i"*count + " = 0 ; i < " + str(self._bn.variable(int(i)).domainSize()) + " ; " + "i"*count + "++ )\n"
      affectation = "\t" + affectation + "i"*count + ","

    affectation[-1] = ']'
    dim[-1] = ']'
    dim += ";\n"

    return typeVariable + potentielName + " = " + dim + loops + affectation + "\n"
    #return ("Creation de potentiel {0} (vars={1},fill={2})\n".format(potentielName,potentielVariables,fillval))
  
  def addVarPot(self,evid, cliq, index, value):
    pass # ?
    #return("  Ajout de la variable "+str(evid)+" au potentiel "+str(cliq)+"\n")
          
  def addSoftEvPot(self,evid,nompot,index,value):
    return str(nompot)+'= evs["'+str(evid)+'"];\n'
    #return "Add soft evidence "+str(evid)+" in "+str(nompot)+"'index:"+str(index)+",value="+str(value)+")\n"
  
  def mulPotCpt(self,nompot, var,variables):
    R = len(variables)
    res = ""
    indexPot = ""
    indexCpt = ""
    cpt = CSharpGenerator.nameCpt(self._bn,int(var))

    for i in range(R):
      res += "  for($i"+str(i)+"=0;$i"+str(i)+"<"+str(self._bn.variable(variables[i]).domainSize())+";$i"+str(i)+"++)\n"
      res += "  "*(i+1)
      indexPot = "[$i"+str(i)+"]"+indexPot

    for i in self._bn.cpt(int(var)).var_names:
      id_var = self._bn.idFromName(i)
      indexCpt += "[$i"+str(variables.index(id_var))+"]"

    res += "  "+"$"+nompot+indexPot+" *= $"+str(cpt)+str(indexCpt)+";\n"
    return res
    #return("Multiplication du potentiel "+str(nompot)+" par la cpt de la variable "+str(var)+"\n")
  
  def mulPotPot(self, nompot1, nompot2, varPot1, varPot2):
    return("Multiplication du potentiel "+str(nompot1)+" par le potentiel "+str(nompot2)+"\n")
      
  def margi(self,  cliq1, seloncliq2,varPot1,varPot2):
    return("Marginalisation de "+str(cliq1)+ " par "+str(seloncliq2)+"\n")
      
  def norm(self, nompot, targ):
    return("Normalisation de "+str(targ)+" on "+str(nompot)+"\n")
  
  def fill(self, pot, num): 
    return("Fill "+str(num)+" de "+str(pot)+"\n")
  
  def genereHeader(self,stream,header,nameFunc):
    stream.write(header+"\n\n")
    
  def genereFooter(self,stream,evs,nameFunc):
    stream.write('\n')
    
  def getCommentLine(self):
    return "//"

  def getSample(self,stream,evs,nameFunc,defs):
    return "(On definit ici un exemple de code utilisant la fonction)"
    pass
    