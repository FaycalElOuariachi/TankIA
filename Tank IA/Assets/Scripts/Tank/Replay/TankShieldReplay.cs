using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Interfaces;

public class TankShieldReplay : Shield {

    private GameObject m_Shield;
    private float m_DeltaTime;


    void Awake()
    {
        m_Shield = GameObject.FindWithTag("Shield");
        m_Shield.SetActive(false);
        m_ShieldOrders = new Dictionary<int, float>();
		m_DeltaTime = Time.time;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Activate();

        if (m_IsActivate && Time.time - m_DeltaTime > m_DelayActivation)
        {
            m_Shield.SetActive(false);

            m_IsActivate = false;
            m_DeltaTime = Time.time;

        }
        else if (!m_IsActivate && !m_IsEnable && Time.time - m_DeltaTime > m_DelayRecharge)
        {
            m_IsEnable = true;
        }
    }

    public void Activate()
    {
        if (m_IsEnable && !m_IsActivate && m_ShieldOrders.ContainsKey(Time.frameCount))
        {
            m_Shield.SetActive(true);
            m_IsActivate = true;
            m_IsEnable = false;
            m_DeltaTime = Time.time;
        }
    }
}
