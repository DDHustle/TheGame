using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

using Con = System.Console;

namespace TheGame {
	// Use this instead of the Console class, console crashes if the program runs as 
	static class Console {
#if DEBUG
		static bool Discard = false;
#else
		static bool Discard = true;
#endif

		public static string Title {
			get {
				if (Discard)
					return "";
				return Con.Title;
			}
			set {
				if (Discard)
					return;
				Con.Title = value;
			}
		}

		public static void WriteLine(object O, params object[] Args) {
			if (Discard)
				return;
			if (O != null && Args != null)
				O = string.Format(O.ToString(), Args);
			Con.WriteLine(O ?? "NULL");
		}

		public static void Write(object O, params object[] Args) {
			if (Discard)
				return;
			if (O != null && Args != null)
				O = string.Format(O.ToString(), Args);
			Con.Write(O ?? "NULL");
		}
	}
}