# -*- coding: utf-8 -*-
"""
Created on Wed Oct 29 13:26:32 2014

@author: phw
"""
import pyAgrum as gum
from spyder import showBN,showJT

def DgivenE1(bn,a,b,c,d,e):
    # clique potential
    Pbcd=gum.Potential()

    Pbcd.add(bn.variable(b))
    Pbcd.add(bn.variable(c))
    Pbcd.add(bn.variable(d))
    Pbcd.fill(1)

    Pabc=gum.Potential()
    Pabc.add(bn.variable(a))
    Pabc.add(bn.variable(b))
    Pabc.add(bn.variable(c))
    Pabc.fill(1)

    Pace=gum.Potential()
    Pace.add(bn.variable(a))
    Pace.add(bn.variable(c))
    Pace.add(bn.variable(e))
    Pace.fill(1)

    #cpts
    Pabc.multiplicateBy(bn.cpt(a))
    Pabc.multiplicateBy(bn.cpt(b))
    Pace.multiplicateBy(bn.cpt(c))
    Pbcd.multiplicateBy(bn.cpt(d))
    Pace.multiplicateBy(bn.cpt(e))

    #evidence
    evE1=gum.Potential()
    evE1.add(bn.variable(e))
    evE1.fill(0)
    evE1[{'e':1}]=1
    Pace.multiplicateBy(evE1)

    #ACE -> ABC
    Pace_abc=gum.Potential()
    Pace_abc.add(bn.variable(a))
    Pace_abc.add(bn.variable(c))
    Pace_abc.marginalize(Pace)
    Pabc.multiplicateBy(Pace_abc)

    #ABC -> BCD
    Pabc_bcd=gum.Potential()
    Pabc_bcd.add(bn.variable(b))
    Pabc_bcd.add(bn.variable(c))
    Pabc_bcd.marginalize(Pabc)
    Pbcd.multiplicateBy(Pabc_bcd)

    #BCD -> D
    Pd=gum.Potential()
    Pd.add(bn.variable(d))
    Pd.marginalize(Pbcd)
    Pd.normalize()

    return(Pd)

def EgivenD1(bn,a,b,c,d,e):
    # clique potential
    Pbcd=gum.Potential()
    Pbcd.add(bn.variable(b))
    Pbcd.add(bn.variable(c))
    Pbcd.add(bn.variable(d))
    Pbcd.fill(1)

    Pabc=gum.Potential()
    Pabc.add(bn.variable(a))
    Pabc.add(bn.variable(b))
    Pabc.add(bn.variable(c))
    Pabc.fill(1)

    Pace=gum.Potential()
    Pace.add(bn.variable(a))
    Pace.add(bn.variable(c))
    Pace.add(bn.variable(e))
    Pace.fill(1)

    #cpts
    Pabc.multiplicateBy(bn.cpt(a))
    Pabc.multiplicateBy(bn.cpt(b))
    Pace.multiplicateBy(bn.cpt(c))
    Pbcd.multiplicateBy(bn.cpt(d))
    Pace.multiplicateBy(bn.cpt(e))

    #evidence
    evD1=gum.Potential()
    evD1.add(bn.variable(d))
    evD1.fill(0)
    evD1[{'d':1}]=1
    Pbcd.multiplicateBy(evD1)

    #BCD -> ABC
    Pbcd_abc=gum.Potential()
    Pbcd_abc.add(bn.variable(b))
    Pbcd_abc.add(bn.variable(c))
    Pbcd_abc.marginalize(Pbcd)
    Pabc.multiplicateBy(Pbcd_abc)

    #ABC -> ACE
    Pabc_ace=gum.Potential()
    Pabc_ace.add(bn.variable(a))
    Pabc_ace.add(bn.variable(c))
    Pabc_ace.marginalize(Pabc)
    Pace.multiplicateBy(Pabc_ace)

    #ACE -> E
    Pe=gum.Potential()
    Pe.add(bn.variable(e))
    Pe.marginalize(Pace)
    Pe.normalize()

    return(Pe)

def AgivenD1E1(bn,a,b,c,d,e):
    # clique potential
    Pbcd=gum.Potential()
    Pbcd.add(bn.variable(b))
    Pbcd.add(bn.variable(c))
    Pbcd.add(bn.variable(d))
    Pbcd.fill(1)()

    Pabc=gum.Potential()
    Pabc.add(bn.variable(a))
    Pabc.add(bn.variable(b))
    Pabc.add(bn.variable(c))
    Pabc.fill(1)

    Pace=gum.Potential()
    Pace.add(bn.variable(a))
    Pace.add(bn.variable(c))
    Pace.add(bn.variable(e))
    Pace.fill(1)

    #cpts
    Pabc.multiplicateBy(bn.cpt(a))
    Pabc.multiplicateBy(bn.cpt(b))
    Pace.multiplicateBy(bn.cpt(c))
    Pbcd.multiplicateBy(bn.cpt(d))
    Pace.multiplicateBy(bn.cpt(e))

    #evidence D
    evD1=gum.Potential()
    evD1.add(bn.variable(d))
    evD1.fill(0)
    evD1[{'d':1}]=1
    Pbcd.multiplicateBy(evD1)

    #evidence E
    evE1=gum.Potential()
    evE1.add(bn.variable(e))
    evE1.fill(0)
    evE1[{'e':1}]=1
    Pace.multiplicateBy(evE1)

    #BCD -> ABC
    Pbcd_abc=gum.Potential()
    Pbcd_abc.add(bn.variable(b))
    Pbcd_abc.add(bn.variable(c))
    Pbcd_abc.marginalize(Pbcd)
    Pabc.multiplicateBy(Pbcd_abc)

    #ACE -> ABC
    Pace_abc=gum.Potential()
    Pace_abc.add(bn.variable(a))
    Pace_abc.add(bn.variable(c))
    Pace_abc.marginalize(Pace)
    Pabc.multiplicateBy(Pace_abc)

    #ABC -> E
    Pa=gum.Potential()
    Pa.add(bn.variable(a))
    Pa.marginalize(Pabc)
    Pa.normalize()

    return(Pa)


bn=gum.BayesNet()
a,b,c,d,e=[bn.add(gum.LabelizedVariable(s,s,2)) for s in 'abcde']
print("ICIIIIII"+str(a))
print(str(b))
print(str(c))
bn.addArc(a,b)
bn.addArc(a,c)
bn.addArc(b,d)
bn.addArc(c,d)
bn.addArc(e,c)

bn.generateCPTs()

print("\n"*5)

#showBN(bn,size="3")
#showJT(bn,size="3")



print("####################")
print("##### P(D|E=1) #####")
print("####################")
ie=gum.LazyPropagation(bn)
ie.setEvidence({"e":1})
ie.makeInference()
print("Lazy P(D|E=1)= {0}".format(ie.posterior(d)))
print("ICI!!!")
print("A la main    : {0}".format(DgivenE1(bn,a,b,c,d,e)))




print("####################")
print("##### P(E|D=1) #####")
print("####################")
ie.setEvidence({"d":1})
ie.makeInference()
print("Lazy P(E|D=1)= {0}".format(ie.posterior(e)))
print("A la main    : {0}".format(EgivenD1(bn,a,b,c,d,e)))


print("####################")
print("### P(A|D=1,E=1) ###")
print("####################")
ie.setEvidence({"d":1,"e":1})
ie.makeInference()
print("Lazy P(A|D=1,E=1)= {0}".format(ie.posterior(a)))
print("A la main        : {0}".format(AgivenD1E1(bn,a,b,c,d,e)))