﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Reflection;
using Interfaces;
using System;

public class Logger : MonoBehaviour {
	[HideInInspector] public int m_TimeReference;
	public int m_PlayerNumber = 1;


	[HideInInspector] public ILog m_Logger;
	private string m_PathLogger = ScenesParameters.m_PathLogger;
	private string m_PathLog = ScenesParameters.m_PathLogger + Path.AltDirectorySeparatorChar + "dir_" + ScenesParameters.m_Logger;

	// Use this for initialization
	public void Setup () {
		LoadLogger ();
		m_Logger.m_PlayerNumber = m_PlayerNumber;
		m_Logger.m_TimeReference = m_TimeReference;
		m_Logger.m_PathLog = m_PathLog;
		m_Logger.Setup ();
	}

	public void setMask(LayerMask collisionMask, LayerMask shellMask) {
		m_Logger.setMask (collisionMask, shellMask);
	}

	private void LoadLogger() {
		string file = ScenesParameters.m_Logger + ".dll";
		//string relativePath = String.Format ("{0}{2}", m_PathLogger, Path.AltDirectorySeparatorChar + "file");
		string relativePath = m_PathLogger +  Path.AltDirectorySeparatorChar + file;
		//Debug.Log (relativePath);

		//Charge l'assembly
		Assembly assembly = Assembly.LoadFile (relativePath);

		// Récupérer et instancier la class ILog
		foreach ( Type module in assembly.GetTypes() ) {
			if (module.BaseType == typeof(ILog))
				m_Logger = (ILog) Activator.CreateInstance (module);
		}
	}
	
	// Update is called once per frame
	void Update () {
		m_Logger.captureFrame ();
	}

	public void SetTank(ITankManager m_TankManager, ITankManager m_ennemy) {
		m_Logger.setTank (m_TankManager, m_ennemy);
	}

	public void WriteLog() {
		m_Logger.WriteASCII ();
		m_Logger.Write ();
	}

	public void Reset() {
		m_Logger.Reset ();
	}

	public void setTimeReference(int time) {
		m_Logger.setTimeReference (time);
	}
}
