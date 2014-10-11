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
			
			Renderer R = new Renderer(800, 600);
			Stopwatch SWatch = new Stopwatch();
			SWatch.Start();

			R.SwitchState(new MenuState());
            
			while (R.IsOpen()) {
				R.DispatchEvents();
				R.Update((float)SWatch.ElapsedMilliseconds / 1000);
				R.Render();
				SWatch.Restart();
			}
            Console.WriteLine("Quitting...");
		}
        static void KeyPressed(char c, bool down)
        {
            Console.WriteLine(c.ToString());
        }
	}
}