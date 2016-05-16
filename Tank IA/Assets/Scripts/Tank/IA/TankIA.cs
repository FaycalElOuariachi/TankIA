using System;
using System.IO;
using System.Reflection;

using Interfaces;
using UnityEngine;

public class TankIA
{
	public int m_PlayerNumber = 1;
	public IATank m_IATank;
	private string m_IAPath = ScenesParameters.m_IAPath;

	public TankIA() {
		string file = ScenesParameters.m_IATanks[m_PlayerNumber-1];
		string relativePath = String.Format ("{0}{2}", m_IAPath, Path.DirectorySeparatorChar, file);
		Debug.Log (relativePath);

		//Charge l'assembly
		Assembly assembly = Assembly.LoadFile (relativePath);

		// Récupérer et instancier la class IIAMovements
		foreach ( Type module in assembly.GetTypes() ) {
			if (module.BaseType == typeof(IATank))
				m_IATank = (IATank) Activator.CreateInstance (module);
		}
	}

	public void setMask(LayerMask collisionMask, LayerMask shellMask) {
		m_IATank.setMask (collisionMask, shellMask);
	}

	public void setTanks (ITankManager allie, ITankManager ennemy) {
		Debug.Log ("123");
		Debug.Log (allie.m_Instance.GetComponent<Rigidbody>());
		Debug.Log (ennemy.m_Instance.GetComponent<Rigidbody>());
		m_IATank.setTanks (allie.m_Instance.GetComponent<Rigidbody>(), ennemy.m_Instance.GetComponent<Rigidbody>());
	}
}

