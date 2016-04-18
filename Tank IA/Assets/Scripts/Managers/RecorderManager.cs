using UnityEngine;
using System.Collections;

public class RecorderManager : MonoBehaviour {

	[HideInInspector] public int m_PlayerNumber = 1;
	[HideInInspector] public GameObject m_Instance;

	private Recorder m_Recorder;

	// Use this for initialization
	public void Setup () {
		m_Recorder = m_Instance.GetComponent<Recorder> ();

		m_Recorder.m_PlayerNumber = m_PlayerNumber;
	}

	public void SetTankInstance(TankManager tankManager) {
		m_Recorder.SetTankManager (tankManager);
	}

	public void WriteActions() {
		m_Recorder.WriteActions ();
		m_Recorder.WriteActionsASCII ();

		m_Recorder.WriteTransforms ();
		m_Recorder.WriteTransformsASCII ();
	}


	public void DisableControl()
	{
		m_Recorder.enabled = false;
	}


	public void EnableControl()
	{
		m_Recorder.enabled = true;
	}


	public void Reset()
	{
		m_Instance.SetActive(false);
		m_Recorder.Reset ();
		m_Instance.SetActive(true);
	}
}
