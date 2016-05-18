# Code generation API
from abstractGenerator import AbstractGenerator

class CSharpGenerator(AbstractGenerator):   
  
  def initCpts(self):
      res = "private double sum;\n"
      for i in self._bn.ids():
        list = str(self._bn.cpt(i).tolist())
        list = list.replace('[', '{', len(list))
        list = list.replace(']', '}', len(list))

        cpt = self.getNbDimList(list)
        dim = self.setDim(cpt)

        res += "private double" + dim + " " + CSharpGenerator.nameCpt(self._bn,i) +"= "+list+";\n"

      res += self.genereFunctionGetProba()
      return res

  def getNbDimList(self,list):
        cpt = 0
        for j in list:
                if j=="{":
                    cpt+=1
                else:
                    break
        return cpt

  def setDim(self,cpt):
      res="["
      for i in range(1,cpt):
           res+=","
      res+="]"
      return res
    
  def getLanguageVersion(self):
    return "CSharp .NET 3.5"
      
  def  creaPot(self, potentielName, potentielVariables,fillval):
    count = 1
    typeVariable = "double[" + ","*(len(potentielVariables) - 1) + "] "
    dim = "new double["
    loops = ""
    affectation = potentielName + "["
    for i in potentielVariables:
      dim += str(self._bn.variable(int(i)).domainSize()) + ","

      loops += "\t"*(count-1) + "for ( int " + "i" + str(count) + " = 0 ; " + "i" + str(count) + " < " + str(self._bn.variable(int(i)).domainSize()) + " ; " + "i" + str(count) + "++ )\n"
      affectation = "\t" + affectation + "i" + str(count) + ","
      count+=1

    affectation = affectation[:-1]
    affectation += "] = " + str(fillval) + ";"
    dim = dim[:-1]
    dim += ']'
    dim += ";\n"

    return typeVariable + potentielName + " = " + dim + loops + affectation + "\n"
  
  def addVarPot(self,evid, cliq, index, value):
    pass
          
  def addSoftEvPot(self,evid,nompot,index,value):
    return "double[] " + str(nompot)+'= evs["'+str(evid)+'"];\n'
  
  def mulPotCpt(self,nompot, var,variables):
    R = len(variables)
    res = ""
    indexPot = ""
    indexCpt = ""
    cpt = CSharpGenerator.nameCpt(self._bn,int(var))

    for i in range(R):
      res += "\tfor(int i"+str(i)+" = 0 ; i"+str(i)+" < "+str(self._bn.variable(variables[i]).domainSize())+"; i"+str(i)+"++ )\n"
      res += "\t"*(i+1)
      indexPot = indexPot + "[i"+str(i)+"]"


    for i in self._bn.cpt(int(var)).var_names:
      id_var = self._bn.idFromName(i)
      indexCpt += "[i"+str(variables.index(id_var))+"]"

    res += "\t"+nompot+self.indexCSharp(indexPot)+" *= "+str(cpt)+str(self.indexCSharp(indexCpt))+";\n"
    return res

  def indexCSharp(self, index):
    indexCSharp = ""
    flag = False
    for i in range(len(index)-1):
      if flag:
        flag = False
        continue
      if index[i] == ']' and index[i+1] == '[':
        indexCSharp += ','
        flag = True
      else:
        indexCSharp += index[i]

    return indexCSharp + ']'

  def mulPotPot(self, nompot1, nompot2, varPot1, varPot2):
    R = len(varPot1)
    res = ""
    indexPot1 = ""
    indexPot2 = ""
    for i in range(R):
      res += "\tfor(int i"+str(i)+"=0;i"+str(i)+"<"+str(self._bn.variable(varPot1[i]).domainSize())+";i"+str(i)+"++)\n"
      res += "\t"*(i+1)
      indexPot1 += "[i"+str(i)+"]" #+ indexPot1

    for i in varPot2:
      indexPot2 += "[i"+str(varPot1.index(int(i)))+"]"#+indexPot2

    res += "\t"+nompot1+self.indexCSharp(indexPot1)+" *= "+nompot2+self.indexCSharp(indexPot2)+";\n"
    return res
      
  def margi(self,  cliq1, seloncliq2,varPot1,varPot2):
    res = ""
    R1 = len(varPot1)
    R2 = len(varPot2)
    indexPot1 = ""
    indexPot2 = ['*']*R2
    varPot3 = list(set(varPot2) - set(varPot1))
    R3 = len(varPot3)
    for i in range(R1):
      res += "\tfor(int i"+str(i)+"=0;i"+str(i)+"<"+str(self._bn.variable(varPot1[i]).domainSize())+";i"+str(i)+"++)\n"
      res += "\t"*(i+1)
      indexPot1 += "[i"+str(i)+"]"#+indexPot1
      indexPot2[R2-1-varPot2.index(int(varPot1[i]))] = "[i"+str(i)+"]"

    for j in range(R3):
      res += "\tfor(int j"+str(j)+"=0;j"+str(j)+"<"+str(self._bn.variable(varPot3[j]).domainSize())+";j"+str(j)+"++)\n"
      res += "\t"*(j+i+2)
      indexPot2[R2-1-varPot2.index(int(varPot3[j]))] = "[j"+str(j)+"]"
    indexPot2.reverse()
    indexPot2 = "".join(indexPot2)
    res += "\t"+cliq1+self.indexCSharp(indexPot1)+" += "+seloncliq2+self.indexCSharp(indexPot2)+";\n"
    return res
      
  def norm(self, nompot, targ):

    res = "\tsum=0.0;\n"
    res += "\tfor(int i0=0;i0<"+nompot+".Length"+";i0++)\n"
    res += "\t\tsum+="+nompot+"[i0];\n"
    res += "\tfor(int i0=0;i0<"+nompot+".Length"+";i0++)\n"
    res +=  "\t\t"+nompot+"[i0]/=sum;\n"
    res += "\tres.Add (\""+targ+"\""+","+nompot+");\n"
    return res
  
  def fill(self, pot, num):
    pass
  
  def genereHeader(self,stream,header,nameFunc):
    stream.write(header+"\n\n")
    stream.write("using System.Collections;"+"\n"+"using System.Collections.Generic;"+"\n\n")
    stream.write("public class " + nameFunc + " {"+"\n")

  def genereFunctionGetProba(self):
    return "public Dictionary<string, double[]> getProbasIA(Dictionary<string,double[]> evs){"+"\n" + "Dictionary<string, double[]> res = new Dictionary<string, double[]> ();\n"

  def genereFooter(self,stream,evs,nameFunc):
    stream.write("\n\treturn res;\n}\n}\n\n")
    
  def getCommentLine(self):
    return "//"

  def getSample(self,stream,evs,nameFunc,defs):
    return "(On definit ici un exemple de code utilisant la fonction)"
    pass
    