using System;
using System.Collections.Generic;
using System.Diagnostics;

using SFML.Graphics;
using SFML.Window;

namespace DDVG {
	class Program {
		static void Main(string[] args) {
			Renderer R = new Renderer(800, 600);
			Stopwatch SWatch = new Stopwatch();
			SWatch.Start();

			while (R.IsOpen()) {
				R.DispatchEvents();
				R.Update((float)SWatch.ElapsedMilliseconds / 1000);
				R.Render();
				SWatch.Restart();
			}
		}
	}
}