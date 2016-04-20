using UnityEngine;
using System.Collections;

using Interfaces;
using System;
using System.IO;
using System.Reflection;

public class LoggerManager : MonoBehaviour {
	/**
	 * --- Identitée du tank à suivre
	 * Player Number      : Numéro (id) du tank
	 * Instance           : Référence sur le logger
	 * 
	 * --- Logger
	 * Logger             : Référence sur le logger, qui sera chargé à la volée
	 * 						à partir d'un module .dll
	 */
	[HideInInspector] public int m_PlayerNumber = 1;
	[HideInInspector] public GameObject m_Instance;

	private ILog m_Logger;
	private string m_PathLogger = @"" + ScenesParameters.m_PathLogger;

	// Use this for initialization
	public void Setup () {
		//m_Logger = m_Instance.GetComponent<ILogger> ();
		LoadLogger();
		m_Logger.m_PlayerNumber = m_PlayerNumber;
	}

	private void LoadLogger() {
		string file = ScenesParameters.m_Logger;
		string relativePath = String.Format ("{0}{2}", m_PathLogger, Path.DirectorySeparatorChar, file);

		//Charge l'assembly
		Assembly assembly = Assembly.LoadFile (relativePath);

		// Récupérer et instancier la class IIAMovements
		foreach ( Type module in assembly.GetTypes() ) {
			if (module.BaseType == typeof(ILog))
				m_Logger = (ILog) Activator.CreateInstance (module);
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
		m_Logger.Write ();
	}

	public void DisableControl()
	{
		m_Logger.enabled = false;
	}


	public void EnableControl()
	{
		m_Logger.enabled = true;
	}


	public void Reset()
	{
		//m_Instance.SetActive(false);
		m_Logger.Reset ();
		//m_Instance.SetActive(true);
	}

}
