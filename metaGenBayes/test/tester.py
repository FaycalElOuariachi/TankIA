# -*- coding: utf-8 -*-

import sys
import os
sys.path.insert(0, os.path.abspath('..'))

import subprocess
import json
import ast

import pyAgrum as gum
import numpy as np

import random
random.seed()

from Compiler import Compiler
import Generator.numpyGenerator
import Generator.pyAgrumGenerator 
import Generator.phpGenerator
import Generator.javascriptGenerator




NUMBER_OF_NETWORKS = 15
NUMBER_OF_EVIDENCES_TESTED = 10
countNOBN = 0
countNOET = 0

resNumpyGen = {}
resPHPGen = {}
resPyAgrumGen = {}
resJavascriptGen = {}
error = True
header = "#####Fichier de test#####"

#print(targets)
#print(evs)
#print(resPyAgrumRef)

def importOrReload(module_name, *names):
    import sys

    if module_name in sys.modules:
        reload(sys.modules[module_name])
    else:
        __import__(module_name, fromlist=names)

    for name in names:
        globals()[name] = getattr(sys.modules[module_name], name)


def randomEvidenceGenerator(bn, prop):
    '''randomEvidenceGenerator create a dictionaries of soft evidences to be used in the testing of metaGenBayes functions'''
    evs = {}
    nodeToEvs = list(bn.ids())
    evsLength = random.randint(1, len(bn.ids())/prop)
    i = 0
    while (i < evsLength):
        randVar = random.choice(nodeToEvs)
        nodeToEvs.remove(randVar)
        evs[bn.variable(randVar).name()] = list(np.random.dirichlet(np.ones(bn.variable(randVar).domainSize()), size=1)[0])
        i=i+1    
    return evs

def targetSelecter(bn, evs):
    '''selects a few targets that are in your bn and not evidences.'''
    res = list()
    possibleTargets = list()
    for i in bn.ids():
        if(bn.variable(i).name()) not in evs:
            possibleTargets.append(i)
    tarLength = random.randint(1, len(possibleTargets)/2)
    cpt = 0
    while (cpt < tarLength):
        randVar = random.choice(possibleTargets)
        possibleTargets.remove(randVar)
        res.append(bn.variable(randVar).name())
        cpt = cpt + 1
    return res

def describeJunctionTree(bn, jt, target):
    '''String describing key elements of the junction tree like the components of every cliques, the edges, the main clique chosen by Compiler'''
    res = ""
    for i in jt.ids():
        res += str(i)+"---------\n"
        for j in jt.clique(i):
            res += bn.variable(j).name()+"\n"
    res += "\n"+str(list(jt.edges()))
    res += "\nClique racine : "+str(Compiler.mainClique(bn, jt, target))
    return res

def errorMarginChecker(generated, reference, targets, epsilon, option='default'):
    '''checks the deviation of thr generated results from a reference. If the deviation is > than epsilon, errorMarginChecker returns the boolean false'''
    for t in reference:
        i=0
        boolean = True
        if(option=='default'):
            while(i<bn.variable(bn.idFromName(t)).domainSize()):
                if(abs(reference.get(t)[i] - generated.get(t)[i])/reference.get(t)[i] >epsilon):
                    boolean=False
                    print("ERROR @ target "+t+", value "+str(i)+" is: "+str(generated.get(t)[i])+", should be "+str(reference.get(t)[i])+"\n")
                    break
                i=i+1
        elif(option=='PHP'):
             while(i<bn.variable(bn.idFromName(t)).domainSize()):
                if(abs(reference.get(t)[i] - generated.get(t)[0][i])/reference.get(t)[i] >epsilon):
                    boolean=False
                    print("ERROR @ target "+t+", value "+str(i)+" is: "+str(generated.get(t)[i])+", should be "+str(reference.get(t)[i])+"\n")
                    break
                i=i+1
        else:
            print("Wrong optional argument")
            return -1
    return boolean         

def errorWriter(language, filename):
     print("Error @ "+language+" version")
     target = open(filename, 'w')
     target.write('Bayesian network : '+str(bn.ids())+'\n')
     target.write('countNOET : '+str(countNOET)+'\ncountNOBN = '+str(countNOBN)+'\n')
     target.write('Evidences : '+str(evs)+'\nTargets : '+str(targets)+'\n')
     target.write('\n\nJunction Tree description : \n'+describeJunctionTree(bn, jt, targets))
     target.close()



while(countNOBN < NUMBER_OF_NETWORKS and error):
    #bn = gum.loadBN("/home/ubuntu/metaGenBayes/BNs/insurance.bif")
    bn = gum.BNGenerator().generate()
    bn.saveBIF('tester.bif')
    
    while(countNOET < NUMBER_OF_EVIDENCES_TESTED):
                
        evs = randomEvidenceGenerator(bn,2)
        targets = targetSelecter(bn, evs)
        comp = Compiler.compil(bn, targets[:], evs)
        
        '''pyAgrum reference version'''
        resPyAgrumRef= {}
        ie=gum.LazyPropagation(bn)
        ie.setEvidence(evs)
        ie.makeInference()
        jt = ie.junctionTree()
        for t in targets:
            resPyAgrumRef[t] = list(ie.posterior(bn.idFromName(t)))
            
        '''pyAgrum generated version'''
        resPyAgrumGen.clear()
        generator= Generator.pyAgrumGenerator.pyAgrumGenerator()
        generator.genere(bn, targets, evs, comp, "pyAgrumGenerated____test.py", "getValue", header)
        importOrReload('pyAgrumGenerated____test', 'getValue')
        resPyAgrumGen = getValue(evs)
        print('resPyAgrumRef : '+str(resPyAgrumRef))
        print('resPyAgrumGen : '+str(resPyAgrumGen))
        bool_pyAg = errorMarginChecker(resPyAgrumGen, resPyAgrumRef, targets, 0.1)
        if(not(bool_pyAg)):
            print("ERROR")
            errorWriter('pyAgrum', 'errorPyAgrum.txt')
            error = False
            break
        
        
        '''numpy generated version'''
        resNumpyGen.clear()
        generator = Generator.numpyGenerator.numpyGenerator()
        generator.genere(bn, targets, evs, comp, "numpyGenerated____test.py", "getValueNP", header)
        importOrReload('numpyGenerated____test', 'getValueNP')
        resNumpyGen = getValueNP(evs)
        print("resNumpyGen : "+str(resNumpyGen))
        bool_numpy = errorMarginChecker(resNumpyGen, resPyAgrumRef, targets, 0.1)
        if(not(bool_numpy)):
            print("ERROR")
            errorWriter('numpy', 'errorNumpy.txt')
            error = False
            break

        '''PHP generated version'''
        resPHPGen.clear()
        generator = Generator.phpGenerator.phpGenerator()
        generator.genere(bn, targets, evs, comp, "PHPGenerated____test.php", "getValuePHP", header)
        proc = subprocess.Popen("php PHPGenerated____test.php", shell = True, stdout = subprocess.PIPE)
        resPHPGen = ast.literal_eval(proc.stdout.read())
        print("resPHPGen :"+str(resPHPGen))
        bool_PHP = errorMarginChecker(resPHPGen, resPyAgrumRef, targets, 0.1, 'PHP')
        if(not(bool_PHP)):
            print("ERROR")
            errorWriter('php', 'errorPHP.txt')
            error = False
            break
      
  
        '''Javascript generated version'''
        resJavascriptGen.clear()
        generator = Generator.javascriptGenerator.javascriptGenerator()
        generator.genere(bn, targets, evs, comp, "javascriptGenerated____test.js", "getValueJS", header)
        proc = subprocess.Popen('nodejs javascriptGenerated____test.js', shell = True, stdout = subprocess.PIPE)
        print("resJsGen :"+str(proc.stdout.read()))
    

        countNOET = countNOET + 1
    
    countNOBN = countNOBN + 1

                
            
        

