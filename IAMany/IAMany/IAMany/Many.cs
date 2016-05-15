using System;

using Interfaces;
using System.Collections.Generic;

namespace Many
{
	public class Many : IATank
	{
		private ProbaMany m_ProbaMany = new ProbaMany();

		private Dictionary<string, double[]> m_Probas = null;
		private int m_FrameCounter = -1;

		public Many() {
			m_IAMovement = new IAMovements (this);
			m_IAShooting = new IAShooting (this);
			m_IAShield = new IAShield (this);
		}

		public int chooseValue(double[] obs) {
			Random random = new Random();
			double rand = random.NextDouble ();
			int i;
			for ( i = 0 ; i < obs.Length ; i++) {
				if (rand < obs[i]) {
					return i;
				}
			}
			return i;
		}

		public Dictionary<string, double[]> getObs(Dictionary<string,double[]> obs, int frameCounter) {
			if (m_Probas == null || m_FrameCounter < m_FrameCounter) {
				m_Probas = m_ProbaMany.getProbasIA (obs);
				m_FrameCounter = frameCounter;
			}
			return m_Probas;			
		}


	}
}

