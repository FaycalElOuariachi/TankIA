from pylab import *
import matplotlib.pyplot as plt

import pyAgrum as gum
import pyAgrum.lib.notebook as gnb
from csharpGenerator import CSharpGenerator


# A partir d'un reseau fait a la main

learner=gum.BNLearner("WholeLog.csv")
print(learner.names())
#learner.useLocalSearchWithTabuList()
learner.useK2([0,1,2,3,4,5,6,7,8, 9, 10, 11, 12])

print("Timon")
bn = learner.learnBN()
learner.eraseForbiddenArc("move?", "")
#gnb.showBN(bn)

print("Pumba")

sequenceOfInstructions=[['CPO', 'TRXTBH', [2, 3], 1.0],
                        ['CPO', 'FCPPAV', [2, 6, 7], 1.0],
                        ['CPO', 'QUIBFX', [0, 1], 1.0],
                        ['CPO', 'HWIALF', [1, 2, 4], 1.0],
                        ['CPO', 'JDMAEU', [2, 5, 6], 1.0],
                        ['CPO', 'MVTHNR', [2, 4, 5], 1.0],
                        ['MUC', 'QUIBFX', '0', [0, 1]],
                        ['MUC', 'QUIBFX', '1', [0, 1]],
                        ['MUC', 'HWIALF', '2', [1, 2, 4]],
                        ['MUC', 'TRXTBH', '3', [2, 3]],
                        ['MUC', 'MVTHNR', '4', [2, 4, 5]],
                        ['MUC', 'JDMAEU', '5', [2, 5, 6]],
                        ['MUC', 'JDMAEU', '6', [2, 5, 6]],
                        ['MUC', 'FCPPAV', '7', [2, 6, 7]],
                        ['ASE', 'smoking?', 'EV_5', '0', "evs.get([5, 'smoking?'][1])"],
                        ['ASE', 'positive_XraY?', 'EV_3', '0', "evs.get([3, 'positive_XraY?'][1])"],
                        ['ASE', 'visit_to_Asia?', 'EV_0', '0', "evs.get([0, 'visit_to_Asia?'][1])"],
                        ['ASE', 'dyspnoea?', 'EV_7', '0', "evs.get([7, 'dyspnoea?'][1])"],
                        ['MUL', 'QUIBFX', 'EV_0', [0, 1], ['0']],
                        ['MUL', 'TRXTBH', 'EV_3', [2, 3], ['3']],
                        ['MUL', 'JDMAEU', 'EV_5', [2, 5, 6], ['5']],
                        ['MUL', 'FCPPAV', 'EV_7', [2, 6, 7], ['7']],
                        ['CPO', 'XAKXFL', [2, 5, 6], 1.0],
                        ['MUL', 'XAKXFL', 'JDMAEU', [2, 5, 6], [2, 5, 6]],
                        ['CPO', 'IFVUXZ', [2, 4, 5], 1.0],
                        ['MUL', 'IFVUXZ', 'MVTHNR', [2, 4, 5], [2, 4, 5]],
                        ['CPO', 'JZGJUB', [2, 4, 5], 1.0],
                        ['MUL', 'JZGJUB', 'MVTHNR', [2, 4, 5], [2, 4, 5]],
                        ['CPO', 'IPEKWS', [1, 2, 4], 1.0],
                        ['MUL', 'IPEKWS', 'HWIALF', [1, 2, 4], [1, 2, 4]],
                        ['CPO', 'ZLCECG', [2, 6], 0.0],
                        ['MAR', 'ZLCECG', 'FCPPAV', [2, 6], [2, 6, 7]],
                        ['MUL', 'JDMAEU', 'ZLCECG', [2, 5, 6], [2, 6]],
                        ['CPO', 'KNMSKT', [1], 0.0],
                        ['MAR', 'KNMSKT', 'QUIBFX', [1], [0, 1]],
                        ['MUL', 'HWIALF', 'KNMSKT', [1, 2, 4], [1]],
                        ['CPO', 'KIKCCO', [2, 4], 0.0],
                        ['MAR', 'KIKCCO', 'HWIALF', [2, 4], [1, 2, 4]],
                        ['MUL', 'MVTHNR', 'KIKCCO', [2, 4, 5], [2, 4]],
                        ['CPO', 'KVWKZF', [2, 5], 0.0],
                        ['MAR', 'KVWKZF', 'MVTHNR', [2, 5], [2, 4, 5]],
                        ['MUL', 'JDMAEU', 'KVWKZF', [2, 5, 6], [2, 5]],
                        ['CPO', 'ZXLINC', [2], 0.0],
                        ['MAR', 'ZXLINC', 'TRXTBH', [2], [2, 3]],
                        ['MUL', 'JDMAEU', 'ZXLINC', [2, 5, 6], [2]],
                        ['MUL', 'XAKXFL', 'ZLCECG', [2, 5, 6], [2, 6]],
                        ['MUL', 'XAKXFL', 'ZXLINC', [2, 5, 6], [2]],
                        ['CPO', 'NBUOFU', [2, 5], 0.0],
                        ['MAR', 'NBUOFU', 'XAKXFL', [2, 5], [2, 5, 6]],
                        ['MUL', 'IFVUXZ', 'NBUOFU', [2, 4, 5], [2, 5]],
                        ['CPO', 'ZMUCOI', [2, 4], 0.0],
                        ['MAR', 'ZMUCOI', 'IFVUXZ', [2, 4], [2, 4, 5]],
                        ['MUL', 'JZGJUB', 'KIKCCO', [2, 4, 5], [2, 4]],
                        ['MUL', 'JZGJUB', 'NBUOFU', [2, 4, 5], [2, 5]],
                        ['MUL', 'IPEKWS', 'ZMUCOI', [1, 2, 4], [2, 4]],
                        ['MUL', 'IPEKWS', 'KNMSKT', [1, 2, 4], [1]],
                        ['CPO', 'P_6', ['6'], 0.0],
                        ['MAR', 'P_6', 'JDMAEU', [6], [2, 5, 6]],
                        ['NOR', 'P_6', 'bronchitis?'],
                        ['CPO', 'P_4', ['4'], 0.0],
                        ['MAR', 'P_4', 'JZGJUB', [4], [2, 4, 5]],
                        ['NOR', 'P_4', 'lung_cancer?'],
                        ['CPO', 'P_1', ['1'], 0.0],
                        ['MAR', 'P_1', 'IPEKWS', [1], [1, 2, 4]],
                        ['NOR', 'P_1', 'tuberculosis?']]
generator = CSharpGenerator()
filename="ProbaMany.cs"

import pyAgrum as gum

generator.setBN(bn)
generator.setCommentMode(True)
generator.genere(['move?', 'turn?', 'shell?', "shield?"],
                 {'distance?', 'direction?', 'dist_coll_1?', 'dir_coll_1?', 'dist_coll_2?', 'dir_coll_2?',
                  'dist_coll_3?', 'dir_coll_3?', "dist_shell?"},
                 {},
                 sequenceOfInstructions,
                 filename,
                 "ProbaMany",
                 "# generation of CSharp")