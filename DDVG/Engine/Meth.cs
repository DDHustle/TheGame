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

		public static bool InRange(this Vector2f A, Vector2f B, float Range) {
			return A.Distance(B) < Range;
		}

		public static float Pow(this float A, float Pwr = 2) {
			if (Pwr == 2)
				return A * A;
			return (float)Math.Pow(A, Pwr);
		}

		public static float Sqrt(this float A) {
			return (float)Math.Sqrt(A);
		}

		public static float Dot(this Vector2f A, Vector2f B) {
			return A.X * B.X + A.Y * B.Y;
		}

		public static float Angle(this Vector2f A, Vector2f B) {
			float xD = A.X - B.X;
			float yD = A.Y - B.Y;
			return (float)(Math.Atan2(yD, xD) /** 180.0 / Math.PI*/);
		}
	}
}