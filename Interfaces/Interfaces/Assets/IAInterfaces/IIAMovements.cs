using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
	public abstract class IIAMovements
	{
		/**
		 * Garde une référence sur le Component Movement du tank
		 */
		protected Movement m_TankMovement;

		/**
		 * Récupère un lien vers le Component Shooting du tank
		 */
		virtual public void setTankMovement (Movement tankMovement) { return; }
		virtual public void setTankMovement (Movement tankMovement, Rigidbody ennemy) { return; }

		/**
		 * Détermine si le Tank doit avancer, reculer, ou non
		 * ---
		 * Return
		 * • -1 : Reculer
		 * •  0 : Ne pas bouger
		 * •  1 : Avancer
		 */
		virtual public int Move() { return 0; }
		virtual public int Move(Dictionary<string,double[]> obs) { return 0; }

		/**
		 * Détermine si le Tank doit tourner ou non
		 * ---
		 * Return
		 * • -1 : Tourner à droite
		 * •  0 : Ne pas tourner
		 * •  1 : Tourner à gauche
		 */
		virtual public int Turn() { return 0; }
		virtual public int Turn(Dictionary<string,double[]> obs) { return 0; }

	}
}

