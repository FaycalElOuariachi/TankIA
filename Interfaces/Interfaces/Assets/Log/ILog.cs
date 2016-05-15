using UnityEngine;
using System.Collections.Generic;namespace Interfaces {

	public class ILog { //: MonoBehaviour {

		[HideInInspector] public int m_TimeReference;
		public int m_PlayerNumber;
		public string m_PathLog;

		protected List<int> m_ActionsTime = new List<int> ();
		protected List<float> m_Actions = new List<float> ();
		protected List<char> m_ActionsType = new List<char> ();

		virtual public void setTank(ITankManager m_TankManager, ITankManager m_ennemy)
		{
		}

		virtual public void Setup() {

		}

		virtual public void setMask(LayerMask collisionMask, LayerMask shellMask) {

		}

		virtual public void WriteDomaines() {
		}

		virtual public void captureFrame()
		{

		}

		virtual public void WriteASCII() {

		}

		virtual public void Write() {

		}

		virtual public void Reset() {

		}

		public void setActions(List<int> actionsTime, List<float> actions, List<char> actionsType) {
			m_ActionsTime = actionsTime;
			m_Actions      = actions;
			m_ActionsType = actionsType;
		}

		public void setTimeReference(int time) {
			m_TimeReference = time;
		}

	}

}