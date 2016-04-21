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
		m_Logger.m_PlayerNumber = m_PlayerNumber;
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
			if (module.BaseType == typeof(ILog)) {
				//m_Logger = m_Instance.AddComponent<module> ();
				m_Logger = (ILog) Activator.CreateInstance (module);
				m_Logger.captureFrame ();
			}
			//m_Instance = (ILog) Activator.CreateInstance (module);
		}
	}
	
	// Update is called once per frame
	void Update () {
		m_Logger.captureFrame ();
	}

	public void SetTank(ITankManager m_TankManager) {
		m_Logger.setTank (m_TankManager);
	}

	public void WriteLog() {
		m_Logger.WriteASCII ();
		m_Logger.Write ();
	}

	public void Reset() {
		Debug.Log ("Timon");
		//m_Logger.Reset ();
		Debug.Log ("Pumba");
	}
}
