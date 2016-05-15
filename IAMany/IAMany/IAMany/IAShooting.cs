﻿using System;
using UnityEngine;

using Interfaces;
using System.Collections.Generic;

namespace Many
{
	public class IAShooting : IIAShooting
	{
		Shooting m_TankShooting;
		Rigidbody m_RigidBody;
		public Many m_Many;

		private int count = 0;

		public IAShooting (Many many) {
			m_Many = many;
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
		override public int Fire(Dictionary<string,double[]> obs) {
			Dictionary<string, double[]> probas = m_Many.getObs(obs, Time.frameCount);
			return m_Many.chooseValue (probas ("shell?"));
		}
	}
}