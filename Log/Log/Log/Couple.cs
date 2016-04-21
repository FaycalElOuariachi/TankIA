using System;

namespace Log
{
	public class Couple<T, U> {
		public Couple() {
		}

		public Couple(T first, U second) {
			this.First = first;
			this.Second = second;
		}

		public T First { get; set; }
		public U Second { get; set; }
	};
}

