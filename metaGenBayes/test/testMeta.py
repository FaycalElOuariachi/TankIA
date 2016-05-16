# -*- coding: utf-8 -*-
"""
Created on Wed Apr 22 14:00:38 2015

@author: Marvin
"""
"""FICHIER TEST"""
"""
import Compilator as compilator
from debugGenerator import debugGenerator
import pyAgrum as gum
import os
os.chdir("C:/Users/Marvin/Desktop/Informatique/Projet PIMA/testMetaBaysGen/Recent2")

bn = gum.loadBN("C:/Users/Marvin/Desktop/Informatique/Projet PIMA/testMetaBaysGen/BNs/Asia.bif")
target = ["bronchitis?","visit_to_Asia?"]
evs = {"smoking?":[1,0]}

comp = compilator.compil(bn, target, evs)
generator = debugGenerator()
generator.genere(comp, "test.py", "getValue")
"""


from Compiler import Compiler
from Generator.pyAgrumGenerator import pyAgrumGenerator
from Generator.numpyGenerator import numpyGenerator
from Generator.phpGenerator import phpGenerator
from Generator.javascriptGenerator import javascriptGenerator

import pyAgrum as gum
#import os
#os.chdir("C:/Users/Marvin/Desktop/Informatique/Projet PIMA/testMetaBaysGen/Recent2")

#Choose the right bn
#bn=gum.BayesNet()
#a,b,c,d,e=[bn.add(gum.LabelizedVariable(s,s,2)) for s in 'abcde']
#bn.addArc(a,b)
#bn.addArc(a,c)
#bn.addArc(b,d)
#bn.addArc(c,d)
#bn.addArc(e,c)
#bn.generateCPTs()

bn = gum.loadBN("/home/ubuntu/metaGenBayes/BNs/hailfinder.bif")
header = "######Testeur rapide#####"

#Choose the rights targets
#targets = ["d","a"] #Our own bn
#targets = ["bronchitis?","positive_XraY?"] #asia
#targets = ["HYPOVOLEMIA","CATECHOL"] #alarm1
#targets = ["ERRCAUTER","HR","HRBP","MINVOLSET","VENTMACH"] #alarm2
targets = ["Boundaries","SynForcng"] #hailfinder

#Choose the rigts evs
#evs = {"e":[1,0], "b":[0.25,0.75]} #Our own bn
#evs = {"smoking?":[0.5,0.5]} #asia
#evs = {"ANAPHYLAXIS":[0.4,0.6]} #alarm1
#evs = {"HREKG":[0.4,0.6,0.0]} #alarm2
#evs = {"INTUBATION":[1,0,0]}
evs = {"AMInstabMt":[0,1,0]} #hailfinder

print("** Version aGrUM **")
ie=gum.LazyPropagation(bn)
ie.setEvidence(evs)
ie.makeInference()
for t in targets:
    print(ie.posterior(bn.idFromName(t)))

print("** Génération pyAgrum **")
comp = Compiler.compil(bn, targets[:], evs)
generator = pyAgrumGenerator()
generator.genere(bn, targets, evs, comp, "generated.py", "getValue", header)
from generated import getValue
print(getValue(evs))

    
print("**Génération Python (numpy)**")
generator = numpyGenerator()
generator.genere(bn,targets,evs,comp,"generatedNumpy.py","getValue", header)
from generatedNumpy import getValue
print(getValue(evs))

print("** Génération PHP **")
generator = phpGenerator()
generator.genere(bn,targets,evs,comp,"generatedPHP.php","getValue", header)
import subprocess
proc = subprocess.Popen("php /home/ubuntu/metaGenBayes/generatedPHP.php", shell = True, stdout = subprocess.PIPE)
script_response = proc.stdout.read()
print(script_response)


print("** Génération Javascript **")
generator = javascriptGenerator()
generator.genere(bn,targets,evs,comp,"generatedJavascript.js","getValue", header)
proc = subprocess.Popen('nodejs /home/ubuntu/metaGenBayes/generatedJavascript.js', shell = True, stdout = subprocess.PIPE)
script_response = proc.stdout.read()
print(script_response)
