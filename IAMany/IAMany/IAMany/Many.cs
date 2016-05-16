using System;

using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Many
{
	public class Many : IATank
	{

		private Dictionary<string, int[]> values = new Dictionary<string, int[]> ()  {
			{"move?", new int[]{1, 0, -1}},
			{"turn?", new int[]{1, 0, -1}},
			{"shield?", new int[]{0, 1}},
			{"shell?", new int[]{0, 1, 2}}
		};

													

		private ProbaMany m_ProbaMany = new ProbaMany();

		private Dictionary<string, double[]> m_Probas = null;
		private int m_FrameCounter = -1;
		private Observe observe = new Observe ();

		public Many() {
			m_IAMovement = new IAMovements (this);
			m_IAShooting = new IAShooting (this);
			m_IAShield = new IAShield (this);
		}

		public int chooseValue(double[] obs, string action) {
			System.Random random = new System.Random();
			double rand = random.NextDouble ();
			int i = 0;
			for ( i = 0 ; i < obs.Length ; i++) {
				Debug.Log (obs[i]);
				if (rand < obs[i]) {
					return values[action][i];
				}
			}
			return values[action][i];
		}

		override public void setMask(LayerMask collisionMask, LayerMask shellMask)
		{
			observe.setMask (collisionMask, shellMask);
		}

		override public void setTanks (Rigidbody allie, Rigidbody ennemy) {
			observe.setTanks (allie, ennemy);
		}

		public Dictionary<string, double[]> getObs(int frameCounter) {
			if (m_Probas == null || m_FrameCounter < frameCounter) {
				Dictionary<string,double[]> obs;
				obs = observe.Observation ();
				m_Probas = m_ProbaMany.getProbasIA (obs);
				Debug.Log ("Timon");
				Debug.Log (m_Probas["move?"][0]);
				Debug.Log (m_Probas["move?"][1]);
				Debug.Log (m_Probas["move?"][2]);
				m_FrameCounter = frameCounter;
			}
			return m_Probas;			
		}


	}
}

