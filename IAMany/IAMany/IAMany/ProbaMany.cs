# generation of CSharp

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProbaMany {
	// Generation de 
	private double sum;
	private double[] P0sachant= {0.009999999776482582, 0.9900000095367432};
	private double[,] P1sachant0= {{0.05000000074505806, 0.949999988079071}, {0.009999999776482582, 0.9900000095367432}};
	private double[,,] P2sachant1_4= {{{1.0, 0.0}, {1.0, 0.0}}, {{1.0, 0.0}, {0.0, 1.0}}};
	private double[,] P3sachant2= {{0.9800000190734863, 0.019999999552965164}, {0.05000000074505806, 0.949999988079071}};
	private double[,] P4sachant5= {{0.10000000149011612, 0.8999999761581421}, {0.009999999776482582, 0.9900000095367432}};
	private double[] P5sachant= {0.5, 0.5};
	private double[,] P6sachant5= {{0.6000000238418579, 0.4000000059604645}, {0.30000001192092896, 0.699999988079071}};
	private double[,,] P7sachant6_2= {{{0.8999999761581421, 0.10000000149011612}, {0.699999988079071, 0.30000001192092896}}, {{0.800000011920929, 0.20000000298023224}, {0.10000000149011612, 0.8999999761581421}}};

	public Dictionary<string, double[]> getProbasIA(Dictionary<string,double[]> evs){
		Dictionary<string, double[]> res = new Dictionary<string, double[]> ();

		// CPO TRXTBH [2, 3] 1.0
		double[,] TRXTBH = new double[2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				TRXTBH[i1,i2] = 1.0;
		// CPO FCPPAV [2, 6, 7] 1.0
		double[,,] FCPPAV = new double[2,2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				for ( int i3 = 0 ; i3 < 2 ; i3++ )
					FCPPAV[i1,i2,i3] = 1.0;
		// CPO QUIBFX [0, 1] 1.0
		double[,] QUIBFX = new double[2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				QUIBFX[i1,i2] = 1.0;
		// CPO HWIALF [1, 2, 4] 1.0
		double[,,] HWIALF = new double[2,2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				for ( int i3 = 0 ; i3 < 2 ; i3++ )
					HWIALF[i1,i2,i3] = 1.0;
		// CPO JDMAEU [2, 5, 6] 1.0
		double[,,] JDMAEU = new double[2,2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				for ( int i3 = 0 ; i3 < 2 ; i3++ )
					JDMAEU[i1,i2,i3] = 1.0;
		// CPO MVTHNR [2, 4, 5] 1.0
		double[,,] MVTHNR = new double[2,2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				for ( int i3 = 0 ; i3 < 2 ; i3++ )
					MVTHNR[i1,i2,i3] = 1.0;
		// MUC QUIBFX 0 [0, 1]
			for(int i0 = 0 ; i0 < 2; i0++ )
				for(int i1 = 0 ; i1 < 2; i1++ )
					QUIBFX[i1,i0] *= P0sachant[i0];
		// MUC QUIBFX 1 [0, 1]
			for(int i0 = 0 ; i0 < 2; i0++ )
				for(int i1 = 0 ; i1 < 2; i1++ )
					QUIBFX[i1,i0] *= P1sachant0[i0,i1];
		// MUC HWIALF 2 [1, 2, 4]
			for(int i0 = 0 ; i0 < 2; i0++ )
				for(int i1 = 0 ; i1 < 2; i1++ )
					for(int i2 = 0 ; i2 < 2; i2++ )
						HWIALF[i2,i1,i0] *= P2sachant1_4[i0,i2,i1];
		// MUC TRXTBH 3 [2, 3]
			for(int i0 = 0 ; i0 < 2; i0++ )
				for(int i1 = 0 ; i1 < 2; i1++ )
					TRXTBH[i1,i0] *= P3sachant2[i0,i1];
		// MUC MVTHNR 4 [2, 4, 5]
			for(int i0 = 0 ; i0 < 2; i0++ )
				for(int i1 = 0 ; i1 < 2; i1++ )
					for(int i2 = 0 ; i2 < 2; i2++ )
						MVTHNR[i2,i1,i0] *= P4sachant5[i2,i1];
		// MUC JDMAEU 5 [2, 5, 6]
			for(int i0 = 0 ; i0 < 2; i0++ )
				for(int i1 = 0 ; i1 < 2; i1++ )
					for(int i2 = 0 ; i2 < 2; i2++ )
						JDMAEU[i2,i1,i0] *= P5sachant[i1];
		// MUC JDMAEU 6 [2, 5, 6]
			for(int i0 = 0 ; i0 < 2; i0++ )
				for(int i1 = 0 ; i1 < 2; i1++ )
					for(int i2 = 0 ; i2 < 2; i2++ )
						JDMAEU[i2,i1,i0] *= P6sachant5[i1,i2];
		// MUC FCPPAV 7 [2, 6, 7]
			for(int i0 = 0 ; i0 < 2; i0++ )
				for(int i1 = 0 ; i1 < 2; i1++ )
					for(int i2 = 0 ; i2 < 2; i2++ )
						FCPPAV[i2,i1,i0] *= P7sachant6_2[i0,i1,i2];
		// ASE smoking? EV_5 0 evs.get([5, 'smoking?'][1])
		double[] EV_5= evs["smoking?"];
		// ASE positive_XraY? EV_3 0 evs.get([3, 'positive_XraY?'][1])
		double[] EV_3= evs["positive_XraY?"];
		// ASE visit_to_Asia? EV_0 0 evs.get([0, 'visit_to_Asia?'][1])
		double[] EV_0= evs["visit_to_Asia?"];
		// ASE dyspnoea? EV_7 0 evs.get([7, 'dyspnoea?'][1])
		double[] EV_7= evs["dyspnoea?"];
		// MUL QUIBFX EV_0 [0, 1] ['0']
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					QUIBFX[i1,i0] *= EV_0[i0];
		// MUL TRXTBH EV_3 [2, 3] ['3']
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					TRXTBH[i1,i0] *= EV_3[i1];
		// MUL JDMAEU EV_5 [2, 5, 6] ['5']
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						JDMAEU[i2,i1,i0] *= EV_5[i1];
		// MUL FCPPAV EV_7 [2, 6, 7] ['7']
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						FCPPAV[i2,i1,i0] *= EV_7[i2];
		// CPO XAKXFL [2, 5, 6] 1.0
		double[,,] XAKXFL = new double[2,2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				for ( int i3 = 0 ; i3 < 2 ; i3++ )
					XAKXFL[i1,i2,i3] = 1.0;
		// MUL XAKXFL JDMAEU [2, 5, 6] [2, 5, 6]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						XAKXFL[i2,i1,i0] *= JDMAEU[i2,i1,i0];
		// CPO IFVUXZ [2, 4, 5] 1.0
		double[,,] IFVUXZ = new double[2,2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				for ( int i3 = 0 ; i3 < 2 ; i3++ )
					IFVUXZ[i1,i2,i3] = 1.0;
		// MUL IFVUXZ MVTHNR [2, 4, 5] [2, 4, 5]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						IFVUXZ[i2,i1,i0] *= MVTHNR[i2,i1,i0];
		// CPO JZGJUB [2, 4, 5] 1.0
		double[,,] JZGJUB = new double[2,2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				for ( int i3 = 0 ; i3 < 2 ; i3++ )
					JZGJUB[i1,i2,i3] = 1.0;
		// MUL JZGJUB MVTHNR [2, 4, 5] [2, 4, 5]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						JZGJUB[i2,i1,i0] *= MVTHNR[i2,i1,i0];
		// CPO IPEKWS [1, 2, 4] 1.0
		double[,,] IPEKWS = new double[2,2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				for ( int i3 = 0 ; i3 < 2 ; i3++ )
					IPEKWS[i1,i2,i3] = 1.0;
		// MUL IPEKWS HWIALF [1, 2, 4] [1, 2, 4]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						IPEKWS[i2,i1,i0] *= HWIALF[i2,i1,i0];
		// CPO ZLCECG [2, 6] 0.0
		double[,] ZLCECG = new double[2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				ZLCECG[i1,i2] = 0.0;
		// MAR ZLCECG FCPPAV [2, 6] [2, 6, 7]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int j0=0;j0<2;j0++)
						ZLCECG[i1,i0] += FCPPAV[j0,i1,i0];
		// MUL JDMAEU ZLCECG [2, 5, 6] [2, 6]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						JDMAEU[i2,i1,i0] *= ZLCECG[i2,i0];
		// CPO KNMSKT [1] 0.0
		double[] KNMSKT = new double[2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			KNMSKT[i1] = 0.0;
		// MAR KNMSKT QUIBFX [1] [0, 1]
			for(int i0=0;i0<2;i0++)
				for(int j0=0;j0<2;j0++)
					KNMSKT[i0] += QUIBFX[i0,j0];
		// MUL HWIALF KNMSKT [1, 2, 4] [1]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						HWIALF[i2,i1,i0] *= KNMSKT[i0];
		// CPO KIKCCO [2, 4] 0.0
		double[,] KIKCCO = new double[2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				KIKCCO[i1,i2] = 0.0;
		// MAR KIKCCO HWIALF [2, 4] [1, 2, 4]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int j0=0;j0<2;j0++)
						KIKCCO[i1,i0] += HWIALF[i1,i0,j0];
		// MUL MVTHNR KIKCCO [2, 4, 5] [2, 4]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						MVTHNR[i2,i1,i0] *= KIKCCO[i1,i0];
		// CPO KVWKZF [2, 5] 0.0
		double[,] KVWKZF = new double[2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				KVWKZF[i1,i2] = 0.0;
		// MAR KVWKZF MVTHNR [2, 5] [2, 4, 5]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int j0=0;j0<2;j0++)
						KVWKZF[i1,i0] += MVTHNR[i1,j0,i0];
		// MUL JDMAEU KVWKZF [2, 5, 6] [2, 5]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						JDMAEU[i2,i1,i0] *= KVWKZF[i1,i0];
		// CPO ZXLINC [2] 0.0
		double[] ZXLINC = new double[2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			ZXLINC[i1] = 0.0;
		// MAR ZXLINC TRXTBH [2] [2, 3]
			for(int i0=0;i0<2;i0++)
				for(int j0=0;j0<2;j0++)
					ZXLINC[i0] += TRXTBH[j0,i0];
		// MUL JDMAEU ZXLINC [2, 5, 6] [2]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						JDMAEU[i2,i1,i0] *= ZXLINC[i0];
		// MUL XAKXFL ZLCECG [2, 5, 6] [2, 6]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						XAKXFL[i2,i1,i0] *= ZLCECG[i2,i0];
		// MUL XAKXFL ZXLINC [2, 5, 6] [2]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						XAKXFL[i2,i1,i0] *= ZXLINC[i0];
		// CPO NBUOFU [2, 5] 0.0
		double[,] NBUOFU = new double[2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				NBUOFU[i1,i2] = 0.0;
		// MAR NBUOFU XAKXFL [2, 5] [2, 5, 6]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int j0=0;j0<2;j0++)
						NBUOFU[i1,i0] += XAKXFL[j0,i1,i0];
		// MUL IFVUXZ NBUOFU [2, 4, 5] [2, 5]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						IFVUXZ[i2,i1,i0] *= NBUOFU[i2,i0];
		// CPO ZMUCOI [2, 4] 0.0
		double[,] ZMUCOI = new double[2,2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			for ( int i2 = 0 ; i2 < 2 ; i2++ )
				ZMUCOI[i1,i2] = 0.0;
		// MAR ZMUCOI IFVUXZ [2, 4] [2, 4, 5]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int j0=0;j0<2;j0++)
						ZMUCOI[i1,i0] += IFVUXZ[j0,i1,i0];
		// MUL JZGJUB KIKCCO [2, 4, 5] [2, 4]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						JZGJUB[i2,i1,i0] *= KIKCCO[i1,i0];
		// MUL JZGJUB NBUOFU [2, 4, 5] [2, 5]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						JZGJUB[i2,i1,i0] *= NBUOFU[i2,i0];
		// MUL IPEKWS ZMUCOI [1, 2, 4] [2, 4]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						IPEKWS[i2,i1,i0] *= ZMUCOI[i2,i1];
		// MUL IPEKWS KNMSKT [1, 2, 4] [1]
			for(int i0=0;i0<2;i0++)
				for(int i1=0;i1<2;i1++)
					for(int i2=0;i2<2;i2++)
						IPEKWS[i2,i1,i0] *= KNMSKT[i0];
		// CPO P_6 ['6'] 0.0
		double[] P_6 = new double[2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			P_6[i1] = 0.0;
		// MAR P_6 JDMAEU [6] [2, 5, 6]
			for(int i0=0;i0<2;i0++)
				for(int j0=0;j0<2;j0++)
					for(int j1=0;j1<2;j1++)
						P_6[i0] += JDMAEU[i0,j1,j0];
		// NOR P_6 bronchitis?
			sum=0.0;
			for(int i0=0;i0<P_6.Length;i0++)
				sum+=P_6[i0];
			for(int i0=0;i0<P_6.Length;i0++)
				P_6[i0]/=sum;
			res.Add ("bronchitis?", P_6);
		// CPO P_4 ['4'] 0.0
		double[] P_4 = new double[2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			P_4[i1] = 0.0;
		// MAR P_4 JZGJUB [4] [2, 4, 5]
			for(int i0=0;i0<2;i0++)
				for(int j0=0;j0<2;j0++)
					for(int j1=0;j1<2;j1++)
						P_4[i0] += JZGJUB[j1,i0,j0];
		// NOR P_4 lung_cancer?
			sum=0.0;
			for(int i0=0;i0<P_4.Length;i0++)
				sum+=P_4[i0];
			for(int i0=0;i0<P_4.Length;i0++)
				P_4[i0]/=sum;
			res.Add ("lung_cancer?", P_4);
		// CPO P_1 ['1'] 0.0
		double[] P_1 = new double[2];
		for ( int i1 = 0 ; i1 < 2 ; i1++ )
			P_1[i1] = 0.0;
		// MAR P_1 IPEKWS [1] [1, 2, 4]
			for(int i0=0;i0<2;i0++)
				for(int j0=0;j0<2;j0++)
					for(int j1=0;j1<2;j1++)
						P_1[i0] += IPEKWS[j1,j0,i0];
		// NOR P_1 tuberculosis?
			sum=0.0;
			for(int i0=0;i0<P_1.Length;i0++)
				sum+=P_1[i0];
			for(int i0=0;i0<P_1.Length;i0++)
				P_1[i0]/=sum;
			res.Add ("tuberculosis?",P_1);

			return res;
		}
}


//
// Sample code using this function
//

