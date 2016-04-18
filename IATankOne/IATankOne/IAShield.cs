using System;
using UnityEngine;

using Interfaces;

namespace IATankOne
{
	public class IAShield : IIAShield
	{
		Shield m_TankShield;
		Rigidbody m_RigidBody;

		private int count = 0;

		public IAShield ()
		{
		}

		override public void setTankMovement (Shield tankShield) {
			m_TankShield = tankShield;
			m_RigidBody = m_TankShield.GetComponent<Rigidbody> ();
		}

		override public bool Activate() {
			if (count >= 100) {
				count = 0;
				return true;
			}
			count++;

			return false;
		}
	}
}

