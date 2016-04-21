using UnityEngine;namespace Interfaces {

	public class ILog { //: MonoBehaviour {


		public int m_PlayerNumber;
		public string m_PathLog;
		ITankManager m_TankManager;

		public void setTank(ITankManager m_TankManager)
		{
			this.m_TankManager = m_TankManager;
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

	}

}