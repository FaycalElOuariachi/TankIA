from abstractGenerator import AbstractGenerator


def flatten(liste):
    '''Transforms multidimensional arrays into one'''
    for i in liste:
        if isinstance(i, list) or isinstance(i, tuple):
            for j in flatten(i):
                yield j
        else:
            yield i


def makeIndexOutOfDict(dictionary, potSize):
    '''From a dictionary of indexes, lenghts, structured in the margi function,
    to the string uses in the potential index of the same function'''
    arr = list()
    size = potSize
    ind=0
    while(ind < len(dictionary)):
        for i in dictionary:
            if(dictionary.get(i)[0] == ind):
                size /= dictionary.get(i)[1]
                arr.append(str(i)+'*'+str(size))
                ind += 1
    return "+".join(arr)

class JavascriptGenerator(AbstractGenerator):
  def getLanguageVersion(self):
    return "node.js>0.10"
      
  def initCpts(self):
    res = ""
    for i in self._bn.ids():
      res += "  "+JavascriptGenerator.nameCpt(self._bn,i) +"= new Float32Array("+str(list(flatten(self._bn.cpt(i).tolist())))+");\n"
    return res     
      
      
  def creaPot(self, potentielName,potentielVariables,fillval):
    dim = []
    res = ""
    for i in potentielVariables:
      dim += str(self._bn.variable(int(i)).domainSize())
    dim = "*".join(dim)
    res += "  "+potentielName+"= new Float32Array("+str(dim)+");\n"
    res += "  for(i=0;i<"+str(dim)+";i++)\n    "+potentielName+"[i] = "+str(fillval)+";\n"
    
    return res
  
  def addVarPot(self,evid, cliq, index, value):
    pass
      
  def addSoftEvPot(self,evid,nompot,index,value):
    return "  "+str(nompot)+"= new Float32Array(evs['"+str(evid)+"']);\n"        
      
  def mulPotCpt(self, nompot, var, variables):
    R = len(variables)
    res = ""
    indexPotList = list()
    indexCptList = list()
    indexPot = "["
    indexCpt = "["
    cpt = JavascriptGenerator.nameCpt(self._bn, int(var))
    sizePot = 1
    sizeCpt = 1
    for i in self._bn.cpt(int(var)).var_names:
      sizeCpt *= self._bn.variable(self._bn.idFromName(i)).domainSize()
    
    for i in range(R):
      res += "  for (i"+str(i)+"=0; i"+str(i)+"<"+str(self._bn.variable(variables[i]).domainSize())+";i"+str(i)+"++){\n"
      res += "  "*(i+1)
      indexPotList.append("i"+str(i)+"*"+str(sizePot))
      sizePot *= self._bn.variable(variables[i]).domainSize()
    indexPot += "+".join(indexPotList)+"]"

    for i in self._bn.cpt(int(var)).var_names:
      id_var = self._bn.idFromName(i)
      sizeCpt /= self._bn.variable(variables[variables.index(id_var)]).domainSize()
      indexCptList.append("i"+str(variables.index(id_var))+"*"+str(sizeCpt))
    indexCpt += "+".join(indexCptList)+"]"
    
    res += "  "+nompot+indexPot+" *= "+str(cpt)+str(indexCpt)+";"+"}"*(R)+"\n"
    return res
      
  def mulPotPot(self,nompot1,nompot2,varPot1,varPot2):
    R = len(varPot1)
    res = ""
    indexPot1List = list()
    indexPot2List = list()
    indexPot1 = "["
    indexPot2 = "["
    sizePot = 1
    for i in range(R):
      res += "  for (i"+str(i)+"=0; i"+str(i)+"<"+str(self._bn.variable(varPot1[i]).domainSize())+";i"+str(i)+"++){\n"
      res += "  "*(i+1)
      indexPot1List.append("i"+str(i)+"*"+str(sizePot))
      sizePot *= self._bn.variable(varPot1[i]).domainSize()
    indexPot1 += "+".join(indexPot1List)+"]"
    sizePot = 1

    for i in varPot2:
      indexPot2List.append("i"+str(varPot1.index(int(i)))+"*"+str(sizePot))
      sizePot *= self._bn.variable(varPot1[varPot1.index(int(i))]).domainSize()
    indexPot2 += "+".join(indexPot2List)+"]"
        
    res += "  "*(R-2)+nompot1+indexPot1+" *= "+nompot2+indexPot2+";"+"}"*(R)+"\n"
    return res
      
      
  def margi(self,cliq1, seloncliq2,varPot1,varPot2):
    res = ""
    R1 = len(varPot1)
    R2 = len(varPot2)
    indexPot1 = "["
    indexPot2 = "["
    indexPot1List = list()
    indexPot2Dict = {}
    sizePot1=1
    sizePot2=1
    varPot3 = list(set(varPot2) - set(varPot1))
    R3 = len(varPot3)
    
    for i in range(R1):
      res += "  for (i"+str(i)+"=0;i"+str(i)+"<"+str(self._bn.variable(varPot1[i]).domainSize())+";i"+str(i)+"++){\n"
      res += "  "*(i+1)
      indexPot1List.append("i"+str(i)+"*"+str(sizePot1))
      sizePot1 *= self._bn.variable(varPot1[i]).domainSize()
      indexPot2Dict['i'+str(i)] = [R2-1-varPot2.index(int(varPot1[i])), self._bn.variable(varPot2[varPot2.index(int(varPot1[i]))]).domainSize()]
      sizePot2 *= self._bn.variable(varPot2[varPot2.index(int(varPot1[i]))]).domainSize()
    indexPot1 += "+".join(indexPot1List)+"]"
    
    for j in range(R3):
      res += "  for (j"+str(j)+"=0;j"+str(j)+"<"+str(self._bn.variable(varPot3[j]).domainSize())+";j"+str(j)+"++){\n"
      res += "  "*(j+i+2)
      indexPot2Dict['j'+str(j)] = [R2-1-varPot2.index(int(varPot3[j])), self._bn.variable(varPot3[j]).domainSize()]
      sizePot2 *= self._bn.variable(varPot3[j]).domainSize()
        
    indexPot2 += makeIndexOutOfDict(indexPot2Dict, sizePot2)
    res += "  "+cliq1+indexPot1+" += "+seloncliq2+indexPot2+"];"+"}"*(R1+R3)+"\n"
    return res
    
  def norm(self, nompot, targ):
    res = "  sum = 0.0\n"        
    res += "  for (i0=0; i0 <"+nompot+".length;i0++){\n"
    res += "    sum +="+nompot+"[i0];}\n"
    res += "  for (i0=0; i0 <"+nompot+".length;i0++){\n"
    res += "    "+nompot+"[i0]/=sum;}\n"
    res += "  res['"+targ+"']="+str(nompot)+";\n"
    return res

  def genereHeader(self,stream,header,nameFunc):
    stream.write("/*\n"+header+"\n*/\n\n")
    stream.write("function "+nameFunc+"(evs) {\n")
    stream.write("  res=[];\n")
    
  def genereFooter(self,stream,evs,nameFunc):
    stream.write("  return res;\n}\n")
    
  def getSample(self,stream,evs,nameFunc,defs):
    evsjs = []
    for i in evs:
      ev = "  \""+str(i)+"\""+" : "
      if i in defs:
        ev+=str(defs[i])
      else:
        ev+='['+','.join(['1']*self._bn.variableFromName(i).domainSize())+']'
      evsjs.append(ev)
      
    stream.write("res="+nameFunc+"({\n"+",\n".join(evsjs)+"\n});\n")
    stream.write("""      
for (k in res) {
  st=""+k+" => ";
  for (i=0;i<res[k].length;i++) {
    st+=i+" : "+  res[k][i]+" | "
  }
  console.log(st)
}
    """)
  
  
  def getCommentLine(self):
    return "//"
            
