using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;

namespace TheGame {
	static class Meth {
		public static float Distance(this Vector2f A, Vector2f B) {
			return ((B.X - A.X).Pow() + (B.Y - A.Y).Pow()).Sqrt();
		}

		public static float Pow(this float A, float Pwr = 2) {
			if (Pwr == 2)
				return A * A;
			return (float)Math.Pow(A, Pwr);
		}

		public static float Sqrt(this float A) {
			return (float)Math.Sqrt(A);
		}
	}
}