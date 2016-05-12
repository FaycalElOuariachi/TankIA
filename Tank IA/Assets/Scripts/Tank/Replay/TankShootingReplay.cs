using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

using Interfaces;

public class TankShootingReplay : Shooting {
    //private float m_CurrentLaunchForce;
    //private float m_ChargeSpeed;
    private bool m_Fired = false;


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }

    private void awake()
    {
        m_FireOrders = new Dictionary<int, int>();
    }


    private void Start()
    {
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }


    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        m_AimSlider.value = m_MinLaunchForce;
        int input = 3;
      
        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
		{
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }
		else if (m_FireOrders.TryGetValue(Time.frameCount - m_TimeReference, out input))
		{

            if (input == 0)
            {
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;

                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();
            }
            else if (input == 1)
            {
				m_CurrentLaunchForce += m_ChargeSpeed * 0.02f; // Time.deltaTime;

                m_AimSlider.value = m_CurrentLaunchForce;
            }
            else if (input == 2 && !m_Fired)
            {
				Fire();
            }
        }
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
        m_Fired = true;

        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.GetComponent<ShellExplosionReplay>().m_PlayerNumber = m_PlayerNumber;

        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        m_CurrentLaunchForce = m_MinLaunchForce;
    }
}
