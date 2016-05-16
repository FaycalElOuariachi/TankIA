using System;
using Interfaces;
using UnityEngine;
using System.Collections.Generic;

namespace Many
{
	public class IAMovements : IIAMovements
	{
		Rigidbody m_RigidBody;
		Rigidbody m_Ennemy;
		public Many m_Many;

		public IAMovements(Many many) {
			m_Many = many;
		}

		override public void setTankMovement (Movement tankMovement, Rigidbody ennemy) {
			m_TankMovement = tankMovement;
			m_RigidBody = m_TankMovement.GetComponent<Rigidbody> ();
			m_Ennemy = ennemy;
		}

		override public int Move() {
			Dictionary<string, double[]> probas = m_Many.getObs(Time.frameCount);
			return m_Many.chooseValue (probas ["move?"], "move?");
		}

		override public int Turn() {
			Dictionary<string, double[]> probas = m_Many.getObs(Time.frameCount);
			return m_Many.chooseValue (probas ["turn?"], "turn?");
		}
	}
}

