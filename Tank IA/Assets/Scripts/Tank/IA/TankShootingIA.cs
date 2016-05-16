using UnityEngine;
using UnityEngine.UI;

using Interfaces;
using System;
using System.IO;
using System.Reflection;
using System.Linq;

public class TankShootingIA : Shooting {
	
   	//private float m_CurrentLaunchForce;  
    //private float m_ChargeSpeed;         
    private bool m_Fired;
	private bool m_Charging;

	private string m_IAPath = ScenesParameters.m_IAPath;
	private IIAShooting m_IAShooting;

    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
		LoadIA ();

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }
    

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
		m_AimSlider.value = m_MinLaunchForce;

		int action = m_IAShooting.Fire ();

		if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired) {
			m_CurrentLaunchForce = m_MaxLaunchForce;
			Fire ();
		}
		else if (!m_Charging && action == 1) {
			m_Charging = true;
			m_Fired = false;
			m_CurrentLaunchForce = m_MinLaunchForce;

			m_ShootingAudio.clip = m_ChargingClip;
			m_ShootingAudio.Play ();
		}
		else if (action == 1) {
			m_CurrentLaunchForce += m_ChargeSpeed * 0.02f;

			m_AimSlider.value = m_CurrentLaunchForce;
		}
		else if (action == 2 && !m_Fired) {
			m_Charging = false;
			Fire ();
		}
		else if (action == 2 && !m_Charging) {
			Fire ();
		}
    }

	private void LoadIA() {
		/*string file = ScenesParameters.m_IATanks[m_PlayerNumber-1];

		string relativePath = String.Format ("{0}{2}", m_IAPath, Path.DirectorySeparatorChar, file);

		//Charge l'assembly
		Assembly assembly = Assembly.LoadFile (relativePath);

		// Récupérer et instancier la class IIAMovements
		foreach ( Type module in assembly.GetTypes() ) {
			if (module.BaseType == typeof(IIAShooting))
				m_IAShooting = (IIAShooting) Activator.CreateInstance (module);
		}*/

		m_IAShooting = m_IATank.getIAShooting ();
		m_IAShooting.setTankMovement ((Shooting)this);
	}


    private void Fire()
    {
        // Instantiate and launch the shell.
		m_Fired = true;

		Rigidbody shellInstance = Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

		shellInstance.GetComponent<ShellExplosion> ().m_PlayerNumber = m_PlayerNumber;

		shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

		m_ShootingAudio.clip = m_FireClip;
		m_ShootingAudio.Play ();

		m_CurrentLaunchForce = m_MinLaunchForce;
    }
}