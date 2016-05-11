using UnityEngine;namespace Interfaces {

	public class ILog { //: MonoBehaviour {


		public int m_PlayerNumber;
		public string m_PathLog;

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

	}

}