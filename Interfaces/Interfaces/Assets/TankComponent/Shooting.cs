using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Interfaces {
	public class Shooting : MonoBehaviour
	{
	    public int m_PlayerNumber = 1;       
	    public Rigidbody m_Shell;            
	    public Transform m_FireTransform;    
	    public Slider m_AimSlider;           
	    public AudioSource m_ShootingAudio;  
	    public AudioClip m_ChargingClip;     
	    public AudioClip m_FireClip;         
	    public float m_MinLaunchForce = 15f; 
	    public float m_MaxLaunchForce = 30f; 
		public float m_MaxChargeTime = 0.75f;
		[HideInInspector]
		public Dictionary<int, int> m_FireOrders;

		private float m_ChargeSpeed;
		private float m_CurrentLaunchForce;

		public float getChargeSpeed() {
			return m_ChargeSpeed;
		}

		public float getCurrentLaunchForce () {
			return m_CurrentLaunchForce;
		}

		public void setCurrentLaunchForce (float newForce) {
			m_CurrentLaunchForce = newForce;
		}
	}
}