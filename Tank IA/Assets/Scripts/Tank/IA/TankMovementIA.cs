using UnityEngine;

using Interfaces;
using System.Reflection;
using System;
using System.IO;
using System.Linq;

public class TankMovementIA : Movement
{
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue = 0;
    private float m_TurnInputValue = 0;
    private float m_OriginalPitch;         

	private string m_IAPath = ScenesParameters.m_IAPath;
	private IIAMovements m_IAMovement;
	private float m_AcceleroValue = 0.05f;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
		LoadIA ();

        m_OriginalPitch = m_MovementAudio.pitch;
    }
    
	private void LoadIA() {
		/*string file = ScenesParameters.m_IATanks[m_PlayerNumber-1];
		string relativePath = String.Format ("{0}{2}", m_IAPath, Path.DirectorySeparatorChar, file);

		//Charge l'assembly
		Assembly assembly = Assembly.LoadFile (relativePath);

		// Récupérer et instancier la class IIAMovements
		foreach ( Type module in assembly.GetTypes() ) {
			if (module.BaseType == typeof(IIAMovements))
				m_IAMovement = (IIAMovements) Activator.CreateInstance (module);
		}*/
		m_IAMovement = m_IATank.getIAMovement ();
		m_IAMovement.setTankMovement ((Movement)this);
	}

    private void Update()
    {
		EngineAudio ();
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.

		if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f) {
			if (m_MovementAudio.clip == m_EngineDriving) {
				m_MovementAudio.clip = m_EngineIdling;
				m_MovementAudio.pitch = UnityEngine.Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		} else {
			if (m_MovementAudio.clip == m_EngineIdling) {
				m_MovementAudio.clip = m_EngineDriving;
				m_MovementAudio.pitch = UnityEngine.Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play ();
			}
		}


    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
		int move = m_IAMovement.Move ();
		int turn = m_IAMovement.Turn ();

		// Acceleration
		switch (move) {
		case -1:
			if (m_MovementInputValue > -1f)
				m_MovementInputValue -= m_AcceleroValue;
			if (m_MovementInputValue < -1f)
				m_MovementInputValue = -1f;
			break;
		case 0:
			if (m_MovementInputValue < 0f) {
				m_MovementInputValue += m_AcceleroValue;
			} else if (m_MovementInputValue > 0f) {
				m_MovementInputValue -= m_AcceleroValue;
			}
			break;
		case 1:
			if (m_MovementInputValue < 1f)
				m_MovementInputValue += m_AcceleroValue;
			if (m_MovementInputValue > 1f)
				m_MovementInputValue = 1f;
			break;
		}
		switch (turn) {
		case -1:
			if (m_TurnInputValue > -1f)
				m_TurnInputValue -= m_AcceleroValue;
			if (m_TurnInputValue < -1f)
				m_TurnInputValue = -1f;
			break;
		case 0:
			if (m_TurnInputValue < 0f) {
				m_TurnInputValue += m_AcceleroValue;
			} else if (m_TurnInputValue > 0f) {
				m_TurnInputValue -= m_AcceleroValue;
			}
			break;
		case 1:
			if (m_TurnInputValue < 1f)
				m_TurnInputValue += m_AcceleroValue;
			if (m_TurnInputValue > 1f)
				m_TurnInputValue = 1f;
			break;
		}
		Move ();
		Turn ();

	}

	private void Move()
	{
		// Adjust the position of the tank based on the player's input.
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * (float) 0.02f;//Time.deltaTime;

		m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
	}


	private void Turn()
	{
		// Adjust the rotation of the tank based on the player's input.
		float turn = m_TurnInputValue * m_TurnSpeed * (float) 0.02f;// Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
	}
}