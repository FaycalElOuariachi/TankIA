using UnityEngine;
using System.Collections;
using System.IO;

public class ScenesParameters {
	static public bool m_HasRecorder = true;
	static public int m_GameNumber = -1;
	static public string m_GameName = "";
	//static public string[] m_IATanks = {"IATankTwo.dll", "IATankTwo.dll"};
	static public string[] m_IATanks = {"", ""};
	static public string m_Logger = "Log";
	static public string m_PathLogger = @"." + Path.AltDirectorySeparatorChar + "Assets" + Path.AltDirectorySeparatorChar + "Library" + Path.AltDirectorySeparatorChar + "loggers";
}
