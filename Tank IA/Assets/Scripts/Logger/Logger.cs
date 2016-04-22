using UnityEngine;
using System.Collections;
using System.IO;
using System.Reflection;
using Interfaces;
using System;

public class Logger : MonoBehaviour {

	private ILog m_Logger;
	public int m_PlayerNumber = 1;
	private string m_PathLogger = ScenesParameters.m_PathLogger;
	private string m_PathLog = ScenesParameters.m_PathLogger + Path.AltDirectorySeparatorChar + "dir_" + ScenesParameters.m_Logger;

	// Use this for initialization
	public void Setup () {
		LoadLogger ();
		Debug.Log("Timon & ");
		m_Logger.m_PlayerNumber = m_PlayerNumber;
		m_Logger.m_PathLog = m_PathLog;
		m_Logger.Setup ();
		Debug.Log("Pumba");
	}

	private void LoadLogger() {
		string file = ScenesParameters.m_Logger;
		//string relativePath = String.Format ("{0}{2}", m_PathLogger, Path.AltDirectorySeparatorChar + "file");
		string relativePath = m_PathLogger +  Path.AltDirectorySeparatorChar + file;
		//Debug.Log (relativePath);

		//Charge l'assembly
		Assembly assembly = Assembly.LoadFile (relativePath);

		// Récupérer et instancier la class IIAMovements
		foreach ( Type module in assembly.GetTypes() ) {
			if (module.BaseType == typeof(ILog))
				m_Logger = (ILog) Activator.CreateInstance (module);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Timon et ");
		m_Logger.Reset ();
		Debug.Log ("Pumba");
		m_Logger.captureFrame ();
	}

	public void SetTank(ITankManager m_TankManager, ITankManager m_ennemy) {
		m_Logger.setTank (m_TankManager, m_ennemy);
	}

	public void WriteLog() {
		Debug.Log ("NON ---------------------------");
		m_Logger.WriteASCII ();
		Debug.Log ("OUI ---------------------------");
		m_Logger.Write ();
	}

	public void Reset() {
		m_Logger.Reset ();
	}
}
