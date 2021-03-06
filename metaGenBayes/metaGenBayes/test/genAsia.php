<?php
/*
################################################################################
# 2016-02-01 20:40:44 : genAsia.php generated from /home/phw/Documents/gits/lip6/metaGenBayes/metaGenBayes/test/config.yaml
#
# This file is generated by metaGenBayes 1.0.0 for PHP >=5.6
#
# Do not make changes to this file unless you know what you are doing
# Please modify the configuration file (/home/phw/Documents/gits/lip6/metaGenBayes/metaGenBayes/test/config.yaml) instead.
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
  $FCYWBU=array_fill(0,2,array_fill(0,2,1.0));
  $GGCPOB=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
  $COINHJ=array_fill(0,2,array_fill(0,2,1.0));
  $WXZLYA=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
  $UJKNOP=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
  $CUOCCM=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $COINHJ[$i1][$i0] *= $P0sachant[$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $COINHJ[$i1][$i0] *= $P1sachant0[$i0][$i1];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $WXZLYA[$i2][$i1][$i0] *= $P2sachant1_4[$i0][$i2][$i1];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $FCYWBU[$i1][$i0] *= $P3sachant2[$i0][$i1];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $CUOCCM[$i2][$i1][$i0] *= $P4sachant5[$i2][$i1];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $UJKNOP[$i2][$i1][$i0] *= $P5sachant[$i1];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $UJKNOP[$i2][$i1][$i0] *= $P6sachant5[$i1][$i2];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $GGCPOB[$i2][$i1][$i0] *= $P7sachant6_2[$i0][$i1][$i2];
  $EV_0= $evs['visit_to_Asia?'];
  $EV_3= $evs['positive_XraY?'];
  $EV_5= $evs['smoking?'];
  $EV_7= $evs['dyspnoea?'];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $COINHJ[$i1][$i0] *= $EV_0[$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      $FCYWBU[$i1][$i0] *= $EV_3[$i1];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $UJKNOP[$i2][$i1][$i0] *= $EV_5[$i1];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $GGCPOB[$i2][$i1][$i0] *= $EV_7[$i2];
  $WNVORY=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $WNVORY[$i2][$i1][$i0] *= $UJKNOP[$i2][$i1][$i0];
  $OIDNHI=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $OIDNHI[$i2][$i1][$i0] *= $CUOCCM[$i2][$i1][$i0];
  $KOGAET=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $KOGAET[$i2][$i1][$i0] *= $WXZLYA[$i2][$i1][$i0];
  $XXCUWZ=array_fill(0,2,array_fill(0,2,array_fill(0,2,1.0)));
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $XXCUWZ[$i2][$i1][$i0] *= $CUOCCM[$i2][$i1][$i0];
  $SPAETV=array_fill(0,2,array_fill(0,2,0.0));
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $SPAETV[$i1][$i0] += $GGCPOB[$j0][$i1][$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $UJKNOP[$i2][$i1][$i0] *= $SPAETV[$i2][$i0];
  $MZHWGO=array_fill(0,2,0.0);
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      $MZHWGO[$i0] += $COINHJ[$i0][$j0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $WXZLYA[$i2][$i1][$i0] *= $MZHWGO[$i0];
  $ZJGDKY=array_fill(0,2,array_fill(0,2,0.0));
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $ZJGDKY[$i1][$i0] += $WXZLYA[$i1][$i0][$j0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $CUOCCM[$i2][$i1][$i0] *= $ZJGDKY[$i1][$i0];
  $DHMBNM=array_fill(0,2,array_fill(0,2,0.0));
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $DHMBNM[$i1][$i0] += $CUOCCM[$i1][$j0][$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $UJKNOP[$i2][$i1][$i0] *= $DHMBNM[$i1][$i0];
  $QJAZOC=array_fill(0,2,0.0);
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      $QJAZOC[$i0] += $FCYWBU[$j0][$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $UJKNOP[$i2][$i1][$i0] *= $QJAZOC[$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $WNVORY[$i2][$i1][$i0] *= $SPAETV[$i2][$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $WNVORY[$i2][$i1][$i0] *= $QJAZOC[$i0];
  $OEWVTV=array_fill(0,2,array_fill(0,2,0.0));
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $OEWVTV[$i1][$i0] += $WNVORY[$j0][$i1][$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $OIDNHI[$i2][$i1][$i0] *= $OEWVTV[$i2][$i0];
  $FMQQXW=array_fill(0,2,array_fill(0,2,0.0));
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($j0=0;$j0<2;$j0++)
        $FMQQXW[$i1][$i0] += $OIDNHI[$j0][$i1][$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $KOGAET[$i2][$i1][$i0] *= $FMQQXW[$i2][$i1];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $KOGAET[$i2][$i1][$i0] *= $MZHWGO[$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $XXCUWZ[$i2][$i1][$i0] *= $ZJGDKY[$i1][$i0];
  for($i0=0;$i0<2;$i0++)
    for($i1=0;$i1<2;$i1++)
      for($i2=0;$i2<2;$i2++)
        $XXCUWZ[$i2][$i1][$i0] *= $OEWVTV[$i2][$i0];
  $GNRLDE=array_fill(0,2,0.0);
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      for($j1=0;$j1<2;$j1++)
        $GNRLDE[$i0] += $UJKNOP[$i0][$j1][$j0];
  $sum=0.0;
  for($i0=0;$i0<count($GNRLDE);$i0++)
    $sum+=$GNRLDE[$i0];
  for($i0=0;$i0<count($GNRLDE);$i0++)
    $GNRLDE[$i0]/=$sum;
  $res['bronchitis?']=$GNRLDE;
  $CXZIEH=array_fill(0,2,0.0);
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      for($j1=0;$j1<2;$j1++)
        $CXZIEH[$i0] += $KOGAET[$j1][$j0][$i0];
  $sum=0.0;
  for($i0=0;$i0<count($CXZIEH);$i0++)
    $sum+=$CXZIEH[$i0];
  for($i0=0;$i0<count($CXZIEH);$i0++)
    $CXZIEH[$i0]/=$sum;
  $res['tuberculosis?']=$CXZIEH;
  $AGRUQA=array_fill(0,2,0.0);
  for($i0=0;$i0<2;$i0++)
    for($j0=0;$j0<2;$j0++)
      for($j1=0;$j1<2;$j1++)
        $AGRUQA[$i0] += $XXCUWZ[$j1][$i0][$j0];
  $sum=0.0;
  for($i0=0;$i0<count($AGRUQA);$i0++)
    $sum+=$AGRUQA[$i0];
  for($i0=0;$i0<count($AGRUQA);$i0++)
    $AGRUQA[$i0]/=$sum;
  $res['lung_cancer?']=$AGRUQA;
  return $res;
}

foreach(getProbaForAsia(array(
    "visit_to_Asia?" => [],
    "positive_XraY?" => [],
    "smoking?" => [],
    "dyspnoea?" => []
)) as $var=>$proba) {
  echo($var." => ");
  foreach ($proba as $v=>$p)
    echo("$v : $p | ");
  echo("\n");
}