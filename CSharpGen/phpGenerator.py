from abstractGenerator import AbstractGenerator

class PhpGenerator(AbstractGenerator):
  def getLanguageVersion(self):
    return "PHP >=5.6"
        
  def initCpts(self):
      res = ""
      for i in self._bn.ids():
        res += "  $"+PhpGenerator.nameCpt(self._bn,i) +"= "+str(self._bn.cpt(i).tolist())+";\n"
      return res
    
  def creaPot(self, potentielName, potentielVariables,fillval): 
    dim = ""
    for i in potentielVariables:
      dim = "array_fill(0,"+str(self._bn.variable(int(i)).domainSize())+","+dim

    return "  $"+potentielName+"="+dim+str(fillval)+")"*len(potentielVariables)+";\n"
    
  def addVarPot(self,evid, cliq, index, value):      
    pass
    
  def addSoftEvPot(self,evid,nompot,index,value):
    return "  $"+str(nompot)+"= $evs['"+str(evid)+"'];\n"
        
  def mulPotCpt(self,nompot, var,variables):
    R = len(variables)
    res = ""
    indexPot = ""
    indexCpt = ""
    cpt = PhpGenerator.nameCpt(self._bn,int(var))
    
    for i in range(R):
      res += "  for($i"+str(i)+"=0;$i"+str(i)+"<"+str(self._bn.variable(variables[i]).domainSize())+";$i"+str(i)+"++)\n"
      res += "  "*(i+1)
      indexPot = "[$i"+str(i)+"]"+indexPot

    for i in self._bn.cpt(int(var)).var_names:
      id_var = self._bn.idFromName(i)
      indexCpt += "[$i"+str(variables.index(id_var))+"]"
  
    res += "  "+"$"+nompot+indexPot+" *= $"+str(cpt)+str(indexCpt)+";\n"
    return res
           
  def mulPotPot(self,nompot1,nompot2,varPot1,varPot2):
    R = len(varPot1)
    res = ""
    indexPot1 = ""
    indexPot2 = ""
    for i in range(R):
      res += "  for($i"+str(i)+"=0;$i"+str(i)+"<"+str(self._bn.variable(varPot1[i]).domainSize())+";$i"+str(i)+"++)\n"
      res += "  "*(i+1)
      indexPot1 = "[$i"+str(i)+"]"+indexPot1

    for i in varPot2:
      indexPot2 = "[$i"+str(varPot1.index(int(i)))+"]"+indexPot2
        
    res += "  "+"$"+nompot1+indexPot1+" *= $"+nompot2+indexPot2+";\n"
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
      res += "  for($i"+str(i)+"=0;$i"+str(i)+"<"+str(self._bn.variable(varPot1[i]).domainSize())+";$i"+str(i)+"++)\n"
      res += "  "*(i+1)
      indexPot1 = "[$i"+str(i)+"]"+indexPot1
      indexPot2[R2-1-varPot2.index(int(varPot1[i]))] = "[$i"+str(i)+"]"
      
    for j in range(R3):
      res += "  for($j"+str(j)+"=0;$j"+str(j)+"<"+str(self._bn.variable(varPot3[j]).domainSize())+";$j"+str(j)+"++)\n"
      res += "  "*(j+i+2)
      indexPot2[R2-1-varPot2.index(int(varPot3[j]))] = "[$j"+str(j)+"]"
    indexPot2 = "".join(indexPot2)
    res += "  $"+cliq1+indexPot1+" += $"+seloncliq2+indexPot2+";\n"
    return res
      
  def norm(self, nompot,targ):
    res = "  $sum=0.0;\n"      
    res += "  for($i0=0;$i0<count($"+nompot+");$i0++)\n"
    res += "    $sum+=$"+nompot+"[$i0];\n"
    res += "  for($i0=0;$i0<count($"+nompot+");$i0++)\n"
    res += "    $"+nompot+"[$i0]/=$sum;\n"
    res += "  $res['"+targ+"']=$"+nompot+";\n"
    return res
        
  def fill(self, pot, num):
    pass
  
          
  def equa(self, nompot1, nompot2):
    return "  $"+nompot1+" = $"+nompot2+";\n"
  
  def getCommentLine(self):
    return "//"
      
  def genereHeader(self,stream,header,nameFunc):
    stream.write("<?php\n")
    stream.write("/*\n"+header+"\n*/\n\n")
    stream.write("function "+nameFunc+"($evs) {\n")
    stream.write("  $res=[];\n")

  def genereFooter(self,stream,evs,nameFunc):
    stream.write("  return $res;\n}\n\n")
  
  def getSample(self,stream,evs,nameFunc,defs):  
    stream.write("foreach("+nameFunc+"(array(\n")
    lsEvs = []
    for name in evs:
        ev="  "*2+"\""+str(name)+"\" => "
        if name in defs:
          ev+=str(defs[name])
        else:
          ev+='['+','.join(['1']*self._bn.variableFromName(name).domainSize())+']'
        lsEvs.append(ev)
    stream.write(",\n".join(lsEvs))
    stream.write("\n))");
    stream.write(""" as $var=>$proba) {
  echo($var." => ");
  foreach ($proba as $v=>$p)
    echo("$v : $p | ");
  echo("\\n");
}""")
    
