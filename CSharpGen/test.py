import pyAgrum as gum
import pyAgrum.lib.notebook as gnb
import os

import pydotplus as pydot

bn = gum.BayesNet('Water Sprinkler Bayesian Network')

c = bn.add(gum.LabelizedVariable('c', 'cloudy ?', 2))

d = bn.add(gum.LabelizedVariable('d', 'running ?', 2))

bn.addArc(c, d)

graph = pydot.Dot( bn, graph_type='digraph')

#gnb.showBN(bn)

print (bn)