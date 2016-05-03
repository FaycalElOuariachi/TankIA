using System;
using UnityEngine;

using Interfaces;

// xcopy "$(TargetPath)" "G:\Work\University\M1 Androïde\Semestre 2\P-ANDROIDE\Océan\Tank IA\Assets\Library"

namespace IATankOne
{
	public class IAMovements : IIAMovements
	{
		Movement m_TankMovement;
		Rigidbody m_RigidBody;

		private int count = 0;

		public IAMovements () {}

		override public void setTankMovement (Movement tankMovement) {
			m_TankMovement = tankMovement;
			m_RigidBody = m_TankMovement.GetComponent<Rigidbody> ();
		}

		override public int Move() {
			if (count < 25) {
				Vector3 movement = m_TankMovement.transform.forward * 0.1f;
				m_RigidBody.MovePosition (m_RigidBody.position + movement);
			}

			count++;
			if (count > 50)
				count = 0;

			return 0;
		}

		override public int Turn() {
			if (count >= 25) {
				float turn = 3.6f;// Time.deltaTime;

				Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

				m_RigidBody.MoveRotation (m_RigidBody.rotation * turnRotation);
			}
			return 0;
		}
	}
}

