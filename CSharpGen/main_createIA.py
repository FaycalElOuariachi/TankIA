from pylab import *
import matplotlib.pyplot as plt

import pyAgrum as gum
import pyAgrum.lib.notebook as gnb


# A partir d'un reseau fait a la main

learner=gum.BNLearner("test.csv")
print(learner.names())
learner.useLocalSearchWithTabuList()
#learner.useK2([0,1,2,3,4,5,6,7,8])

print("Timon")
bn = learner.learnBN()
#gnb.showBN(bn)

print("Pumba")