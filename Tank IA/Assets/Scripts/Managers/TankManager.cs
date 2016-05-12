using System;
using UnityEngine;
using UnityEngine.UI;

using Interfaces;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class TankManager : ITankManager// : ATankManager
{
	public Color m_PlayerColor;
	public Transform m_SpawnPoint;
	[HideInInspector] public int m_PlayerNumber;
	[HideInInspector] public string m_ColoredPlayerText;
	// [HideInInspector] public GameObject m_Instance;
	[HideInInspector] public int m_Wins;
	[HideInInspector] public string m_RecorderPath;
	[HideInInspector] public bool m_isReplay = false;
	[HideInInspector] public int m_TimeReference;


	private int m_nbRounds;
	private int m_nbFrame;
    private Movement m_Movement;       
    private Shooting m_Shooting;
	private Shield m_Shield;
	private Health m_Health;
    private GameObject m_CanvasGameObject;


    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<Movement>();
        m_Shooting = m_Instance.GetComponent<Shooting>();
        m_Shield = m_Instance.GetComponent<Shield>();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

		if (m_isReplay) {
			m_Health = m_Instance.GetComponent<Health> ();
			m_Instance.GetComponent<BoxCollider> ().enabled = false;
			m_Movement.m_TimeReference = m_TimeReference;
			m_Shooting.m_TimeReference = m_TimeReference;
			m_Shield.m_TimeReference = m_TimeReference;
			m_Health.m_TimeReference = m_TimeReference;
		}

        m_Movement.m_PlayerNumber = m_PlayerNumber;
        m_Shooting.m_PlayerNumber = m_PlayerNumber;
        m_Shield.m_PlayerNumber = m_PlayerNumber;

        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }

	/*public void Update() {
		//if (Time.frameCount > m_nbFrame + 10)
		//	m_Health.TakeDamage (100);
	}*/

    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;
		m_Shield.enabled = false;

		if (m_isReplay)
			m_Health.enabled = false;

        m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;
		m_Shield.enabled = true;

		if (m_isReplay) {
			m_Health.enabled = true;
		}

		m_CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
		if (m_isReplay) {
			m_nbRounds++;
			setOrderLists ();
			getTranformLists ();
		}

        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
	}

	private void setOrderLists()
	{
		Dictionary<int, float> movementOrders = new Dictionary<int, float>();
		Dictionary<int, float> turnOrders = new Dictionary<int, float>();
		Dictionary<int, int> fireOrders = new Dictionary<int, int>();
		Dictionary<int, float> shieldOrders = new Dictionary<int, float>();

		using (StreamReader fileIn = new StreamReader(m_RecorderPath + "\\Round" + m_nbRounds + "_" + m_PlayerNumber + "ascii.IArec"))
		{
			string read;
			while ((read = fileIn.ReadLine()) != null)
			{
				string[] split = read.Split(' ');

				if (m_nbFrame < int.Parse(split[0]))
					m_nbFrame = int.Parse(split[0]);
				
				switch (split[1])
				{
				case "M":
					movementOrders.Add(int.Parse(split[0]), float.Parse(split[2]));
					break;

				case "T":
					turnOrders.Add(int.Parse(split[0]), float.Parse(split[2]));
					break;

				case "F":
					fireOrders.Add(int.Parse(split[0]), int.Parse(split[2]));
					break;

				case "S":
					shieldOrders.Add(int.Parse(split[0]), float.Parse(split[2]));
					break;

				default:
					break;
				}

			}
		}
		m_Movement.m_MovementOrders = movementOrders;
		m_Movement.m_TurnOrders = turnOrders;
		m_Shooting.m_FireOrders = fireOrders;
		m_Shield.m_ShieldOrders = shieldOrders;

	}

	private void getTranformLists()
	{

		Dictionary<int, Vector3> positionOrders = new Dictionary<int, Vector3>();
		Dictionary<int, Quaternion> rotationOrders = new Dictionary<int, Quaternion>();
		Dictionary<int, float> healthOrders = new Dictionary<int, float>();

		using (StreamReader fileIn = new StreamReader(m_RecorderPath + "\\Round" + m_nbRounds + "_" + m_PlayerNumber + "_Transforms_ascii.IArec"))
		{
			string read;
			while ((read = fileIn.ReadLine()) != null)
			{
				string[] split = read.Split(' ');

				if (m_nbFrame < int.Parse(split[0]))
					m_nbFrame = int.Parse(split[0]);

				positionOrders.Add (int.Parse (split [0]), new Vector3 (float.Parse (split [1]), float.Parse (split [2]), float.Parse (split [3])));
				rotationOrders.Add (int.Parse (split [0]), new Quaternion (float.Parse (split [4]), float.Parse (split [5]), float.Parse (split [6]), float.Parse(split[7])));
				healthOrders.Add (int.Parse (split [0]), float.Parse (split [8]));
			}
		}
		m_Movement.m_PositionOrders = positionOrders;
		m_Movement.m_RotationOrders = rotationOrders;
		m_Health.m_HealthOrders = healthOrders;
	}

	public void OnDestroy() {
		UnityEngine.Object.Destroy (m_Instance);
	}
}
