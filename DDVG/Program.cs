﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

using TheGame.States;

using SFML.Graphics;
using SFML.Window;

namespace TheGame {
	class Program {
		static void Main(string[] args) {
			Console.Title = "The Game Console";

			Renderer R = new Renderer(800, 600);
			Stopwatch SWatch = new Stopwatch();
			SWatch.Start();

			R.SwitchState(new MenuState(R));

			while (R.IsOpen()) {
				R.DispatchEvents();
				R.Update((float)SWatch.ElapsedMilliseconds / 1000f);
				R.Render();
				SWatch.Restart();
			}

			Terminal.PrintLn("Quitting...");
			Environment.Exit(0); // Required
		}
	}
}