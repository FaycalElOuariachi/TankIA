from pylab import *
import matplotlib.pyplot as plt

import pyAgrum as gum
import pyAgrum.lib.notebook as gnb


# A partir d'un réseau fait à la main
learner=gum.BNLearner("sample_asia.csv")
gnb.showBN(bn)
bn2=learner.learnParameters(bn)
gnb.showBN(bn2)