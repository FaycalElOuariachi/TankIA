'''Abstract class for the generators'''
from config import Conf

class AbstractGenerator:
  @classmethod
  def nameCpt(self, bn, var):
    parents = []
    for i in bn.parents(var):
      parents.append(str(i))
    parents = "_".join(parents)
    return "P"+str(var)+"sachant"+parents
      
  def getLanguageVersion(self):
    raise NotImplementedError
      
  def initCpts(self):
    raise NotImplementedError
  
  def creaPot(self, potentielName, potentielVariables,fillval):
    raise NotImplementedError
  
  def addVarPot(self,evid, cliq, index, value):
    raise NotImplementedError
      
  def addSoftEvPot(self,evid,nompot,index,value):
    raise NotImplementedError
  
  def mulPotCpt(self,nompot, var,variables):
    raise NotImplementedError
  
  def mulPotPot(self, nompot1, nompot2, varPot1, varPot2):
    raise NotImplementedError
      
  def margi(self, cliq1, seloncliq2,varPot1,varPot2):
    raise NotImplementedError
      
  def norm(self, nompot, targ):
    raise NotImplementedError
  
  def fill(self, pot,num):
    raise NotImplementedError
    
  def genereHeader(self,stream,header,nameFunc):
    raise NotImplementedError
    
  def genereFooter(self,stream,evs,nameFunc):
    raise NotImplementedError
    
  def getCommentLine(self):
    raise NotImplementedError
    
  def getSample(self,stream,evs,nameFunc,defs):
    raise NotImplementedError
      
  def setBN(self,bn):
    self._bn=bn
    
  def setCommentMode(self,val):
    self._comment=val
        
  def genere(self, targets, evs, defs, comp, nameFile, nameFunc, header):
    stream = open(nameFile,'w')
    self.genereHeader(stream,header,nameFunc)
    stream.write(self.initCpts())
    for cur in comp:
      if self._comment:
        stream.write(self.getCommentLine()+" "+" ".join(map(str,cur))+"\n")
      act = cur[0]
      args= cur[1:]
      if act == Conf.CPO:
        stream.write(self.creaPot(*args))
      elif act == Conf.ASE:
        stream.write(self.addSoftEvPot(*args))
      elif act == Conf.MUC:
        stream.write(self.mulPotCpt(*args))
      elif act == Conf.MUL:
        stream.write(self.mulPotPot(*args))
      elif act == Conf.MAR:
        stream.write(self.margi(*args))
      elif act == Conf.NOR:
        stream.write(self.norm(*args))
      else:
        stream.write(self.getCommentLine()+act+'?')
    self.genereFooter(stream,evs,nameFunc)
    stream.write("\n"+self.getCommentLine()+"\n"+self.getCommentLine()+" Sample code using this function\n"+self.getCommentLine()+"\n")
    self.getSample(stream,evs,nameFunc,defs)
    stream.write("\n")
    stream.close()        