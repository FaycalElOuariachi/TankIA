using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class TankManagerReplay {//: ITankManager {

    public Color m_PlayerColor;
    public Transform m_SpawnPoint;
    [HideInInspector]public int m_PlayerNumber;
    [HideInInspector]public string m_ColoredPlayerText;
    [HideInInspector]public GameObject m_Instance;
    [HideInInspector]public int m_Wins;
    [HideInInspector]public string m_RecorderPath;

    private int m_nbRounds;
    private TankMovementReplay m_Movement;
    private TankShootingReplay m_Shooting;
	private TankHealthReplay m_Health;
    private TankShieldReplay m_Shield;
    private GameObject m_CanvasGameObject;


    public void Setup()
    {
		Debug.Log ("Timon");
        m_Movement = m_Instance.GetComponent<TankMovementReplay>();
        m_Shooting = m_Instance.GetComponent<TankShootingReplay>();
        m_Shield = m_Instance.GetComponent<TankShieldReplay>();
		m_Health = m_Instance.GetComponent<TankHealthReplay> ();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;
		Debug.Log ("Pumba");

        m_Movement.m_PlayerNumber = m_PlayerNumber;
        m_Shooting.m_PlayerNumber = m_PlayerNumber;
        m_Shield.m_PlayerNumber = m_PlayerNumber;

		Debug.Log ("Hakuna");
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
		Debug.Log ("Matata");
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }


    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;
        m_Shield.enabled = false;

        m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;
        m_Shield.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        m_nbRounds++;
        setOrderLists();
		getTranformLists();

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
                switch (split[1])
                {
                    case "M":
                        movementOrders.Add(int.Parse(split[0]), float.Parse(split[2]));
                        break;

                    case "T":
                        turnOrders.Add(int.Parse(split[0]), float.Parse(split[2]));
                        Debug.LogWarning(split[2]);
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
		//Dictionary<int, int> fireOrders = new Dictionary<int, int>();
		//Dictionary<int, float> shieldOrders = new Dictionary<int, float>();

		using (StreamReader fileIn = new StreamReader(m_RecorderPath + "\\Round" + m_nbRounds + "_" + m_PlayerNumber + "_Transforms_ascii.IArec"))
		{
			string read;
			while ((read = fileIn.ReadLine()) != null)
			{
				string[] split = read.Split(' ');
				positionOrders.Add (int.Parse (split [0]), new Vector3 (float.Parse (split [1]), float.Parse (split [2]), float.Parse (split [3])));
				rotationOrders.Add (int.Parse (split [0]), new Quaternion (float.Parse (split [4]), float.Parse (split [5]), float.Parse (split [6]), float.Parse(split[7])));
				healthOrders.Add (int.Parse (split [0]), float.Parse (split [8]));
			}
		}
		m_Movement.m_PositionOrders = positionOrders;
		m_Movement.m_RotationOrders = rotationOrders;
		if (m_Health.m_HealthOrders != null)
			Debug.Log("Timon & Pumba");
		m_Health.m_HealthOrders = healthOrders;


		/*m_Movement.m_MovementOrders = movementOrders;
		m_Movement.m_TurnOrders = turnOrders;
		m_Shooting.m_FireOrders = fireOrders;
		m_Shield.m_ShieldOrders = shieldOrders;*/

	}

}
