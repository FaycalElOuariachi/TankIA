<?php
/*
################################################################################
# 2015-11-25 17:25:26 : Tests/genAsia.php generated from config.yaml
#
# This file is generated by metaGenBayes 1.0.0 for PHP >=5.6
#
# Do not make changes to this file unless you know what you are doing
# Please modify the configuration file (config.yaml) instead.
################################################################################
*/

function getProbaForAsia($evs) {
  $res=[];
  $P0sachant= [0.009999999776482582, 0.9900000095367432];
  $P1sachant0= [[0.05000000074505806, 0.949999988079071], [0.009999999776482582, 0.9900000095367432]];
  $P2sachant1_4= [[[1.0, 0.0], [1.0, 0.0]], [[1.0, 0.0], [0.0, 1.0]]];
  $P3sachant2= [[0.9800000190734863, 0.019999999552965164], [0.05000000074505806, 0.949999988079071]];
  $P4sachant5= [[0.10000000149011612, 0.8999999761581421], [0.009999999776482582, 0.9900000095367432]];
  $P5sachant= [0.5, 0.5];
  $P6sachant5= [[0.6000000238418579, 0.4000000059604645], [0.30000001192092896, 0.699999988079071]];
  $P7sachant6_2= [[[0.8999999761581421, 0.10000000149011612], [0.699999988079071, 0.30000001192092896]], [[0.800000011920929, 0.20000000298023224], [0.10000000149011612, 0.8999999761581421]]];
// CPO __PHI__2_3_c0 [2, 3]
  $__PHI__2_3_c0=array_fill(0,2,array_fill(0,2,0.0));
// ADV 2 __PHI__2_3_c0
// ADV 3 __PHI__2_3_c0
// FIL __PHI__2_3_c0 1
// CPO __PHI__2_6_7_c1 [2, 6, 7]
  $__PHI__2_6_7_c1=array_fill(0,2,array_fill(0,2,array_fill(0,2,0.0)));
// ADV 2 __PHI__2_6_7_c1
// ADV 6 __PHI__2_6_7_c1
// ADV 7 __PHI__2_6_7_c1
// FIL __PHI__2_6_7_c1 1
// CPO __PHI__0_1_c2 [0, 1]
  $__PHI__0_1_c2=array_fill(0,2,array_fill(0,2,0.0));
// ADV 0 __PHI__0_1_c2
// ADV 1 __PHI__0_1_c2
// FIL __PHI__0_1_c2 1
// CPO __PHI__1_2_4_c3 [1, 2, 4]
  $__PHI__1_2_4_c3=array_fill(0,2,array_fill(0,2,array_fill(0,2,0.0)));
// ADV 1 __PHI__1_2_4_c3
// ADV 2 __PHI__1_2_4_c3
// ADV 4 __PHI__1_2_4_c3
// FIL __PHI__1_2_4_c3 1
// CPO __PHI__2_5_6_c4 [2, 5, 6]
  $__PHI__2_5_6_c4=array_fill(0,2,array_fill(0,2,array_fill(0,2,0.0)));
// ADV 2 __PHI__2_5_6_c4
// ADV 5 __PHI__2_5_6_c4
// ADV 6 __PHI__2_5_6_c4
// FIL __PHI__2_5_6_c4 1
// CPO __PHI__2_4_5_c5 [2, 4, 5]
  $__PHI__2_4_5_c5=array_fill(0,2,array_fill(0,2,array_fill(0,2,0.0)));
// ADV 2 __PHI__2_4_5_c5
// ADV 4 __PHI__2_4_5_c5
// ADV 5 __PHI__2_4_5_c5
// FIL __PHI__2_4_5_c5 1
// MUC __PHI__0_1_c2 0 [0, 1]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $__PHI__0_1_c2[$i1][$i0] *= $P0sachant[$i0];
// MUC __PHI__0_1_c2 1 [0, 1]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $__PHI__0_1_c2[$i1][$i0] *= $P1sachant0[$i0][$i1];
// MUC __PHI__1_2_4_c3 2 [1, 2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__1_2_4_c3[$i2][$i1][$i0] *= $P2sachant1_4[$i0][$i2][$i1];
// MUC __PHI__2_3_c0 3 [2, 3]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $__PHI__2_3_c0[$i1][$i0] *= $P3sachant2[$i0][$i1];
// MUC __PHI__2_4_5_c5 4 [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_4_5_c5[$i2][$i1][$i0] *= $P4sachant5[$i2][$i1];
// MUC __PHI__2_5_6_c4 5 [2, 5, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_5_6_c4[$i2][$i1][$i0] *= $P5sachant[$i1];
// MUC __PHI__2_5_6_c4 6 [2, 5, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_5_6_c4[$i2][$i1][$i0] *= $P6sachant5[$i1][$i2];
// MUC __PHI__2_6_7_c1 7 [2, 6, 7]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_6_7_c1[$i2][$i1][$i0] *= $P7sachant6_2[$i0][$i1][$i2];
// CPO EV_5 5
// ADV 5 EV_5
// ASE smoking? EV_5 0 evs.get([5, 'smoking?'][1])
  $EV_5= $evs['smoking?'];
// CPO EV_0 0
// ADV 0 EV_0
// ASE visit_to_Asia? EV_0 0 evs.get([0, 'visit_to_Asia?'][1])
  $EV_0= $evs['visit_to_Asia?'];
// CPO EV_7 7
// ADV 7 EV_7
// ASE dyspnoea? EV_7 0 evs.get([7, 'dyspnoea?'][1])
  $EV_7= $evs['dyspnoea?'];
// CPO EV_3 3
// ADV 3 EV_3
// ASE positive_XraY? EV_3 0 evs.get([3, 'positive_XraY?'][1])
  $EV_3= $evs['positive_XraY?'];
// MUL __PHI__0_1_c2 EV_0 [0, 1] ['0']
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $__PHI__0_1_c2[$i1][$i0] *= $EV_0[$i0];
// MUL __PHI__2_3_c0 EV_3 [2, 3] ['3']
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $__PHI__2_3_c0[$i1][$i0] *= $EV_3[$i1];
// MUL __PHI__2_5_6_c4 EV_5 [2, 5, 6] ['5']
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_5_6_c4[$i2][$i1][$i0] *= $EV_5[$i1];
// MUL __PHI__2_6_7_c1 EV_7 [2, 6, 7] ['7']
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_6_7_c1[$i2][$i1][$i0] *= $EV_7[$i2];
// CPO __PHI__2_5_6_to__PHI__2_4_5_c4 [2, 5, 6]
  $__PHI__2_5_6_to__PHI__2_4_5_c4=array_fill(0,2,array_fill(0,2,array_fill(0,2,0.0)));
// ADV 2 __PHI__2_5_6_to__PHI__2_4_5_c4
// ADV 5 __PHI__2_5_6_to__PHI__2_4_5_c4
// ADV 6 __PHI__2_5_6_to__PHI__2_4_5_c4
// FIL __PHI__2_5_6_to__PHI__2_4_5_c4 1
// MUL __PHI__2_5_6_to__PHI__2_4_5_c4 __PHI__2_5_6_c4 [2, 5, 6] [2, 5, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_5_6_to__PHI__2_4_5_c4[$i2][$i1][$i0] *= $__PHI__2_5_6_c4[$i2][$i1][$i0];
// CPO __PHI__2_4_5_to__PHI__1_2_4_c5 [2, 4, 5]
  $__PHI__2_4_5_to__PHI__1_2_4_c5=array_fill(0,2,array_fill(0,2,array_fill(0,2,0.0)));
// ADV 2 __PHI__2_4_5_to__PHI__1_2_4_c5
// ADV 4 __PHI__2_4_5_to__PHI__1_2_4_c5
// ADV 5 __PHI__2_4_5_to__PHI__1_2_4_c5
// FIL __PHI__2_4_5_to__PHI__1_2_4_c5 1
// MUL __PHI__2_4_5_to__PHI__1_2_4_c5 __PHI__2_4_5_c5 [2, 4, 5] [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_4_5_to__PHI__1_2_4_c5[$i2][$i1][$i0] *= $__PHI__2_4_5_c5[$i2][$i1][$i0];
// CPO __PHI__1_2_4_tar [1, 2, 4]
  $__PHI__1_2_4_tar=array_fill(0,2,array_fill(0,2,array_fill(0,2,0.0)));
// ADV 1 __PHI__1_2_4_tar
// ADV 2 __PHI__1_2_4_tar
// ADV 4 __PHI__1_2_4_tar
// FIL __PHI__1_2_4_tar 1
// MUL __PHI__1_2_4_tar __PHI__1_2_4_c3 [1, 2, 4] [1, 2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__1_2_4_tar[$i2][$i1][$i0] *= $__PHI__1_2_4_c3[$i2][$i1][$i0];
// CPO __PHI__2_4_5_tar [2, 4, 5]
  $__PHI__2_4_5_tar=array_fill(0,2,array_fill(0,2,array_fill(0,2,0.0)));
// ADV 2 __PHI__2_4_5_tar
// ADV 4 __PHI__2_4_5_tar
// ADV 5 __PHI__2_4_5_tar
// FIL __PHI__2_4_5_tar 1
// MUL __PHI__2_4_5_tar __PHI__2_4_5_c5 [2, 4, 5] [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_4_5_tar[$i2][$i1][$i0] *= $__PHI__2_4_5_c5[$i2][$i1][$i0];
// CPO __PSI__2_6_7_xx2_5_6_c1_1c2_4 [2, 6]
  $__PSI__2_6_7_xx2_5_6_c1_1c2_4=array_fill(0,2,array_fill(0,2,0.0));
// ADV 2 __PSI__2_6_7_xx2_5_6_c1_1c2_4
// ADV 6 __PSI__2_6_7_xx2_5_6_c1_1c2_4
// MAR BN{nodes: 8, arcs: 8, domainSize: 256, parameters: 36, compression ratio: 85% } __PSI__2_6_7_xx2_5_6_c1_1c2_4 __PHI__2_6_7_c1 [2, 6] [2, 6, 7]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $__PSI__2_6_7_xx2_5_6_c1_1c2_4[$i1][$i0] += $__PHI__2_6_7_c1[$j0][$i1][$i0];
// MUL __PHI__2_5_6_c4 __PSI__2_6_7_xx2_5_6_c1_1c2_4 [2, 5, 6] [2, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_5_6_c4[$i2][$i1][$i0] *= $__PSI__2_6_7_xx2_5_6_c1_1c2_4[$i2][$i0];
// CPO __PSI__0_1_xx1_2_4_c1_2c2_3 [1]
  $__PSI__0_1_xx1_2_4_c1_2c2_3=array_fill(0,2,0.0);
// ADV 1 __PSI__0_1_xx1_2_4_c1_2c2_3
// MAR BN{nodes: 8, arcs: 8, domainSize: 256, parameters: 36, compression ratio: 85% } __PSI__0_1_xx1_2_4_c1_2c2_3 __PHI__0_1_c2 [1] [0, 1]
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      $__PSI__0_1_xx1_2_4_c1_2c2_3[$i0] += $__PHI__0_1_c2[$i0][$j0];
// MUL __PHI__1_2_4_c3 __PSI__0_1_xx1_2_4_c1_2c2_3 [1, 2, 4] [1]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__1_2_4_c3[$i2][$i1][$i0] *= $__PSI__0_1_xx1_2_4_c1_2c2_3[$i0];
// CPO __PSI__1_2_4_xx2_4_5_c1_3c2_5 [2, 4]
  $__PSI__1_2_4_xx2_4_5_c1_3c2_5=array_fill(0,2,array_fill(0,2,0.0));
// ADV 2 __PSI__1_2_4_xx2_4_5_c1_3c2_5
// ADV 4 __PSI__1_2_4_xx2_4_5_c1_3c2_5
// MAR BN{nodes: 8, arcs: 8, domainSize: 256, parameters: 36, compression ratio: 85% } __PSI__1_2_4_xx2_4_5_c1_3c2_5 __PHI__1_2_4_c3 [2, 4] [1, 2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $__PSI__1_2_4_xx2_4_5_c1_3c2_5[$i1][$i0] += $__PHI__1_2_4_c3[$i1][$i0][$j0];
// MUL __PHI__2_4_5_c5 __PSI__1_2_4_xx2_4_5_c1_3c2_5 [2, 4, 5] [2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_4_5_c5[$i2][$i1][$i0] *= $__PSI__1_2_4_xx2_4_5_c1_3c2_5[$i1][$i0];
// CPO __PSI__2_4_5_xx2_5_6_c1_5c2_4 [2, 5]
  $__PSI__2_4_5_xx2_5_6_c1_5c2_4=array_fill(0,2,array_fill(0,2,0.0));
// ADV 2 __PSI__2_4_5_xx2_5_6_c1_5c2_4
// ADV 5 __PSI__2_4_5_xx2_5_6_c1_5c2_4
// MAR BN{nodes: 8, arcs: 8, domainSize: 256, parameters: 36, compression ratio: 85% } __PSI__2_4_5_xx2_5_6_c1_5c2_4 __PHI__2_4_5_c5 [2, 5] [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $__PSI__2_4_5_xx2_5_6_c1_5c2_4[$i1][$i0] += $__PHI__2_4_5_c5[$i1][$j0][$i0];
// MUL __PHI__2_5_6_c4 __PSI__2_4_5_xx2_5_6_c1_5c2_4 [2, 5, 6] [2, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_5_6_c4[$i2][$i1][$i0] *= $__PSI__2_4_5_xx2_5_6_c1_5c2_4[$i1][$i0];
// CPO __PSI__2_3_xx2_5_6_c1_0c2_4 [2]
  $__PSI__2_3_xx2_5_6_c1_0c2_4=array_fill(0,2,0.0);
// ADV 2 __PSI__2_3_xx2_5_6_c1_0c2_4
// MAR BN{nodes: 8, arcs: 8, domainSize: 256, parameters: 36, compression ratio: 85% } __PSI__2_3_xx2_5_6_c1_0c2_4 __PHI__2_3_c0 [2] [2, 3]
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      $__PSI__2_3_xx2_5_6_c1_0c2_4[$i0] += $__PHI__2_3_c0[$j0][$i0];
// MUL __PHI__2_5_6_c4 __PSI__2_3_xx2_5_6_c1_0c2_4 [2, 5, 6] [2]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_5_6_c4[$i2][$i1][$i0] *= $__PSI__2_3_xx2_5_6_c1_0c2_4[$i0];
// MUL __PHI__2_5_6_to__PHI__2_4_5_c4 __PSI__2_6_7_xx2_5_6_c1_1c2_4 [2, 5, 6] [2, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_5_6_to__PHI__2_4_5_c4[$i2][$i1][$i0] *= $__PSI__2_6_7_xx2_5_6_c1_1c2_4[$i2][$i0];
// MUL __PHI__2_5_6_to__PHI__2_4_5_c4 __PSI__2_3_xx2_5_6_c1_0c2_4 [2, 5, 6] [2]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_5_6_to__PHI__2_4_5_c4[$i2][$i1][$i0] *= $__PSI__2_3_xx2_5_6_c1_0c2_4[$i0];
// CPO __PSI__2_5_6_xx2_4_5_c1_4c2_5 [2, 5]
  $__PSI__2_5_6_xx2_4_5_c1_4c2_5=array_fill(0,2,array_fill(0,2,0.0));
// ADV 2 __PSI__2_5_6_xx2_4_5_c1_4c2_5
// ADV 5 __PSI__2_5_6_xx2_4_5_c1_4c2_5
// MAR BN{nodes: 8, arcs: 8, domainSize: 256, parameters: 36, compression ratio: 85% } __PSI__2_5_6_xx2_4_5_c1_4c2_5 __PHI__2_5_6_to__PHI__2_4_5_c4 [2, 5] [2, 5, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $__PSI__2_5_6_xx2_4_5_c1_4c2_5[$i1][$i0] += $__PHI__2_5_6_to__PHI__2_4_5_c4[$j0][$i1][$i0];
// MUL __PHI__2_4_5_to__PHI__1_2_4_c5 __PSI__2_5_6_xx2_4_5_c1_4c2_5 [2, 4, 5] [2, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_4_5_to__PHI__1_2_4_c5[$i2][$i1][$i0] *= $__PSI__2_5_6_xx2_4_5_c1_4c2_5[$i2][$i0];
// CPO __PSI__2_4_5_xx1_2_4_c1_5c2_3 [2, 4]
  $__PSI__2_4_5_xx1_2_4_c1_5c2_3=array_fill(0,2,array_fill(0,2,0.0));
// ADV 2 __PSI__2_4_5_xx1_2_4_c1_5c2_3
// ADV 4 __PSI__2_4_5_xx1_2_4_c1_5c2_3
// MAR BN{nodes: 8, arcs: 8, domainSize: 256, parameters: 36, compression ratio: 85% } __PSI__2_4_5_xx1_2_4_c1_5c2_3 __PHI__2_4_5_to__PHI__1_2_4_c5 [2, 4] [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $__PSI__2_4_5_xx1_2_4_c1_5c2_3[$i1][$i0] += $__PHI__2_4_5_to__PHI__1_2_4_c5[$j0][$i1][$i0];
// MUL __PHI__1_2_4_tar __PSI__2_4_5_xx1_2_4_c1_5c2_3 [1, 2, 4] [2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__1_2_4_tar[$i2][$i1][$i0] *= $__PSI__2_4_5_xx1_2_4_c1_5c2_3[$i2][$i1];
// MUL __PHI__1_2_4_tar __PSI__0_1_xx1_2_4_c1_2c2_3 [1, 2, 4] [1]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__1_2_4_tar[$i2][$i1][$i0] *= $__PSI__0_1_xx1_2_4_c1_2c2_3[$i0];
// MUL __PHI__2_4_5_tar __PSI__1_2_4_xx2_4_5_c1_3c2_5 [2, 4, 5] [2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_4_5_tar[$i2][$i1][$i0] *= $__PSI__1_2_4_xx2_4_5_c1_3c2_5[$i1][$i0];
// MUL __PHI__2_4_5_tar __PSI__2_5_6_xx2_4_5_c1_4c2_5 [2, 4, 5] [2, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $__PHI__2_4_5_tar[$i2][$i1][$i0] *= $__PSI__2_5_6_xx2_4_5_c1_4c2_5[$i2][$i0];
// CPO P_6 ['6']
  $P_6=array_fill(0,2,0.0);
// ADV 6 P_6
// MAR BN{nodes: 8, arcs: 8, domainSize: 256, parameters: 36, compression ratio: 85% } P_6 __PHI__2_5_6_c4 [6] [2, 5, 6]
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      for($j1=0;$j1<2;$j1++)
        $P_6[$i0] += $__PHI__2_5_6_c4[$i0][$j1][$j0];
// NOR P_6 bronchitis?
  $sum=0.0;
  for($i0=0;$i0<count($P_6);$i0++)
    $sum+=$P_6[$i0];
  for($i0=0;$i0<count($P_6);$i0++)
    $P_6[$i0]/=$sum;
  $res['bronchitis?']=$P_6;
// CPO P_1 ['1']
  $P_1=array_fill(0,2,0.0);
// ADV 1 P_1
// MAR BN{nodes: 8, arcs: 8, domainSize: 256, parameters: 36, compression ratio: 85% } P_1 __PHI__1_2_4_tar [1] [1, 2, 4]
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      for($j1=0;$j1<2;$j1++)
        $P_1[$i0] += $__PHI__1_2_4_tar[$j1][$j0][$i0];
// NOR P_1 tuberculosis?
  $sum=0.0;
  for($i0=0;$i0<count($P_1);$i0++)
    $sum+=$P_1[$i0];
  for($i0=0;$i0<count($P_1);$i0++)
    $P_1[$i0]/=$sum;
  $res['tuberculosis?']=$P_1;
// CPO P_4 ['4']
  $P_4=array_fill(0,2,0.0);
// ADV 4 P_4
// MAR BN{nodes: 8, arcs: 8, domainSize: 256, parameters: 36, compression ratio: 85% } P_4 __PHI__2_4_5_tar [4] [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      for($j1=0;$j1<2;$j1++)
        $P_4[$i0] += $__PHI__2_4_5_tar[$j1][$i0][$j0];
// NOR P_4 lung_cancer?
  $sum=0.0;
  for($i0=0;$i0<count($P_4);$i0++)
    $sum+=$P_4[$i0];
  for($i0=0;$i0<count($P_4);$i0++)
    $P_4[$i0]/=$sum;
  $res['lung_cancer?']=$P_4;
  return $res;
}
echo("{");
$bb=0;
foreach(getProbaForAsia(array(
    "smoking?" => [],
    "visit_to_Asia?" => [],
    "dyspnoea?" => [],
    "positive_XraY?" => []
)) as $k=>$v) {
    if($bb==1) echo(",");
    $bb=1;    echo("'$k': [[");
    $b=0;
    foreach($v as $val) {
      if ($b==1) echo(",");
      $b=1;
      echo(" ");
      echo($val);
    }
    echo("]]");
}
echo("}");