using UnityEngine;namespace Interfaces {

	public abstract class ILog : MonoBehaviour {


		public int m_PlayerNumber;
		ITankManager m_TankManager;

		public void setTank(ITankManager m_TankManager)
		{
			this.m_TankManager = m_TankManager;
		}

		public void captureFrame()
		{

		}

		public void Write() {

		}

		public void Reset() {

		}

	}

}