from abstractGenerator import AbstractGenerator

class PyAgrumGenerator(AbstractGenerator):           
  def getLanguageVersion(self):
    return "pyAgrum (python>=3.4, pyAgrum>=0.9.2)"
           
  def initCpts(self):
    res = ""
    for i in self._bn.ids():
      res += "  v"+str(i)+" = gum.LabelizedVariable('"+self._bn.variable(i).name()+"','"+self._bn.variable(i).name()+"',"+str(self._bn.variable(i).domainSize())+")\n"
      
    for i in self._bn.ids():
      nameCpt = PyAgrumGenerator.nameCpt(self._bn,i)
      res += "  "+nameCpt+" = gum.Potential()\n"
      res += "  "+nameCpt+".add(v"+str(i)+")\n"
      ls = self._bn.cpt(i).var_names
      for j in reversed(ls[0:len(ls)-1]):
        res += "  "+nameCpt+".add(v"+str(self._bn.idFromName(j))+")\n"
      res += "  "+nameCpt+"[:] = np.array("+str(self._bn.cpt(i).tolist())+")\n"
      
    return res
      
  def creaPot(self,potentielName, potentielVariables,fillval):
    res="  "+str(potentielName)+"=gum.Potential()\n"
    for var in potentielVariables:
      res+="  "+str(potentielName)+".add("+str(var)+")\n"
    res+="  "+str(potentielName)+".fill("+str(fillval)+")\n"
    
    return res
      
  def addVarPot(self,var,potentielName):
    pass
      
  def addSoftEvPot(self,evid,nompot,index,value):
    return("  "+str(nompot)+"[:]="+value+"\n")
      
  def mulPotCpt(self, nompot, var, variables):
    cpt = PyAgrumGenerator.nameCpt(self._bn,int(var))
    res = "  "+nompot+".multiplicateBy("+cpt+")\n"
    
    return res
           
  def mulPotPot(self,nompot1,nompot2,varPot1,varPot2):
    return("  "+str(nompot1)+".multiplicateBy("+str(nompot2)+")\n")
      
  def margi(self,cliq1, seloncliq2,varPot1,varPot2): 
    return("  "+str(cliq1)+".marginalize("+str(seloncliq2)+")\n")
      
  def norm(self, nompot, targ):
    return("  "+str(nompot)+".normalize()\n")
  
  def fill(self, pot, num):
    pass
  
    
  def genereHeader(self,stream,header,nameFunc):
    stream.write(header+"\n")
    stream.write("import pyAgrum as gum\nimport numpy as np\n\n")
    stream.write("def "+nameFunc+"(evs):\n")
    stream.write("  res = {}\n")
    
  def genereFooter(self,stream,evs,nameFunc):
    stream.write("  return res")

  def getSample(self,stream,evs,nameFunc,defs):  
    stream.write(self.getCommentLine()+"no sample here\n")
    
  def getCommentLine(self):
    return "# "
    