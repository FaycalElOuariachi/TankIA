using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Interfaces {
	public class Shield : MonoBehaviour {

	    public float m_DelayActivation = 5f;
	    public float m_DelayRecharge = 20f;
	    public int m_PlayerNumber = 1;
	    [HideInInspector]
	    public bool m_IsEnable = true;
		public bool m_IsActivate = false;
		[HideInInspector]
		public Dictionary<int, float> m_ShieldOrders;
		[HideInInspector] public int m_TimeReference;
		[HideInInspector] public IATank m_IATank;
	}
}
