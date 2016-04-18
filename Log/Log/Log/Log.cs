using UnityEngine;
using Interfaces;
using System;

public class Log : ILog
{
	private Rigidbody m_Allie;
	private Rigidbody m_Ennemy;
	private int m_Quartier = 8;
	private float m_Radius = 15;
	private float m_RadiusShell = 3;
	private LayerMask m_CollisionMask;
	private LayerMask m_ShellMask;

	public Log()
	{

	}

	public void setTank(ITankManager tankManager, ITankManager ennemy)
	{

		m_Allie = tankManager.m_Instance.GetComponent<Rigidbody>();
		m_Ennemy = ennemy.m_Instance.GetComponent<Rigidbody>();
	}
	public void setMask(LayerMask collisionMask, LayerMask shellMask)
	{
		m_CollisionMask = collisionMask;
		m_ShellMask = shellMask;
	}

	public void captureFrame()
	{
		float distance = Vector3.Distance(m_Allie.position,m_Ennemy.position);
		Vector3 directionEnnemy = m_Ennemy.position - m_Allie.position;
		Vector3 directionAllie = m_Allie.transform.forward;
		float angle = Vector3.Angle(directionEnnemy, directionAllie);
		float alpha = 360f / m_Quartier;

		int i = (int) Math.Floor(angle / alpha);

		Collider[] colliders = Physics.OverlapSphere(m_Allie.position, m_Radius, m_CollisionMask);

		int[] iC = {0,0,0};

		float[] distancesC = { m_Radius + 1f, m_Radius + 1f, m_Radius + 1f };
		int index = 0;

		foreach (Collider collider in colliders)
		{
			float distanceC = Vector3.Distance(m_Allie.position,collider.transform.position);
			Vector3 directionC = collider.transform.position - m_Allie.position;
			float angleC = Vector3.Angle(directionC, directionAllie);

			if (distancesC[index] > distanceC)
			{
				distancesC[index] = distanceC;
				iC[index] = (int)Math.Floor(angleC / alpha);
				for (int k = 0; k < distancesC.Length; k++)
				{
					if (distancesC[k] > distancesC[index])
					{
						index = k;
					}
				}
			}
		}

		colliders = Physics.OverlapSphere(m_Ennemy.position, m_RadiusShell, m_ShellMask);

		float distanceS = m_RadiusShell + 1f;
		int iS = 0;

		foreach (Collider collider in colliders)
		{
			float distanceC = Vector3.Distance(m_Ennemy.position, collider.transform.position);
			Vector3 directionC = collider.transform.position - m_Ennemy.position;
			float angleC = Vector3.Angle(directionC, directionEnnemy);

			int tmp = (int) Math.Floor(angleC / alpha);


			if(tmp == 0 || tmp == m_Quartier - 1){
				if (distanceS > distanceC)
				{
					distanceS = distanceC;
					iS = tmp;
				}
			}


		}
	}

}