using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Interfaces;

public class TankMovementReplay : Movement
{
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;

	private Vector3 m_Position;
	private Quaternion m_Rotation;
	private bool m_IsMoving = false;
	private bool m_IsTurning= false;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MovementOrders = new Dictionary<int, float>();
        m_TurnOrders = new Dictionary<int, float>(); 
    }


    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {		
        m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update()
	{
		m_IsMoving = m_PositionOrders.TryGetValue (Time.frameCount - m_TimeReference, out m_Position);
		m_IsTurning = m_RotationOrders.TryGetValue (Time.frameCount - m_TimeReference, out m_Rotation);

        EngineAudio();
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.

        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }


    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
		SetLocation();
    }

	private void SetLocation() {
		if (m_IsMoving)
			m_Rigidbody.MovePosition (m_Position);
		if (m_IsTurning)
			m_Rigidbody.MoveRotation (m_Rotation);
	}

	private void Move()
    {
    }


	private void Turn()
    {
    }
}
