using UnityEngine;

using Interfaces;
using System.Reflection;
using System;
using System.IO;
using System.Linq;

public class TankMovementIA : Movement
{
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;         

	private string m_IAPath = ScenesParameters.m_IAPath;
	private IIAMovements m_IAMovement;

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
		string file = ScenesParameters.m_IATanks[m_PlayerNumber-1];
		string relativePath = String.Format ("{0}{2}", m_IAPath, Path.DirectorySeparatorChar, file);

		//Charge l'assembly
		Assembly assembly = Assembly.LoadFile (relativePath);

		// Récupérer et instancier la class IIAMovements
		foreach ( Type module in assembly.GetTypes() ) {
			if (module.BaseType == typeof(IIAMovements))
				m_IAMovement = (IIAMovements) Activator.CreateInstance (module);
		}

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
		m_IAMovement.Move ();
		m_IAMovement.Turn ();
    }
}