################################################################################
# 2016-05-16 10:56:52 : genAsia.cs generated from /home/phw/Documents/gits/lip6/metaGenBayes/testPAND/config.yaml
#
# This file is generated by metaGenBayes 1.0.0 for CSharp ?
#
# Do not make changes to this file unless you know what you are doing
# Please modify the configuration file (/home/phw/Documents/gits/lip6/metaGenBayes/testPAND/config.yaml) instead.
################################################################################


Génération de 
 P0sachant= [0.009999999776482582, 0.9900000095367432];
 P1sachant0= [[0.05000000074505806, 0.949999988079071], [0.009999999776482582, 0.9900000095367432]];
 P2sachant1_4= [[[1.0, 0.0], [1.0, 0.0]], [[1.0, 0.0], [0.0, 1.0]]];
 P3sachant2= [[0.9800000190734863, 0.019999999552965164], [0.05000000074505806, 0.949999988079071]];
 P4sachant5= [[0.10000000149011612, 0.8999999761581421], [0.009999999776482582, 0.9900000095367432]];
 P5sachant= [0.5, 0.5];
 P6sachant5= [[0.6000000238418579, 0.4000000059604645], [0.30000001192092896, 0.699999988079071]];
 P7sachant6_2= [[[0.8999999761581421, 0.10000000149011612], [0.699999988079071, 0.30000001192092896]], [[0.800000011920929, 0.20000000298023224], [0.10000000149011612, 0.8999999761581421]]];
Creation de potentiel KIRMPR (vars=[2, 3],fill=1.0)
Creation de potentiel DCHFTL (vars=[2, 6, 7],fill=1.0)
Creation de potentiel PTGTQJ (vars=[0, 1],fill=1.0)
Creation de potentiel BKAODH (vars=[1, 2, 4],fill=1.0)
Creation de potentiel NOPOPL (vars=[2, 5, 6],fill=1.0)
Creation de potentiel BFXZJR (vars=[2, 4, 5],fill=1.0)
Multiplication du potentiel PTGTQJ par la cpt de la variable 0
Multiplication du potentiel PTGTQJ par la cpt de la variable 1
Multiplication du potentiel BKAODH par la cpt de la variable 2
Multiplication du potentiel KIRMPR par la cpt de la variable 3
Multiplication du potentiel BFXZJR par la cpt de la variable 4
Multiplication du potentiel NOPOPL par la cpt de la variable 5
Multiplication du potentiel NOPOPL par la cpt de la variable 6
Multiplication du potentiel DCHFTL par la cpt de la variable 7
Add soft evidence smoking? in EV_5'index:0,value=evs.get([5, 'smoking?'][1]))
Add soft evidence positive_XraY? in EV_3'index:0,value=evs.get([3, 'positive_XraY?'][1]))
Add soft evidence visit_to_Asia? in EV_0'index:0,value=evs.get([0, 'visit_to_Asia?'][1]))
Add soft evidence dyspnoea? in EV_7'index:0,value=evs.get([7, 'dyspnoea?'][1]))
Multiplication du potentiel PTGTQJ par le potentiel EV_0
Multiplication du potentiel KIRMPR par le potentiel EV_3
Multiplication du potentiel NOPOPL par le potentiel EV_5
Multiplication du potentiel DCHFTL par le potentiel EV_7
Creation de potentiel FWAVXF (vars=[2, 5, 6],fill=1.0)
Multiplication du potentiel FWAVXF par le potentiel NOPOPL
Creation de potentiel INPPGY (vars=[2, 4, 5],fill=1.0)
Multiplication du potentiel INPPGY par le potentiel BFXZJR
Creation de potentiel QYKXHT (vars=[1, 2, 4],fill=1.0)
Multiplication du potentiel QYKXHT par le potentiel BKAODH
Creation de potentiel NJUVGW (vars=[2, 4, 5],fill=1.0)
Multiplication du potentiel NJUVGW par le potentiel BFXZJR
Creation de potentiel LKQQCW (vars=[2, 6],fill=0.0)
Marginalisation de LKQQCW par DCHFTL
Multiplication du potentiel NOPOPL par le potentiel LKQQCW
Creation de potentiel KSVATI (vars=[1],fill=0.0)
Marginalisation de KSVATI par PTGTQJ
Multiplication du potentiel BKAODH par le potentiel KSVATI
Creation de potentiel BSKBJJ (vars=[2, 4],fill=0.0)
Marginalisation de BSKBJJ par BKAODH
Multiplication du potentiel BFXZJR par le potentiel BSKBJJ
Creation de potentiel AEONUX (vars=[2, 5],fill=0.0)
Marginalisation de AEONUX par BFXZJR
Multiplication du potentiel NOPOPL par le potentiel AEONUX
Creation de potentiel MUEKJB (vars=[2],fill=0.0)
Marginalisation de MUEKJB par KIRMPR
Multiplication du potentiel NOPOPL par le potentiel MUEKJB
Multiplication du potentiel FWAVXF par le potentiel LKQQCW
Multiplication du potentiel FWAVXF par le potentiel MUEKJB
Creation de potentiel LNEPUP (vars=[2, 5],fill=0.0)
Marginalisation de LNEPUP par FWAVXF
Multiplication du potentiel INPPGY par le potentiel LNEPUP
Creation de potentiel DFQRDW (vars=[2, 4],fill=0.0)
Marginalisation de DFQRDW par INPPGY
Multiplication du potentiel QYKXHT par le potentiel DFQRDW
Multiplication du potentiel QYKXHT par le potentiel KSVATI
Multiplication du potentiel NJUVGW par le potentiel BSKBJJ
Multiplication du potentiel NJUVGW par le potentiel LNEPUP
Creation de potentiel P_6 (vars=['6'],fill=0.0)
Marginalisation de P_6 par NOPOPL
Normalisation de bronchitis? on P_6
Creation de potentiel P_1 (vars=['1'],fill=0.0)
Marginalisation de P_1 par QYKXHT
Normalisation de tuberculosis? on P_1
Creation de potentiel P_4 (vars=['4'],fill=0.0)
Marginalisation de P_4 par NJUVGW
Normalisation de lung_cancer? on P_4


#
# Sample code using this function
#

