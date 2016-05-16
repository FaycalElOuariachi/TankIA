using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
	public abstract class IIAShield
	{
		/**
		 * Garde une référence sur le Component Shooting du tank
		 */
		protected Shield m_TankShield;

		/**
		 * Récupère un lien vers le Component Shooting du tank
		 */
		virtual public void setTankMovement (Shield tankShield) { return; }
		virtual public void setTankMovement (Shield tankShield, Rigidbody ennemy) { return; }

		/**
		 * Détermine si le Tank doit activer ou non son bouclier
		 * ---
		 * Return
		 * • 1  : Activer le bouclier (si la fonctionnalité n'est pas
		 * 			 disponible, le tank ne l'activera pasà
		 * • 0 : Ne pas activer le bouclier
		 */
		virtual public int Activate() { return 0; }
		virtual public int Activate(Dictionary<string,double[]> obs) { return 0; }
	}
}

