using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame {
	static class Settings {
		public static bool LazyLoading;

		static Settings() {
			LazyLoading = true; // false - preloads all data at startup, true - loads when needed
		}
	}
}
