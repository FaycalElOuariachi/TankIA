using UnityEngine;
using Interfaces;

using System;
using System.Collections.Generic;
using System.IO;

namespace Log {
	public class Log : ILog
	{
		private int m_RoundCounter = 0;
		private int m_GameCounter;

		private Rigidbody m_Allie;
		private Rigidbody m_Ennemy;
		private int m_Quartier = 8;
		private float m_Radius = 5;
		private float m_RadiusShell = 1;
		private LayerMask m_CollisionMask;
		private LayerMask m_ShellMask;

		private List<float> m_DistanceToEnnemy = new List<float> (); // variable distance
		private List<int> m_DirectionToEnnemy = new List<int> (); // variable i
		private List<Couple<float[],int[]>> m_NearestColliders = new List<Couple<float[],int[]>>(); // varaibles distancesC, iC
		private List<Couple<float,int>> m_EnnemyShell = new List<Couple<float,int>>(); // varaibles distanceS, iS


		public Log()
		{
		}

		override public void Setup() {
			Directory.CreateDirectory(m_PathLog);
			m_GameCounter = Directory.GetDirectories (m_PathLog).Length + 1;
		}

		override public void setTank(ITankManager tankManager, ITankManager ennemy)
		{
			m_Allie = tankManager.m_Instance.GetComponent<Rigidbody>();
			m_Ennemy = ennemy.m_Instance.GetComponent<Rigidbody>();
		}

		override public void setMask(LayerMask collisionMask, LayerMask shellMask)
		{
			m_CollisionMask = collisionMask;
			m_ShellMask = shellMask;
		}

		override public void captureFrame()
		{
			float distance = Vector3.Distance(m_Allie.position,m_Ennemy.position);
			Vector3 directionEnnemy = m_Ennemy.position - m_Allie.position;
			Vector3 directionAllie = m_Allie.transform.forward;
			float angle = Vector3.Angle(directionEnnemy, directionAllie);
			if (Vector3.Cross (directionEnnemy, directionAllie).z > 0)
				angle = 360 - angle;

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
				if (Vector3.Cross (directionC, directionAllie).z > 0)
					angleC = 360 - angleC;

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

			if (distancesC [0] > m_Radius && distancesC [1] > m_Radius && distancesC [2] > m_Radius) {
				distancesC [0] = distancesC [1] = distancesC [2] = -1f;
			}
				

			colliders = Physics.OverlapSphere(m_Ennemy.position, m_RadiusShell, m_ShellMask);

			float distanceS = m_RadiusShell + 1f;
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

			if (iS == -1)
				distanceS = -1f;

			// Ajout des observtations
			m_DistanceToEnnemy.Add(distance);
			m_DirectionToEnnemy.Add(i);
			m_NearestColliders.Add (new Couple<float[], int[]> (distancesC, iC));
			m_EnnemyShell.Add(new Couple<float, int>(distanceS, iS));

		}

		// ASCII Writer
		override public void WriteASCII() {
			Directory.CreateDirectory(m_PathLog + Path.AltDirectorySeparatorChar + "Game" + m_GameCounter);
			int nbColl = m_NearestColliders [0].First.Length;
			using (StreamWriter file =
				new StreamWriter (m_PathLog + Path.AltDirectorySeparatorChar + "Game" + m_GameCounter + Path.AltDirectorySeparatorChar + "Round" + m_RoundCounter + "_" + m_PlayerNumber + ".ASCIIlog")) {
				string line = "";
				for ( int i = 0 ; i < m_DistanceToEnnemy.Count ; i++) {
					line = m_DistanceToEnnemy [i].ToString () + " " + m_DirectionToEnnemy [i].ToString () + " ";
					for (int j = 0 ; j < nbColl ; j++) {
						line += m_NearestColliders [i].First [j].ToString() + " " + m_NearestColliders [i].Second [j].ToString() + " ";
					}
					line += m_EnnemyShell [i].First.ToString() + " " + m_EnnemyShell [i].Second.ToString();
					file.WriteLine(line);
				}
			}
		}

		// Binary Writer
		override public void Write() {
			Directory.CreateDirectory(m_PathLog + Path.AltDirectorySeparatorChar + "Game" + m_GameCounter);
			int nbColl = m_NearestColliders [0].First.Length;
			using (BinaryWriter file =
				new BinaryWriter (File.Open(m_PathLog + Path.AltDirectorySeparatorChar + "Game" + m_GameCounter + Path.AltDirectorySeparatorChar + "Round" + m_RoundCounter + "_" + m_PlayerNumber + ".log", FileMode.Create))) {
				for ( int i = 0 ; i < m_DistanceToEnnemy.Count ; i++) {
					file.Write (m_DistanceToEnnemy [i]);
					file.Write (m_DirectionToEnnemy [i]);

					for (int j = 0 ; j < nbColl ; j++) {
						file.Write (m_NearestColliders [i].First [j]);
						file.Write (m_NearestColliders [i].Second [j]);
					}
					file.Write (m_EnnemyShell [i].First);
					file.Write (m_EnnemyShell [i].Second);
				}
			}
		}

		override public void Reset() {
			m_RoundCounter++;
			m_DistanceToEnnemy.Clear ();
			m_DirectionToEnnemy.Clear ();
			m_NearestColliders.Clear ();
			m_EnnemyShell.Clear ();
		}

	}
}