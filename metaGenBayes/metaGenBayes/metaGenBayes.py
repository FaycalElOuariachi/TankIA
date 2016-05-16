#! /usr/bin/env python3
# -*- coding: utf-8 -*-

import sys
import os
import datetime

import pyAgrum as gum

from compiler import compil
from config import Conf,loadConfig

languages = ["Debug", "pyAgrum", "numPy", "PHP", "javascript", "csharp"]
suffixes= {"debug":"dump", "pyagrum":"gum.py", "numpy":"np.py", "php":"php", "javascript":"js","csharp":"cs"} # keys lowered !

class MetaGenBayes:

   
  def __init__(self):
    pass

  def config(self,configFileName):
    configFileName=os.path.abspath(configFileName)
    
    self._request = loadConfig(configFileName)
    self._request['config']=configFileName
    self._request['templateHeader']=self._request['header']
    
    self._request['path']=os.path.dirname(configFileName)+"/"
    
    self._bn=gum.loadBN(self._request['path']+self._request['bayesnet'])
    
  def chgRequest(self,key,val):
    self._request[key]=val
    
  def obfuscate(self,ins):
    import string
    import random
    def id_generator(size=6, chars=string.ascii_uppercase):
      return ''.join(random.choice(chars) for _ in range(size))
    
    dico={}
    obf_names=set()
    for line in ins:    
      for i,arg in enumerate(line):
        if isinstance(arg, str):
          if (arg[0:3] in {"Phi","Psi"}):
            if arg in dico:
              line[i]=dico[arg]
            else:
              obf=id_generator()
              while obf in obf_names:
                obf=id_generator()
              dico[arg]=obf
              obf_names.add(obf)
              line[i]=obf
      
  def updateHeader(self):
    dico={
      'version':Conf.__version__,
      'filename':self._request['genFileName'],
      'generationdate':datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S"),
      'configfile':self._request['config'],
      'language':self._request['language']
    }

    s=self._request['templateHeader']  
    for k in dico:
      s=s.replace('@{0}@'.format(k),dico[k])
    self._request['header']=s
    
    
  def metaCompile(self):    
    ins=compil(self._bn, self._request['target'][:], set(self._request['evidence']))
    if self._request['obfuscation']:
      self.obfuscate(ins)
    return ins
    
  def generate(self,arrayOfInstructions):
    genFileName=self._request['filename']+"."+suffixes[self._request['language'].lower()]
    self._request['genFileName']=genFileName
    
    if(self._request['language'].lower() == languages[1].lower()):
        from pyAgrumGenerator import PyAgrumGenerator
        generator = PyAgrumGenerator()
    elif(self._request['language'].lower() == languages[2].lower()):
        from numpyGenerator import NumpyGenerator
        generator= NumpyGenerator()
    elif(self._request['language'].lower() == languages[3].lower()):
        from phpGenerator import PhpGenerator
        generator = PhpGenerator()
    elif(self._request['language'].lower() == languages[4].lower()):
        from javascriptGenerator import JavascriptGenerator
        generator = JavascriptGenerator()
    elif(self._request['language'].lower() == languages[5].lower()):
        from csharpGenerator import CSharpGenerator
        generator = CSharpGenerator()
    elif(self._request['language'].lower() == languages[0].lower()):
        from debugGenerator import DebugGenerator
        generator = DebugGenerator()
    else:
        print("The language you ask for isn't valid. Languages that have been implemented so far:\n"+str(languages))
        return
    
    self._request['language']=generator.getLanguageVersion()
    self.updateHeader()
    
    generator.setBN(self._bn)
    generator.setCommentMode(not self._request['obfuscation'])
    generator.genere(self._request['target'][:],
                     set(self._request['evidence']),
                     self._request['defaults_evs'],    
                     arrayOfInstructions, 
                     self._request['path']+genFileName, 
                     self._request['function'], 
                     self._request['header'])
    
    return genFileName
    
def signature(pref=''):  
  print(pref+"metaGenBayes {0} - (c) PHW -- 2015\n".format(Conf.__version__))
  
def about():
  print("metaGenBayes [configfile.yaml|BNfile]")
  print()
  sys.exit(0)

def generation(filename):
  mgb=MetaGenBayes()
  print("loading configuration in "+filename)
  mgb.config(filename)
  ins=mgb.metaCompile()
  genFileName=mgb.generate(ins)
  print("Compilation and generation done. File "+genFileName+' created.')
 
def remove_chars_re(subj, chars):
  import re
  return re.sub('[' + re.escape(chars) + ']', '', subj)
    
def documentor(BNfilename):
  bn=gum.loadBN(BNfilename)
  # from "/x/y/sample.1.bif" to "Sample1"
  radical=remove_chars_re(os.path.basename(BNfilename).rsplit(".",1)[0],'+.-#*').capitalize()
  
  maxPerLine=6
  n=0
  namesList="#(@TODO) "
  for node in bn.ids():
    namesList+=bn.variable(node).name()
    if n==maxPerLine:
      n=0
      namesList+="\n#(@TODO) "
    else:
      n+=1
      namesList+="\t"
      
  namesLanguage="#(@TODO) "+"\t".join(languages)
  
  signature('# Yaml config file automatically generated by ')
  print(Conf.template_yaml.format(BNfilename,namesList,namesLanguage,radical))
  
if __name__=='__main__':
  if (len(sys.argv)<2):
    signature()
    about()
  else:
    filename=sys.argv[1]
    if not os.path.exists(filename):
      signature()
      print("File '{}' not found.\n".format(filename))
      about()
    else:
      bname,ext=os.path.splitext(filename)
      if ext=='.yaml':
        signature()
        generation(filename)
      elif ext[1:] in gum.availableBNExts().split('|'):
        documentor(filename)
      else:
        signature()
        print("File '{}' is neither a yaml configuration file nor a recognized Bayesian Network file.\n".format(filename))
        about()