# Code generation API
from abstractGenerator import AbstractGenerator

class DebugGenerator(AbstractGenerator):        
  def getLanguageVersion(self):
    return "DEBUG"
  
  def initCpts(self):
    return ("lecture des cpts du BN")
      
  def  creaPot(self, potentielName, potentielVariables,fillval):
    return ("Creation de potentiel {0} (vars={1},fill={2})\n".format(potentielName,potentielVariables,fillval))
  
  def addVarPot(self,evid, cliq, index, value):
    return("  Ajout de la variable "+str(evid)+" au potentiel "+str(cliq)+"\n")
          
  def addSoftEvPot(self,evid,nompot,index,value):
    return "Add soft evidence "+str(evid)+" in "+str(nompot)+"'index:"+str(index)+",value="+str(value)+")\n"
  
  def mulPotCpt(self,nompot, var,variables):
    return("Multiplication du potentiel "+str(nompot)+" par la cpt de la variable "+str(var)+"\n")
  
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
    return "#"

  def getSample(self,stream,evs,nameFunc,defs):
    pass
    