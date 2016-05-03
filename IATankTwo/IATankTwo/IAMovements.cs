using System;
using UnityEngine;

using Interfaces;

namespace IATankTwo
{
	public class IAMovements : IIAMovements
	{
		Movement m_TankMovement;
		Rigidbody m_RigidBody;

		public IAMovements() {}

		override public void setTankMovement (Movement tankMovement) {
			m_TankMovement = tankMovement;
			m_RigidBody = m_TankMovement.GetComponent<Rigidbody> ();
		}

		override public int Move() {
			Vector3 movement = m_TankMovement.transform.forward * 0.2f;
			m_RigidBody.MovePosition (m_RigidBody.position + movement);
			return 0;
		}

		override public int Turn() {
			float turn = 10f;

			Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

			m_RigidBody.MoveRotation (m_RigidBody.rotation * turnRotation);

			return 0;
		}
	}
}

