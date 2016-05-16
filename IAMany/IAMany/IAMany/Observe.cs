using System;
using System.Collections.Generic;
using UnityEngine;

namespace Many
{
	public class Observe
	{
		Rigidbody m_Allie;
		Rigidbody m_Ennemy;

		private LayerMask m_CollisionMask;
		private LayerMask m_ShellMask;

		static private int m_Quartier = 8;
		private float m_Radius = 20;
		private float m_RadiusShell = 3;

		private string m_NameDistance = "distance?";
		private int m_DomDefDistance = 4;
		private int[] m_IntervallesDistances = { 20, 60, 120};

		private string m_NameDirection = "direction?";
		private int m_DomDefDirection = m_Quartier + 1;

		private string m_NameDistColl1 = "dist_coll_1?";
		private int m_DomDefDistColl1 = 3;
		private string m_NameDirColl1 = "dir_coll_1?";
		private int m_DomDefDirColl1 = m_Quartier + 1;
		private string m_NameDistColl2 = "dist_coll_2?";
		private int m_DomDefDistColl2 = 3;
		private string m_NameDirColl2 = "dir_coll_2?";
		private int m_DomDefDirColl2 = m_Quartier + 1;
		private string m_NameDistColl3 = "dist_coll_3?";
		private int m_DomDefDistColl3 = 3;
		private string m_NameDirColl3 = "dir_coll_3?";
		private int m_DomDefDirColl3 = m_Quartier + 1;
		private int[] m_IntervallesCollision = { 10, 30};

		private string m_NameDistShell = "dist_shell?";
		private int m_DomDefDistShell = 2;

		public Observe ()
		{
		}

		public void setMask(LayerMask collisionMask, LayerMask shellMask)
		{
			m_CollisionMask = collisionMask;
			m_ShellMask = shellMask;
		}

		public void setTanks(Rigidbody allie, Rigidbody ennemy) {
			m_Allie = allie;
			m_Ennemy = ennemy;
		}

		public Dictionary<string,double[]> Observation() {
			Dictionary<string,double[]> obs = new Dictionary<string,double[]> ();
			float distance = Vector3.Distance(m_Allie.position,m_Ennemy.position);
			int finalDistance = -1;

			int j, k;

			for (j = 0; j < m_DomDefDistance - 1 ; j++) {
				if (distance < (float) m_IntervallesDistances [j]) {
					finalDistance = j;
					break;
				}
			}

			Vector3 directionEnnemy = m_Ennemy.position - m_Allie.position;
			Vector3 directionAllie = m_Allie.transform.forward;
			float angle = Vector3.Angle(directionEnnemy, directionAllie); // - 360.0/(m_Quartier*2.0);
			if (Vector3.Cross (directionEnnemy, directionAllie).z > 0)
				angle = 360 - angle;

			float alpha = 360f / m_Quartier;


			int i = (int) Math.Floor(angle / alpha);

			Collider[] colliders = Physics.OverlapSphere(m_Allie.position, m_Radius, m_CollisionMask);

			int[] iC = {0,0,0};

			float[] distancesC = { m_Radius + 1f, m_Radius + 1f, m_Radius + 1f };
			int[] finalDistancesC = { 0, 0, 0};
			int index = 0;

			foreach (Collider collider in colliders)
			{
				float distanceC = Vector3.Distance(m_Allie.position,collider.transform.position);
				Vector3 directionC = collider.transform.position - m_Allie.position;
				float angleC = Vector3.Angle(directionC, directionAllie);
				if (Vector3.Cross (directionC, directionAllie).z > 0)
					angleC = 360 - angleC;

				if (distancesC[index] > distanceC)
				{
					distancesC[index] = distanceC;
					iC[index] = (int)Math.Floor(angleC / alpha);
					for ( k = 0 ; k < distancesC.Length ; k++)
					{
						if (distancesC[k] > distancesC[index])
						{
							index = k;
						}
					}
				}

				// TODO trier distancesC (et iC en parallèle)
			}

			for ( j = 0 ; j < m_DomDefDistColl1 - 1 ; j++ ) {
				for ( k = 0 ; k < 3 ; k++ ) {
					if (distancesC[k] < (float) m_IntervallesCollision[j]) {
						finalDistancesC[k] = j;
					}
				}
			}				

			colliders = Physics.OverlapSphere(m_Ennemy.position, m_RadiusShell, m_ShellMask);

			float distanceS = m_RadiusShell + 1f;
			int ennemyShell = 0;
			int iS = -1;
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
			if (iS != -1)
				ennemyShell = 1;
			else
				ennemyShell = 0;

			// Ajout des observtations
			int ind, jnd;
			double[] finalDistanceD = new double[m_DomDefDistance];
			for ( ind = 0 ; ind < m_DomDefDistance ; ind++ ) {
				if (ind == finalDistance)
					finalDistanceD [ind] = 1.0;
				else
					finalDistanceD [ind] = 0.0;
			}
			double[] iD = new double[m_DomDefDirection];
			for ( ind = 0 ; ind < m_DomDefDirection ; ind++ ) {
				if (ind == i)
					iD [ind] = 1.0;
				else
					iD [ind] = 0.0;
			}

			List<double[]> finalDistancesCD = new List<double[]>();
			for (jnd = 0; jnd < 3; jnd++) {
				finalDistancesCD.Add (new double[m_DomDefDistColl1]);
				for (ind = 0; ind < m_DomDefDistColl1; ind++) {
					if (ind == finalDistancesC[jnd])
						finalDistancesCD[jnd][ind] = 1.0;
					else
						finalDistancesCD[jnd][ind] = 0.0;
				}
			}

			List<double[]> iCD = new List<double[]>();
			for (jnd = 0; jnd < 3; jnd++) {
				iCD.Add (new double[m_DomDefDirColl1]);
				for (ind = 0; ind < m_DomDefDirColl1; ind++) {
					if (ind == iC[jnd])
						iCD[jnd][ind] = 1.0;
					else
						iCD[jnd][ind] = 0.0;
				}
			}

			double[] iSD = new double[m_DomDefDistShell];
			for ( ind = 0 ; ind < m_DomDefDistShell ; ind++ ) {
				if (ind == ennemyShell)
					iSD [ind] = 1.0;
				else
					iSD [ind] = 0.0;
			}

			Debug.Log ("distance");
			Debug.Log (finalDistance);
			Debug.Log (finalDistanceD[0]);
			Debug.Log (finalDistanceD[1]);
			Debug.Log (finalDistanceD[2]);

			obs.Add(m_NameDistance, finalDistanceD);
			obs.Add(m_NameDirection, iD);
			obs.Add(m_NameDistColl1, finalDistancesCD[0]);
			obs.Add(m_NameDistColl2, finalDistancesCD[1]);
			obs.Add(m_NameDistColl3, finalDistancesCD[2]);
			obs.Add(m_NameDirColl1, iCD[0]);
			obs.Add(m_NameDirColl2, iCD[1]);
			obs.Add(m_NameDirColl3, iCD[2]);
			obs.Add (m_NameDistShell, iSD);

			double[] move = { 1.0 / 3.0, 1.0 / 3.0, 1.0 / 3.0 };
			double[] turn = { 1.0 / 3.0, 1.0 / 3.0, 1.0 / 3.0 };
			double[] shield = { 0.5, 0.5 };
			double[] shell = { 1.0 / 3.0, 1.0 / 3.0, 1.0 / 3.0 };
			obs.Add ("move?", move);
			obs.Add ("turn?", turn);
			obs.Add ("shield?", shield);
			obs.Add ("shell?", shell);

			return obs;
			//m_DistanceToEnnemy.Add(finalDistance);
			//m_DirectionToEnnemy.Add(i);
			//m_NearestColliders.Add (new Couple<int[], int[]> (finalDistancesC, iC));
			//m_EnnemyShell.Add(new Couple<int, int>(ennemyShell, iS));
			//m_Frames.Add (Time.frameCount - m_TimeReference);
		}
	}
}

