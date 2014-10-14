using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame {
	// Use this instead of the Console class, console crashes if the program runs as 
	static class Terminal {
#if DEBUG
		static bool Discard = false;
#else
		static bool Discard = true;
#endif

		public static void PrintLn(object O, params object[] Args) {
			if (Discard)
				return;
			if (O != null && Args != null)
				O = string.Format(O.ToString(), Args);
			Console.WriteLine(O ?? "NULL");
		}

		public static void Print(object O, params object[] Args) {
			if (Discard)
				return;
			if (O != null && Args != null)
				O = string.Format(O.ToString(), Args);
			Console.Write(O ?? "NULL");
		}
	}
}