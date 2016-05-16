using UnityEngine;
using System.Collections;

using Interfaces;
using System;
using System.Reflection;
using System.IO;

public class TankShieldIA : Shield {

	private GameObject m_Shield;
    private float m_DeltaTime;
    private string m_ShieldButton;

	private string m_IAPath = ScenesParameters.m_IAPath;
	private IIAShield m_IAShield;
   
 
	void Awake () {
		m_DeltaTime = Time.time;
		m_Shield = GameObject.FindWithTag("Shield");
		m_Shield.SetActive(false);
	}

    // Use this for initialization
	void Start () {
		LoadIA ();
    }
	
	// Update is called once per frame
	void Update () {
        
		Activate(m_IAShield.Activate());
        
        if (m_IsActivate && Time.time - m_DeltaTime > m_DelayActivation) {           
            m_Shield.SetActive(false);
            
            m_IsActivate = false;
            m_DeltaTime = Time.time;
            
        }
		else if (!m_IsActivate && !m_IsEnable && Time.time - m_DeltaTime > m_DelayRecharge) {
            m_IsEnable = true;
        }
	}

	private void LoadIA() {
		/*string file = ScenesParameters.m_IATanks[m_PlayerNumber-1];

		string relativePath = String.Format ("{0}{2}", m_IAPath, Path.DirectorySeparatorChar, file);

		//Charge l'assembly
		Assembly assembly = Assembly.LoadFile (relativePath);

		// Récupérer et instancier la class IIAMovements
		foreach ( Type module in assembly.GetTypes() ) {
			if (module.BaseType == typeof(IIAShield))
				m_IAShield = (IIAShield) Activator.CreateInstance (module);
		}*/

		m_IAShield = m_IATank.getIAShield ();
		m_IAShield.setTankMovement ((Shield)this);
	}

	public void Activate(int action) {
        if (m_IsEnable && !m_IsActivate && action == 1) {
            m_Shield.SetActive(true);
            m_IsActivate = true;    
			m_IsEnable = false;
			m_DeltaTime = Time.time;
    	}
    }
}
