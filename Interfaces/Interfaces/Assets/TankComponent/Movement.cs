using System;
using UnityEngine;
using System.Collections.Generic;

namespace Interfaces {
	public class Movement : MonoBehaviour
	{
		public int m_PlayerNumber = 1;         
		public float m_Speed = 12f;            
		public float m_TurnSpeed = 180f;       
		public AudioSource m_MovementAudio;    
		public AudioClip m_EngineIdling;       
		public AudioClip m_EngineDriving;      
		public float m_PitchRange = 0.2f;
		[HideInInspector]
		public Dictionary<int, float> m_MovementOrders;
		[HideInInspector]
		public Dictionary<int, float> m_TurnOrders;
		[HideInInspector]
		public Dictionary<int, Vector3> m_PositionOrders = new Dictionary<int, Vector3>();
		[HideInInspector]
		public Dictionary<int, Quaternion> m_RotationOrders = new Dictionary<int, Quaternion>();
		[HideInInspector] public int m_TimeReference;
		[HideInInspector] public IATank m_IATank;
	}
}

