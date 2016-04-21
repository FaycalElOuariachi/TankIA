<?php
/*
# generation of CSharp
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
// CPO TRXTBH [2, 3] 1.0
  $TRXTBH=array_fill(0,2,array_fill(0,2,1.0));
// CPO FCPPAV [2, 6, 7] 1.0
  $FCPPAV=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
// CPO QUIBFX [0, 1] 1.0
  $QUIBFX=array_fill(0,2,array_fill(0,2,1.0));
// CPO HWIALF [1, 2, 4] 1.0
  $HWIALF=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
// CPO JDMAEU [2, 5, 6] 1.0
  $JDMAEU=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
// CPO MVTHNR [2, 4, 5] 1.0
  $MVTHNR=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
// MUC QUIBFX 0 [0, 1]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $QUIBFX[$i1][$i0] *= $P0sachant[$i0];
// MUC QUIBFX 1 [0, 1]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $QUIBFX[$i1][$i0] *= $P1sachant0[$i0][$i1];
// MUC HWIALF 2 [1, 2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $HWIALF[$i2][$i1][$i0] *= $P2sachant1_4[$i0][$i2][$i1];
// MUC TRXTBH 3 [2, 3]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $TRXTBH[$i1][$i0] *= $P3sachant2[$i0][$i1];
// MUC MVTHNR 4 [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $MVTHNR[$i2][$i1][$i0] *= $P4sachant5[$i2][$i1];
// MUC JDMAEU 5 [2, 5, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $JDMAEU[$i2][$i1][$i0] *= $P5sachant[$i1];
// MUC JDMAEU 6 [2, 5, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $JDMAEU[$i2][$i1][$i0] *= $P6sachant5[$i1][$i2];
// MUC FCPPAV 7 [2, 6, 7]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $FCPPAV[$i2][$i1][$i0] *= $P7sachant6_2[$i0][$i1][$i2];
// ASE smoking? EV_5 0 evs.get([5, 'smoking?'][1])
  $EV_5= $evs['smoking?'];
// ASE positive_XraY? EV_3 0 evs.get([3, 'positive_XraY?'][1])
  $EV_3= $evs['positive_XraY?'];
// ASE visit_to_Asia? EV_0 0 evs.get([0, 'visit_to_Asia?'][1])
  $EV_0= $evs['visit_to_Asia?'];
// ASE dyspnoea? EV_7 0 evs.get([7, 'dyspnoea?'][1])
  $EV_7= $evs['dyspnoea?'];
// MUL QUIBFX EV_0 [0, 1] ['0']
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $QUIBFX[$i1][$i0] *= $EV_0[$i0];
// MUL TRXTBH EV_3 [2, 3] ['3']
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $TRXTBH[$i1][$i0] *= $EV_3[$i1];
// MUL JDMAEU EV_5 [2, 5, 6] ['5']
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $JDMAEU[$i2][$i1][$i0] *= $EV_5[$i1];
// MUL FCPPAV EV_7 [2, 6, 7] ['7']
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $FCPPAV[$i2][$i1][$i0] *= $EV_7[$i2];
// CPO XAKXFL [2, 5, 6] 1.0
  $XAKXFL=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
// MUL XAKXFL JDMAEU [2, 5, 6] [2, 5, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $XAKXFL[$i2][$i1][$i0] *= $JDMAEU[$i2][$i1][$i0];
// CPO IFVUXZ [2, 4, 5] 1.0
  $IFVUXZ=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
// MUL IFVUXZ MVTHNR [2, 4, 5] [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $IFVUXZ[$i2][$i1][$i0] *= $MVTHNR[$i2][$i1][$i0];
// CPO JZGJUB [2, 4, 5] 1.0
  $JZGJUB=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
// MUL JZGJUB MVTHNR [2, 4, 5] [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $JZGJUB[$i2][$i1][$i0] *= $MVTHNR[$i2][$i1][$i0];
// CPO IPEKWS [1, 2, 4] 1.0
  $IPEKWS=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
// MUL IPEKWS HWIALF [1, 2, 4] [1, 2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $IPEKWS[$i2][$i1][$i0] *= $HWIALF[$i2][$i1][$i0];
// CPO ZLCECG [2, 6] 0.0
  $ZLCECG=array_fill(0,2,array_fill(0,2,0.0));
// MAR ZLCECG FCPPAV [2, 6] [2, 6, 7]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $ZLCECG[$i1][$i0] += $FCPPAV[$j0][$i1][$i0];
// MUL JDMAEU ZLCECG [2, 5, 6] [2, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $JDMAEU[$i2][$i1][$i0] *= $ZLCECG[$i2][$i0];
// CPO KNMSKT [1] 0.0
  $KNMSKT=array_fill(0,2,0.0);
// MAR KNMSKT QUIBFX [1] [0, 1]
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      $KNMSKT[$i0] += $QUIBFX[$i0][$j0];
// MUL HWIALF KNMSKT [1, 2, 4] [1]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $HWIALF[$i2][$i1][$i0] *= $KNMSKT[$i0];
// CPO KIKCCO [2, 4] 0.0
  $KIKCCO=array_fill(0,2,array_fill(0,2,0.0));
// MAR KIKCCO HWIALF [2, 4] [1, 2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $KIKCCO[$i1][$i0] += $HWIALF[$i1][$i0][$j0];
// MUL MVTHNR KIKCCO [2, 4, 5] [2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $MVTHNR[$i2][$i1][$i0] *= $KIKCCO[$i1][$i0];
// CPO KVWKZF [2, 5] 0.0
  $KVWKZF=array_fill(0,2,array_fill(0,2,0.0));
// MAR KVWKZF MVTHNR [2, 5] [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $KVWKZF[$i1][$i0] += $MVTHNR[$i1][$j0][$i0];
// MUL JDMAEU KVWKZF [2, 5, 6] [2, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $JDMAEU[$i2][$i1][$i0] *= $KVWKZF[$i1][$i0];
// CPO ZXLINC [2] 0.0
  $ZXLINC=array_fill(0,2,0.0);
// MAR ZXLINC TRXTBH [2] [2, 3]
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      $ZXLINC[$i0] += $TRXTBH[$j0][$i0];
// MUL JDMAEU ZXLINC [2, 5, 6] [2]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $JDMAEU[$i2][$i1][$i0] *= $ZXLINC[$i0];
// MUL XAKXFL ZLCECG [2, 5, 6] [2, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $XAKXFL[$i2][$i1][$i0] *= $ZLCECG[$i2][$i0];
// MUL XAKXFL ZXLINC [2, 5, 6] [2]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $XAKXFL[$i2][$i1][$i0] *= $ZXLINC[$i0];
// CPO NBUOFU [2, 5] 0.0
  $NBUOFU=array_fill(0,2,array_fill(0,2,0.0));
// MAR NBUOFU XAKXFL [2, 5] [2, 5, 6]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $NBUOFU[$i1][$i0] += $XAKXFL[$j0][$i1][$i0];
// MUL IFVUXZ NBUOFU [2, 4, 5] [2, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $IFVUXZ[$i2][$i1][$i0] *= $NBUOFU[$i2][$i0];
// CPO ZMUCOI [2, 4] 0.0
  $ZMUCOI=array_fill(0,2,array_fill(0,2,0.0));
// MAR ZMUCOI IFVUXZ [2, 4] [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $ZMUCOI[$i1][$i0] += $IFVUXZ[$j0][$i1][$i0];
// MUL JZGJUB KIKCCO [2, 4, 5] [2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $JZGJUB[$i2][$i1][$i0] *= $KIKCCO[$i1][$i0];
// MUL JZGJUB NBUOFU [2, 4, 5] [2, 5]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $JZGJUB[$i2][$i1][$i0] *= $NBUOFU[$i2][$i0];
// MUL IPEKWS ZMUCOI [1, 2, 4] [2, 4]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $IPEKWS[$i2][$i1][$i0] *= $ZMUCOI[$i2][$i1];
// MUL IPEKWS KNMSKT [1, 2, 4] [1]
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $IPEKWS[$i2][$i1][$i0] *= $KNMSKT[$i0];
// CPO P_6 ['6'] 0.0
  $P_6=array_fill(0,2,0.0);
// MAR P_6 JDMAEU [6] [2, 5, 6]
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      for($j1=0;$j1<2;$j1++)
        $P_6[$i0] += $JDMAEU[$i0][$j1][$j0];
// NOR P_6 bronchitis?
  $sum=0.0;
  for($i0=0;$i0<count($P_6);$i0++)
    $sum+=$P_6[$i0];
  for($i0=0;$i0<count($P_6);$i0++)
    $P_6[$i0]/=$sum;
  $res['bronchitis?']=$P_6;
// CPO P_4 ['4'] 0.0
  $P_4=array_fill(0,2,0.0);
// MAR P_4 JZGJUB [4] [2, 4, 5]
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      for($j1=0;$j1<2;$j1++)
        $P_4[$i0] += $JZGJUB[$j1][$i0][$j0];
// NOR P_4 lung_cancer?
  $sum=0.0;
  for($i0=0;$i0<count($P_4);$i0++)
    $sum+=$P_4[$i0];
  for($i0=0;$i0<count($P_4);$i0++)
    $P_4[$i0]/=$sum;
  $res['lung_cancer?']=$P_4;
// CPO P_1 ['1'] 0.0
  $P_1=array_fill(0,2,0.0);
// MAR P_1 IPEKWS [1] [1, 2, 4]
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      for($j1=0;$j1<2;$j1++)
        $P_1[$i0] += $IPEKWS[$j1][$j0][$i0];
// NOR P_1 tuberculosis?
  $sum=0.0;
  for($i0=0;$i0<count($P_1);$i0++)
    $sum+=$P_1[$i0];
  for($i0=0;$i0<count($P_1);$i0++)
    $P_1[$i0]/=$sum;
  $res['tuberculosis?']=$P_1;
  return $res;
}


//
// Sample code using this function
//
foreach(getProbaForAsia(array(
    "dyspnoea?" => [1, 0.5],
    "smoking?" => [1, 1],
    "visit_to_Asia?" => [0, 1],
    "positive_XraY?" => [1,1]
)) as $var=>$proba) {
  echo($var." => ");
  foreach ($proba as $v=>$p)
    echo("$v : $p | ");
  echo("\n");
}
