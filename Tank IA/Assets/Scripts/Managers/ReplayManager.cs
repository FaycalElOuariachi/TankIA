using System;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ReplayManager: MonoBehaviour
{
	public string m_DirName;
	public string m_Logger;
	public GameObject m_PrefabGameManager;
	public Transform[] m_SpawnPoint;

	public CameraControl m_CameraControl;   
	public Text m_MessageText; 

	private string m_RecapFileName = "LogDone.lock";
	private GameManager m_GameManager;

	private int m_Counter = 0;
	private int m_MaxReplay = 3;
	private bool m_Go = true;
	private string[] directories;


	public void Start() {
		directories = Directory.GetDirectories(m_DirName);
	}

	public void Setup(string logger) {
		m_Logger = logger;
	}

	public void Update() {
		if (m_Go) {
			FindReplay (directories);
		}
	}

	private void FindReplay(string[] directories) {

		for (int i = 0 ; i < directories.Length ; i++ ) {
		//foreach (string replay in directories) {
			string replay = directories[i];
			if (m_Counter >= m_MaxReplay) {
				Debug.Log (replay);
				m_Go = false;
				return;
			}
			
			if (!isLogged (replay)) {
				ScenesParameters.m_Logger = m_Logger;
				ScenesParameters.m_HasRecorder = false;
				m_GameManager = (Instantiate (m_PrefabGameManager, new Vector3(0f,0f,0f), new Quaternion(0f,0f,0f,0f)) as GameObject).GetComponent<GameManager>();

				m_GameManager.m_CameraControl = m_CameraControl;
				m_GameManager.m_MessageText = m_MessageText;

				m_GameManager.m_Tanks [0].m_SpawnPoint = m_SpawnPoint [0];
				m_GameManager.m_Tanks [1].m_SpawnPoint = m_SpawnPoint [1];

				m_GameManager.setReplayManager (this);
				m_GameManager.m_GameName = replay;
				m_Counter++;

				directories [i] = null;
			}
		}
	}

	private bool isLogged (string replay) {
		if (replay == null)
			return true;
		return File.Exists (replay + Path.AltDirectorySeparatorChar + m_RecapFileName);
	}

	public void setGo() {
		m_Counter--;
		if (m_Counter == 0)
			m_Go = true;
	}

}

