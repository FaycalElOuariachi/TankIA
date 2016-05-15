using System;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReplayManager: MonoBehaviour
{
	public string m_DirName;
	public string m_Logger;
	public int m_MaxReplay = 1;
	public GameObject m_PrefabGameManager;
	public Transform[] m_SpawnPoint;

	public CameraControl m_CameraControl;   
	public Text m_MessageText; 

	private string m_RecapFileName = "LogDone.lock";
	private GameManager m_GameManager;

	private int m_Counter = 0;
	private bool m_Go = true;
	private int m_CountGames = 0;
	private string[] directories;


	public void Start() {
		directories = Directory.GetDirectories(m_DirName);
	}

	public void Setup(string logger) {
		m_Logger = logger;
	}

	public void Update() {
		//if (m_Go) {
		if (m_Counter < m_MaxReplay && m_CountGames < directories.Length) {
			FindReplay ();
		}

		if (m_Go && m_CountGames >= directories.Length) {
			//SceneManager.LoadScene(0);
		}

	}

	private void FindReplay() {

		/*for (int i = 0 ; i < directories.Length ; i++ ) {
		//foreach (string replay in directories) {
			string replay = directories[i];
			if (m_Counter >= m_MaxReplay) {
				m_Go = false;
				return;
			}
			
			if (!isLogged (replay)) {
				ScenesParameters.m_Logger = m_Logger;
				ScenesParameters.m_HasRecorder = false;
				m_GameManager = (Instantiate (m_PrefabGameManager, new Vector3(0f,0f,0f), new Quaternion(0f,0f,0f,0f)) as GameObject).GetComponent<GameManager>();

				m_GameManager.m_MaskOn = true;

				m_GameManager.m_CameraControl = m_CameraControl;
				m_GameManager.m_MessageText = m_MessageText;

				m_GameManager.m_Tanks [0].m_SpawnPoint = m_SpawnPoint [0];
				m_GameManager.m_Tanks [1].m_SpawnPoint = m_SpawnPoint [1];

				m_GameManager.setReplayManager (this);
				m_GameManager.m_GameName = replay;
				m_Counter++;

				directories [i] = null;
				return;
			}
		}*/

		string replay = directories[m_CountGames];
		m_CountGames++;
		/*if (m_Counter >= m_MaxReplay) {
				m_Go = false;
				return;
			}*/
		if (!isLogged (replay)) {
			ScenesParameters.m_Logger = m_Logger;
			ScenesParameters.m_HasRecorder = false;
			m_GameManager = (Instantiate (m_PrefabGameManager, new Vector3(0f,0f,0f), new Quaternion(0f,0f,0f,0f)) as GameObject).GetComponent<GameManager>();

			m_GameManager.m_MaskOn = true;

			m_GameManager.m_CameraControl = m_CameraControl;
			m_GameManager.m_MessageText = m_MessageText;

			m_GameManager.m_Tanks [0].m_SpawnPoint = m_SpawnPoint [0];
			m_GameManager.m_Tanks [1].m_SpawnPoint = m_SpawnPoint [1];

			m_GameManager.setReplayManager (this);
			m_GameManager.m_GameName = replay;
			m_Counter++;

			directories [m_CountGames - 1] = null;
			return;
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

