﻿using System;

namespace Interfaces
{
	public class IATank
	{
		public IIAMovements m_IAMovement;
		public IIAShooting m_IAShooting;
		public IIAShield m_IAShield;

		public IATank ()
		{
		}

		public IIAMovements getIAMovement() {
			return m_IAMovement;
		}

		public IIAShooting getIAShooting() {
			return m_IAShooting;
		}

		public IIAShield getIAShield() {
			return m_IAShield;
		}
	}
}
