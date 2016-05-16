using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
	public abstract class IIAShooting
	{
		/**
		 * Garde une référence sur le Component Shooting du tank
		 */
		protected Shooting m_TankShooting;

		/**
		 * Récupère un lien vers le Component Shooting du tank
		 */
		virtual public void setTankMovement (Shooting tankShooting) { return; }
		virtual public void setTankMovement (Shooting tankShooting, Rigidbody ennemy) { return; }

		/**
		 * Détermine si le Tank doit charger, tirer, ou non un missile
		 * ---
		 * Return
		 * • 0 : Aucune auction
		 * • 1 : Charger le tire
		 * • 2 : Tirer
		 */
		virtual public int Fire() { return 0; }
		virtual public int Fire(Dictionary<string,double[]> obs) { return 0; }
	}
}

