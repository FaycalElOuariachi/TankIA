using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

using Interfaces;

public class Recorder : MonoBehaviour {

	public string m_PathRecorder;
	[HideInInspector] public int m_PlayerNumber = 1;
	[HideInInspector] public TankManager m_TankManager;
	[HideInInspector] public ILog m_Log = null;
	[HideInInspector] public int m_TimeReference;

	private string m_MovementAxisName;
	private string m_TurnAxisName;
	private string m_FireButton;
	private string m_ShieldButton;
	private float m_MovementInputValue;
	private float m_TurnInputValue;
	private Health m_TankHealth;

	private List<int> m_ActionsTime = new List<int> ();
	private List<float> m_Actions = new List<float> ();
	private List<char> m_ActionsType = new List<char> ();

	private List<int> m_TransformsTime = new List<int> ();
	private List<Vector3> m_TransformsPosition = new List<Vector3> ();
	private List<Quaternion> m_TransformsRotation = new List<Quaternion> ();
	private List<int> m_HealthTime = new List<int> ();
	private List<float> m_Health = new List<float> ();

	//private float m_PreviousHealth = 100f;

	private int m_RoundCounter = 0;
	private int m_GameCounter;


	// Use this for initialization
	void Start () {
		m_MovementAxisName = "Vertical" + m_PlayerNumber;
		m_TurnAxisName = "Horizontal" + m_PlayerNumber;
		m_FireButton = "Fire" + m_PlayerNumber;
		m_ShieldButton = "Shield" + m_PlayerNumber;
		m_PathRecorder = "records";
		Directory.CreateDirectory(m_PathRecorder);
		m_GameCounter = Directory.GetDirectories (m_PathRecorder).Length + 1;
	}
	
	// Update is called once per frame
	void Update () {
		// Store the player's input of the player
		AddInputs();

		// Store the position and the rotation of the tank
		AddStates ();

		// TODO Store more informations
	}

	public void SetTankManager(TankManager tank) {
		m_TankManager = tank;
		m_TankHealth = tank.m_Instance.GetComponent<Health> ();
	}

	private void AddInputs() {
		m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis (m_TurnAxisName);


		// Add movements to log
		if (m_MovementInputValue != 0) {
			m_ActionsTime.Add (Time.frameCount - m_TimeReference);
			m_ActionsType.Add ('M');
			m_Actions.Add (m_MovementInputValue);
		}
		if (m_TurnInputValue != 0) {
			m_ActionsTime.Add (Time.frameCount - m_TimeReference);
			m_ActionsType.Add ('T');
			m_Actions.Add (m_TurnInputValue);
		}


		/**
		 * Add Firing state to log
		 * 0 : Button Down
		 * 1 : Button already Down, and still yet
		 * 2 : Button Up
		 */
		if (Input.GetButtonDown (m_FireButton)) {
			m_ActionsTime.Add (Time.frameCount - m_TimeReference);
			m_ActionsType.Add ('F');
			m_Actions.Add (0f);
		}
		else if (Input.GetButton (m_FireButton)) {
			m_ActionsTime.Add (Time.frameCount - m_TimeReference);
			m_ActionsType.Add ('F');
			m_Actions.Add (1f);
		}
		else if (Input.GetButtonUp (m_FireButton)) {
			m_ActionsTime.Add (Time.frameCount - m_TimeReference);
			m_ActionsType.Add ('F');
			m_Actions.Add (2f);
		}

		/**
		 * Add Activation of shield to log
		 * The value add to m_Actions isn't significant
		 */
		if (Input.GetButtonDown (m_ShieldButton)) {
			m_ActionsTime.Add (Time.frameCount - m_TimeReference);
			m_ActionsType.Add ('S');
			m_Actions.Add (0f);
		}
	}

	private void AddStates() {
		Vector3 pos = new Vector3 (m_TankManager.m_Instance.transform.position.x, m_TankManager.m_Instance.transform.position.y, m_TankManager.m_Instance.transform.position.z);
		Quaternion rot = new Quaternion (m_TankManager.m_Instance.transform.rotation.x, m_TankManager.m_Instance.transform.rotation.y, m_TankManager.m_Instance.transform.rotation.z,
			                 m_TankManager.m_Instance.transform.rotation.w);

		float currentHealth = m_TankHealth.getCurrentHealth ();

		//if (m_PreviousHealth != currentHealth) {
			m_Health.Add (currentHealth);
		m_HealthTime.Add (Time.frameCount - m_TimeReference);
			//m_PreviousHealth = currentHealth;
		//}

		/*if (m_TransformsPosition.Count != 0 && pos.Equals(m_TransformsPosition[m_TransformsPosition.Count-1]) && rot.Equals(m_TransformsRotation[m_TransformsPosition.Count-1])) {
			return;
		}*/
		m_TransformsTime.Add (Time.frameCount - m_TimeReference);
		m_TransformsPosition.Add (pos);
		m_TransformsRotation.Add (rot);
	}

	// Binary Action Writer
	public void WriteActions() {
		Directory.CreateDirectory(m_PathRecorder + "\\Game" + m_GameCounter);
		using (BinaryWriter file =
			new BinaryWriter (File.Open(m_PathRecorder + "\\Game" + m_GameCounter + "\\Round" + m_RoundCounter + "_" + m_PlayerNumber + ".IArec",FileMode.Create))) {
			int i;
			for ( i = 0 ; i < m_ActionsTime.Count ; i++) {
				file.Write (m_ActionsTime [i]);
				file.Write (m_ActionsType[i]);
				file.Write(m_Actions[i]);
			}
		}
	}

	// ASCII Action Writer
	public void WriteActionsASCII() {
		Directory.CreateDirectory(m_PathRecorder + "\\Game" + m_GameCounter);
		using (StreamWriter file =
			new StreamWriter (m_PathRecorder + "\\Game" + m_GameCounter + "\\Round" + m_RoundCounter + "_" + m_PlayerNumber + "ascii.IArec")) {
			int i;
			for ( i = 0 ; i < m_ActionsTime.Count ; i++) {
				file.WriteLine(m_ActionsTime[i].ToString() + " " + m_ActionsType[i] + " " + m_Actions[i].ToString());
			}
		}
	}

	// Binary Transform Writer
	public void WriteTransforms() {
		Directory.CreateDirectory(m_PathRecorder + "\\Game" + m_GameCounter);
		using (BinaryWriter file =
			new BinaryWriter (File.Open(m_PathRecorder + "\\Game" + m_GameCounter + "\\Round" + m_RoundCounter + "_" + m_PlayerNumber + "_Transforms.IArec",FileMode.Create))) {
			int i;
			for ( i = 0 ; i < m_TransformsPosition.Count ; i++) {
				file.Write (m_TransformsTime[i]);

				file.Write (m_TransformsPosition[i].x);
				file.Write (m_TransformsPosition[i].y);
				file.Write (m_TransformsPosition[i].z);

				file.Write (m_TransformsRotation[i].x);
				file.Write (m_TransformsRotation[i].y);
				file.Write (m_TransformsRotation[i].z);
				file.Write (m_TransformsRotation[i].w);
				file.Write (m_Health[i]);
			}
		}
	}

	// ASCII Transform Writer
	public void WriteTransformsASCII() {
		Directory.CreateDirectory(m_PathRecorder + "\\Game" + m_GameCounter);
		using (StreamWriter file =
			new StreamWriter (m_PathRecorder + "\\Game" + m_GameCounter + "\\Round" + m_RoundCounter + "_" + m_PlayerNumber + "_Transforms_ascii.IArec")) {
			int i;
			for ( i = 0 ; i < m_TransformsTime.Count ; i++) {
				//file.WriteLine(m_TransformsTime[i].ToString() + " " + m_TransformsPosition[i].ToString() + " " + m_TransformsRotation[i].ToString() );

				file.WriteLine(m_TransformsTime[i].ToString() + " " + m_TransformsPosition[i].x + " " + m_TransformsPosition[i].y + " " + m_TransformsPosition[i].z + " " + m_TransformsRotation[i].x
					+ " " + m_TransformsRotation[i].y + " " + m_TransformsRotation[i].z + " " + m_TransformsRotation[i].w + " " + m_Health[i]);
			}
		}
	}

	public void Reset() {
		m_RoundCounter++;
		m_Actions.Clear ();
		m_ActionsType.Clear ();
		m_ActionsTime.Clear ();

		m_TransformsPosition.Clear ();
		m_TransformsRotation.Clear ();
		m_TransformsTime.Clear ();
	}

	public void setActionsToLog() {
		if (m_Log != null) {
			m_Log.setActions (m_ActionsTime, m_Actions, m_ActionsType);
		}
	}
}
