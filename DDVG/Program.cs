using System;
using System.Collections.Generic;
using System.Diagnostics;

using TheGame.States;

using SFML.Graphics;
using SFML.Window;

namespace TheGame {
	class Program {
		static void Main(string[] args) {
			Console.Title = "The Game Console";

			ContextSettings CS = new ContextSettings(24, 8, 0, 0, 0);
			Renderer R = new Renderer(800, 600, CS);
			Stopwatch SWatch = new Stopwatch();
			SWatch.Start();

			R.SwitchState(new MenuState(R));
			R.Init();

			while (R.IsOpen()) {
				R.DispatchEvents();
				while (SWatch.ElapsedMilliseconds < 16)
					;
				R.Update((float)SWatch.ElapsedMilliseconds / 1000f);
				SWatch.Restart();
				R.Render();
			}

			Console.WriteLine("Quitting...");
			Environment.Exit(0); // Required
		}
	}
}