using System;
using UnityEngine;

using Interfaces;
using System.Collections.Generic;

namespace Many
{
	public class IAShield : IIAShield
	{
		Shield m_TankShield;
		Rigidbody m_RigidBody;
		public Many m_Many;

		private int count = 0;

		public IAShield (Many many) {
			m_Many = many;
		}

		override public void setTankMovement (Shield tankShield) {
			m_TankShield = tankShield;
			m_RigidBody = m_TankShield.GetComponent<Rigidbody> ();
		}

		override public int Activate(Dictionary<string,double[]> obs) {
			Dictionary<string, double[]> probas = m_Many.getObs(obs, Time.frameCount);
			return m_Many.chooseValue (probas ("shield?"));
		}
	}
}

