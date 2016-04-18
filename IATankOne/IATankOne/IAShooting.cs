using System;
using UnityEngine;
using UnityEngine.UI;

using Interfaces;

namespace IATankTwo
{
	public class IAShooting : IIAShooting
	{
		Shooting m_TankShooting;
		Rigidbody m_RigidBody;

		private int count = 0;

		public IAShooting () {
		}

		override public void setTankMovement (Shooting tankShooting) {
			m_TankShooting = tankShooting;
			m_RigidBody = m_TankShooting.GetComponent<Rigidbody> ();
			m_TankShooting.setCurrentLaunchForce(m_TankShooting.m_MinLaunchForce);
		}

		/**
		 * Détermine si le Tank doit charger, tirer, ou non un missile
		 * ---
		 * Return
		 * • 0 : Aucune action
		 * • 1 : Charger le tire
		 * • 2 : Tirer
		 */
		override public int Fire() {
			if (count >= 20) {
				count = 0;
				return 2;
			}
			count++;

			return 0;
		}
	}
}
