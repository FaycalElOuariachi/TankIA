using System;
using UnityEngine;

using Interfaces;
using System.Collections.Generic;

namespace Many
{
	public class IAShield : IIAShield
	{
		Rigidbody m_RigidBody;
		public Many m_Many;

		public IAShield (Many many) {
			m_Many = many;
		}

		override public void setTankMovement (Shield tankShield) {
			m_TankShield = tankShield;
			m_RigidBody = m_TankShield.GetComponent<Rigidbody> ();
		}

		override public int Activate() {
			Dictionary<string, double[]> probas = m_Many.getObs(Time.frameCount);
			return m_Many.chooseValue (probas ["shield?"], "shield?");
		}
	}
}

