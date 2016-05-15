using System;
using Interfaces;
using UnityEngine;
using System.Collections.Generic;

namespace Many
{
	public class IAMovements : IIAMovements
	{
		Movement m_TankMovement;
		Rigidbody m_RigidBody;
		public Many m_Many;

		public IAMovements(Many many) {
			m_Many = many;
		}

		override public void setTankMovement (Movement tankMovement) {
			m_TankMovement = tankMovement;
			m_RigidBody = m_TankMovement.GetComponent<Rigidbody> ();
		}

		override public int Move(Dictionary<string,double[]> obs) {
			Dictionary<string, double[]> probas = m_Many.getObs(obs, Time.frameCount);
			return m_Many.chooseValue (probas ("move?"));
		}

		override public int Turn(Dictionary<string,double[]> obs) {
			Dictionary<string, double[]> probas = m_Many.getObs(obs, Time.frameCount);
			return m_Many.chooseValue (probas ("turn?"));
		}
	}
}

