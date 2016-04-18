using UnityEngine;
using System.Collections;

using Interfaces;

public class TankShield : Shield {

	/*
    public float m_DelayActivation = 5f;
    public float m_DelayRecharge = 20f;
    public int m_PlayerNumber = 1;
    [HideInInspector]
    public bool m_IsEnable = true;
    public bool m_IsActivate = false;
	*/

	private GameObject m_Shield;
    private float m_DeltaTime = Time.time;
    private string m_ShieldButton;
   
 
	void Awake () {
		m_Shield = GameObject.FindWithTag("Shield");
		m_Shield.SetActive(false);
	}

    // Use this for initialization
	void Start () {
        m_ShieldButton = "Shield" + m_PlayerNumber;
    }
	
	// Update is called once per frame
	void Update () {
        
        Activate();
        
        if (m_IsActivate && Time.time - m_DeltaTime > m_DelayActivation) {           
            m_Shield.SetActive(false);
            
            m_IsActivate = false;
            m_DeltaTime = Time.time;
            
        }
		else if (!m_IsActivate && !m_IsEnable && Time.time - m_DeltaTime > m_DelayRecharge) {
            m_IsEnable = true;
        }
	}

    public void Activate() {
        if (m_IsEnable && !m_IsActivate && Input.GetButtonDown(m_ShieldButton)) {        
            m_Shield.SetActive(true);
            m_IsActivate = true;    
			m_IsEnable = false;
			m_DeltaTime = Time.time;
    	}
    }
}
