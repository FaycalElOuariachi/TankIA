# -*- coding: utf-8 -*-
"""
Created on Fri Mar 06 20:03:54 2015

@author: phw
"""

import yaml

def loadConfig(filename):
  print(filename)
  with open(filename, 'r') as f:
    doc = yaml.load(f)

  ev=[]
  de={}
  for item in doc['evidence']:
    if isinstance(item,str):
      ev.append(item)
    else:
      ev.append(list(item.keys())[0])
      de.update(item)
  
  doc['evidence']=ev
  doc['defaults_evs']=de
  print('\n=========================\nValues in file '+filename+'\n=========================')
  for k in doc:
    print('{0} : {1}'.format(k,doc[k] if k!='header' else '[...]'))
  print('=========================\n\n\n')

  return doc


class Conf:  
  __version__="1.0.0"

  PHI="Phi"
  PSI="Psi"
  CPO="CPO"
  ASE="ASE"
  FIL="FIL"
  MUC="MUC"
  MUL="MUL"
  MAR="MAR"
  NOR="NOR"
  
  template_yaml="""
# specification for inference
#----------------------------
bayesnet: {0}

#(@TODO) choose names of variables for evidence and target among
{1}
evidence:
  - NAME1 : [1,0 ] # values are just for the use test included in the generated file.
  - NAME2          # if no values : mgb will generete an 'empty' evidence.
  - ...

target:
  - NAME1
  - NAME2
  - ...

# specification for code generation
#----------------------------------
#(@TODO) choose language among
{2}
language:  

#(@TODO) choose true of false whether or not the potential names have to be obfuscated
obfuscation: true

#(@TODO) choose a filename (without suffixe) and a function name for the generated code
filename: gen{3}
function: getProbaFor{3}

header: |
  ################################################################################
  # @generationdate@ : @filename@ generated from @configfile@
  #
  # This file is generated by metaGenBayes @version@ for @language@
  #
  # Do not make changes to this file unless you know what you are doing
  # Please modify the configuration file (@configfile@) instead.
  ################################################################################
  """