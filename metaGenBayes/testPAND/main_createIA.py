from pylab import *
import matplotlib.pyplot as plt

import pyAgrum as gum
import pyAgrum.lib.notebook as gnb
from metaGenBayes.csharpGenerator import CSharpGenerator

# A partir d'un reseau fait a la main

from metaGenBayes import phpGenerator

learner=gum.BNLearner("WholeLog.csv")
print(learner.names())
#learner.useLocalSearchWithTabuList()
learner.useK2([0,1,2,3,4,5,6,7,8, 9, 10, 11, 12])

bn = learner.learnBN()
gum.saveBN(bn, "Many.bif")

generator = CSharpGenerator()
#generator = phpGenerator.PhpGenerator()
filename="IA.cs"

import pyAgrum as gum
import metaGenBayes.compiler as Compiler


targets = ['move?', 'turn?', 'shell?', "shield?"]
un_sur_neuf = [1.0/9.0]*9
evs = {'distance?':[0.25, 0.25, 0.25, 0.25],
          "direction?":[1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0],
          'dist_coll_1?':[1.0/3.0, 1.0/3.0, 1.0/3.0],
          'dir_coll_1?':[1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0],
          'dist_coll_2?':[1.0/3.0, 1.0/3.0, 1.0/3.0],
          'dir_coll_2?':[1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0],
          'dist_coll_3?':[1.0/3.0, 1.0/3.0, 1.0/3.0],
          'dir_coll_3?':[1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0, 1.0/9.0],
          "dist_shell?":[0.5,0.5]}
#evs = {}

comp = Compiler.compil(bn, targets[:], evs)

generator.setBN(bn)
generator.setCommentMode(True)
generator.genere(bn,
                 targets,
                 evs,
                 comp,
                 filename,
                 "getProba",
                 "// generation of CSharp")