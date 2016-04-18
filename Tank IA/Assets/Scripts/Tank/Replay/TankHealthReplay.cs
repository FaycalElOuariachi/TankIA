using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using Interfaces;

public class TankHealthReplay : Health {
	
    private AudioSource m_ExplosionAudio;          
    private ParticleSystem m_ExplosionParticles;   
    private float m_CurrentHealth;  
    private bool m_Dead;


    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
		m_Dead = false;

        SetHealthUI();
    }
    
	/*public void update() {
		float newHealth;
		Debug.Log ("Timon");

		if (m_HealthOrders.TryGetValue (Time.frameCount, out newHealth) && m_CurrentHealth != newHealth) {
			Debug.Log (m_CurrentHealth - newHealth);
			TakeDamage (m_CurrentHealth - newHealth);
		}
	}*/

    override public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
		m_CurrentHealth -= amount;

		SetHealthUI ();

		if (m_CurrentHealth <= 0f && !m_Dead)
			OnDeath();
    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
		m_Slider.value = m_CurrentHealth;

		m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
		m_Dead = true;

		m_ExplosionParticles.transform.position = transform.position;
		m_ExplosionParticles.gameObject.SetActive (true);

		m_ExplosionParticles.Play ();
		m_ExplosionAudio.Play ();

		gameObject.SetActive (false);
    }

	override public float getCurrentHealth() {
		return m_CurrentHealth;
	}
}