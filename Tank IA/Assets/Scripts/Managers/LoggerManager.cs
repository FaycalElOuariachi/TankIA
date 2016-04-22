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
	public int m_PlayerNumber = 1;
	//public GameObject m_LoggerPrefab;
	public GameObject m_Instance;

	private Logger m_Logger;

	// Use this for initialization
	public void Setup () {
		m_Logger = m_Instance.GetComponent<Logger> ();
		m_Logger.m_PlayerNumber = m_PlayerNumber;
		m_Logger.Setup ();
	}

	public void SetTank(ITankManager m_TankManager, ITankManager m_ennemy) {
		m_Logger.SetTank (m_TankManager, m_ennemy);
	}

	public void WriteLog() {
		m_Logger.WriteLog ();
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
		m_Instance.SetActive(false);
		m_Logger.Reset ();
		m_Instance.SetActive(true);
	}

}
