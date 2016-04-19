using System;

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

		/**
		 * Détermine si le Tank doit activer ou non son bouclier
		 * ---
		 * Return
		 * • true  : Activer le bouclier (si la fonctionnalité n'est pas
		 * 			 disponible, le tank ne l'activera pasà
		 * • false : Ne pas activer le bouclier
		 */
		virtual public bool Activate() { return false; }
	}
}

