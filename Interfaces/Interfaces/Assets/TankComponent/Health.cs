using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Interfaces {
	public class Health : MonoBehaviour
	{
	    public float m_StartingHealth = 100f;          
	    public Slider m_Slider;                        
	    public Image m_FillImage;                      
	    public Color m_FullHealthColor = Color.green;  
	    public Color m_ZeroHealthColor = Color.red;    
	    public GameObject m_ExplosionPrefab;
		[HideInInspector] public Dictionary<int, float> m_HealthOrders = new Dictionary<int, float>();
		[HideInInspector] public int m_TimeReference;

		protected float m_CurrentHealth;

		virtual public void TakeDamage(float amount) { return; }
		virtual public float getCurrentHealth() { return m_CurrentHealth; }
	}
}