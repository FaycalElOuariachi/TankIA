from abstractGenerator import AbstractGenerator

class NumpyGenerator(AbstractGenerator):  
  def getLanguageVersion(self):
    return "numpy>1.10"
      
  def initCpts(self):
    res = ""
    for i in self._bn.ids():
        res += "  "+NumpyGenerator.nameCpt(self._bn,i) +"= "+str(self._bn.cpt(i).tolist())+"\n"
    return res
  
  def creaPot(self,potentielName, potentielVariables,fillval):
    dim = []      
    for i in potentielVariables:
      dim.insert(0,str(self._bn.variable(int(i)).domainSize()))
    dim = ",".join(dim)
    
    if(fillval==1.0):    
      return "  "+potentielName+"=np.ones(("+dim+"))\n"
        
    #fillval should be =0.0
    return "  "+potentielName+"=np.zeros(("+dim+"))\n"

  def addVarPot(self,evid, cliq, index, value):
    pass
  
  def addSoftEvPot(self,evid,nompot,index,value):
      return "  "+str(nompot)+"= evs.get('"+str(evid)+"')\n"
      
  def mulPotCpt(self, nompot, var, variables):
    R = len(variables)
    res = ""
    indexPot = ""
    indexCpt = ""
    cpt= NumpyGenerator.nameCpt(self._bn,int(var))
    
    for i in range(R):
      res += "  for i"+str(i)+" in range("+str(self._bn.variable(variables[i]).domainSize())+"): \n"
      res += "  "*(i+1)
      indexPot = "[i"+str(i)+"]"+indexPot

    for i in self._bn.cpt(int(var)).var_names:
      id_var = self._bn.idFromName(i)
      indexCpt += "[i"+str(variables.index(id_var))+"]"

    res += "  "+nompot+indexPot+" *= "+str(cpt)+str(indexCpt)+"\n"
    return res
           
  def mulPotPot(self,nompot1,nompot2,varPot1,varPot2):
    R = len(varPot1)
    res = ""
    indexPot1 = ""
    indexPot2 = ""
    for i in range(R):
      res += "  for i"+str(i)+" in range("+str(self._bn.variable(varPot1[i]).domainSize())+"):\n"
      res += "  "*(i+1)
      indexPot1 = "[i"+str(i)+"]"+indexPot1

    for i in varPot2:
      indexPot2 = "[i"+str(varPot1.index(int(i)))+"]"+indexPot2
        
    res += "  "+nompot1+indexPot1+" *= "+nompot2+indexPot2+"\n"
    return res
      
  def margi(self, cliq1,seloncliq2,varPot1,varPot2):
    res = ""
    R1 = len(varPot1)
    R2 = len(varPot2)
    indexPot1 = ""
    indexPot2 = ['*']*R2
    varPot3 = list(set(varPot2) - set(varPot1))
    R3 = len(varPot3)
    for i in range(R1):
      res += "  for i"+str(i)+" in range("+str(self._bn.variable(varPot1[i]).domainSize())+"):\n"
      res += "  "*(i+1)
      indexPot1 = "[i"+str(i)+"]"+indexPot1
      indexPot2[R2-1-varPot2.index(int(varPot1[i]))] = "[i"+str(i)+"]"
        
    for j in range(R3):
      res += "  for j"+str(j)+" in range("+str(self._bn.variable(varPot3[j]).domainSize())+"):\n"
      res += "  "*(j+i+2)
      indexPot2[R2-1-varPot2.index(int(varPot3[j]))] = "[j"+str(j)+"]"
    indexPot2 = "".join(indexPot2)
    res += "  "+cliq1+indexPot1+" += "+seloncliq2+indexPot2+"\n"
    return res
      
  def norm(self, nompot, targ):
    res = "  sum = 0.0\n"        
    res += "  for i0 in range(len("+nompot+")):\n"
    res += "    sum +="+nompot+"[i0]\n"
    res += "  for i0 in range(len("+nompot+")):\n"
    res += "    "+nompot+"[i0]/=sum\n"
    res += "  res['"+targ+"']=np.copy("+str(nompot)+"[:])\n"
    return res
  
  def fill(self,pot,num):
    pass
    
  def genereHeader(self,stream,header,nameFunc):
      stream.write(header+"\n")
      stream.write("\nimport numpy as np\n\n")        
      stream.write("def "+nameFunc+"(evs):\n")
      stream.write("  res = {}\n")
    
  def genereFooter(self,stream,evs,nameFunc):
      stream.write("  return res")

  def getSample(self,stream,evs,nameFunc,defs):
    stream.write("/*\n"+header+"\n*/\n\n")
    stream.write("function "+nameFunc+"(evs) {\n")
    stream.write("  res=[];\n")
    
  def genereFooter(self,stream,evs,nameFunc):
    stream.write("  return res\n")
    
  def getSample(self,stream,evs,nameFunc,defs):
    evsjs = []
    for i in evs:
      ev = "  \""+str(i)+"\""+" : "
      if i in defs:
        ev+=str(defs[i])
      else:
        ev+='['+','.join(['1']*self._bn.variableFromName(i).domainSize())+']'
      evsjs.append(ev)
      
    stream.write("res="+nameFunc+"({\n"+",\n".join(evsjs)+"\n});\n\nprint(res)")
    
  def getCommentLine(self):
    return "#"
    