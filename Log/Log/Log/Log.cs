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
		static private int m_Quartier = 8;
		private float m_Radius = 20;
		private float m_RadiusShell = 3;
		private LayerMask m_CollisionMask;
		private LayerMask m_ShellMask;

		/**
		 * m_ActionsTime				: Liste des frames où une ou plusieurs actions ont été faites
		 * m_Actions					: Liste des valeurs des actions
		 * m_ActionsType				: Liste des types d'actions
		 * 
		 * m_DistanceToEnnemy		: Liste des distances à l'ennemi
		 * m_DirectionToEnnemy		: 
		 * m_NearestColliders			: 
		 * m_EnnemyShell				: 
		 */
		private List<int> m_Frames = new List<int> ();
		private List<int> m_DistanceToEnnemy = new List<int> (); // variable distance
		private List<int> m_DirectionToEnnemy = new List<int> (); // variable i
		private List<Couple<int[],int[]>> m_NearestColliders = new List<Couple<int[],int[]>>(); // varaibles distancesC, iC
		private List<Couple<int,int>> m_EnnemyShell = new List<Couple<int,int>>(); // varaibles distanceS, iS

		private string m_SpaceChara = ",";

		private string m_NameDistance = "distance?";
		private int m_DomDefDistance = 4;
		private int[] m_IntervallesDistances = { 20, 60, 120};

		private string m_NameDirection = "direction?";
		private int m_DomDefDirection = m_Quartier;

		private string m_NameDistColl1 = "dist_coll_1?";
		private int m_DomDefDistColl1 = 3;
		private string m_NameDirColl1 = "dir_coll_1?";
		private int m_DomDefDirColl1 = m_Quartier;
		private string m_NameDistColl2 = "dist_coll_2?";
		private int m_DomDefDistColl2 = 3;
		private string m_NameDirColl2 = "dir_coll_2?";
		private int m_DomDefDirColl2 = m_Quartier;
		private string m_NameDistColl3 = "dist_coll_3?";
		private int m_DomDefDistColl3 = 3;
		private string m_NameDirColl3 = "dir_coll_3?";
		private int m_DomDefDirColl3 = m_Quartier;
		private int[] m_IntervallesCollision = { 10, 30};

		private string m_NameDistShell = "dist_shell?";
		private int m_DomDefDistShell = 2;


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
			m_DistanceToEnnemy.Add(finalDistance);
			m_DirectionToEnnemy.Add(i);
			m_NearestColliders.Add (new Couple<int[], int[]> (finalDistancesC, iC));
			m_EnnemyShell.Add(new Couple<int, int>(ennemyShell, iS));
			m_Frames.Add (Time.frameCount - m_TimeReference);

		}

		// ASCII Writer
		override public void WriteASCII() {
			Directory.CreateDirectory(m_PathLog + Path.AltDirectorySeparatorChar + "Game" + m_GameCounter);
			int nbColl = m_NearestColliders [0].First.Length;
			using (StreamWriter file =
				new StreamWriter (m_PathLog + Path.AltDirectorySeparatorChar + "Game" + m_GameCounter + Path.AltDirectorySeparatorChar + "Round" + m_RoundCounter + "_" + m_PlayerNumber + ".ASCIIlog")) {
				file.WriteLine (WriteEntete());
				string line = "";

				bool flagM;
				bool flagT;
				bool flagF;
				bool flagS;

				for ( int i = 0 ; i < m_DistanceToEnnemy.Count ; i++) {
					line = m_DistanceToEnnemy [i].ToString () + m_SpaceChara + m_DirectionToEnnemy [i].ToString () + m_SpaceChara;
					for (int j = 0 ; j < nbColl ; j++) {
						line += m_NearestColliders [i].First [j].ToString() + m_SpaceChara + m_NearestColliders [i].Second [j].ToString() + m_SpaceChara;
					}
					line += m_EnnemyShell [i].First.ToString();// + m_SpaceChara + m_EnnemyShell [i].Second.ToString();

					flagM = false;
					flagT = false;
					flagF = false;
					flagS = false;

					while (m_ActionsTime.Count != 0 && m_ActionsTime [0] < m_Frames [i]) {
						m_ActionsTime.RemoveAt(0);
						m_Actions.RemoveAt(0);
						m_ActionsType.RemoveAt(0);
					}

					while (m_ActionsTime.Count != 0 && m_ActionsTime[0] == m_Frames[i]) {
						switch(m_ActionsType[0]) {
						case 'M':
							if (m_Actions [0] > 0f) {
								line += m_SpaceChara + "1";
							} else if (m_Actions [0] < 0f) {
								line += m_SpaceChara + "-1";
							} else {
								line += m_SpaceChara + "0";
							}
							flagM = true;
							break;
						case 'T':
							if (!flagM) {
								line += m_SpaceChara + "0";
								flagM = true;
							}
							
							if (m_Actions [0] > 0f) {
								line += m_SpaceChara + "1";
							} else if (m_Actions [0] < 0f) {
								line += m_SpaceChara + "-1";
							} else {
								line += m_SpaceChara + "0";
							}
							flagT = true;
							break;
						case 'F':
							if (!flagM) {
								line += m_SpaceChara + "0";
								flagM = true;
							}
							if (!flagT) {
								line += m_SpaceChara + "0";
								flagT = true;
							}

							line += m_SpaceChara + (int) m_Actions[0];
							flagF = true;
							break;
						case 'S':
							if (!flagM) {
								line += m_SpaceChara + "0";
								flagM = true;
							}
							if (!flagT) {
								line += m_SpaceChara + "0";
								flagT = true;
							}
							if (!flagF) {
								line += m_SpaceChara + "0";
								flagF = true;
							}
							
							line += m_SpaceChara + "1";
							flagS = true;
							break;
						}
						m_ActionsTime.RemoveAt(0);
						m_Actions.RemoveAt(0);
						m_ActionsType.RemoveAt(0);
					}
					int l;
					if (!flagM)
						l = 3;
					else if (!flagT)
						l = 2;
					else if (!flagF)
						l = 1;
					else if (!flagS)
						l = 0;
					else
						l = -1;

					for ( int k = 0 ; k <= l ; k++ ) {  
						line += m_SpaceChara + "0";
					}

					file.WriteLine(line);
				}
			}
		}

		private string WriteEntete() {
			return m_NameDistance + m_SpaceChara + m_NameDirection + m_SpaceChara + m_NameDistColl1 + m_SpaceChara + m_NameDirColl1 + m_SpaceChara + m_NameDistColl2 + m_SpaceChara + m_NameDirColl2 
				+ m_SpaceChara + m_NameDistColl3 + m_SpaceChara + m_NameDirColl3 + m_SpaceChara + m_NameDistShell + m_SpaceChara
				+ "move?" + m_SpaceChara + "turn?" + m_SpaceChara + "shell?" + m_SpaceChara + "shield?";// + + "dir_shell" ;
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
					//file.Write (m_EnnemyShell [i].Second);
				}
			}
		}

		override public void WriteDomaines() {
			Directory.CreateDirectory(m_PathLog);
			int nbColl = m_NearestColliders [0].First.Length;
			using (StreamWriter file =
				       new StreamWriter (File.Open (m_PathLog + Path.AltDirectorySeparatorChar + "domaines.logform", FileMode.Create))) {
				string line = "9" + "\n"
					+ m_NameDistance + m_SpaceChara + m_DomDefDistance + "\n"
					+ m_NameDistance + m_SpaceChara + m_DomDefDistance + "\n"
					+ m_NameDirection + m_SpaceChara + m_DomDefDirection + "\n"

					+ m_NameDistColl1 + m_SpaceChara + m_DomDefDistColl1 + "\n"
					+ m_NameDirColl1 + m_SpaceChara + m_DomDefDirColl1 + "\n"
					+ m_NameDistColl2 + m_SpaceChara + m_DomDefDistColl2 + "\n"
					+ m_NameDirColl2 + m_SpaceChara + m_DomDefDirColl2 + "\n"
					+ m_NameDistColl3 + m_SpaceChara + m_DomDefDistColl3 + "\n"
					+ m_NameDirColl3 + m_SpaceChara + m_DomDefDirColl3 + "\n"

					+ m_NameDistShell + m_SpaceChara + m_DomDefDistShell;
				file.WriteLine (line);
			}
		}

		override public void Reset() {
			m_RoundCounter++;
			m_DistanceToEnnemy.Clear ();
			m_DirectionToEnnemy.Clear ();
			m_NearestColliders.Clear ();
			m_EnnemyShell.Clear ();
			m_ActionsTime.Clear ();
			m_Actions.Clear ();
			m_ActionsType.Clear ();
		}

	}
}