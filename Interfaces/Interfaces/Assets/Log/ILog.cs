namespace Interfaces {

	public abstract class ILog{

		ITankManager m_TankManager;

		public void setTank(ITankManager m_TankManager)
		{
			this.m_TankManager = m_TankManager;
		}

		public void captureFrame()
		{

		}

	}

}