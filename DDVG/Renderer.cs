using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using SFML.Graphics;
using SFML.Window;
using TheGame.States;

namespace TheGame {
	class Renderer : RenderWindow {
		public State ActiveState;
		public Stopwatch GameTime;

		public Renderer(int W, int H)
			: base(new VideoMode((uint)W, (uint)H), "The Game", Styles.Close) {
			GameTime = new Stopwatch();
			GameTime.Start();

			Closed += (S, E) => Close();
			TextEntered += (S, E) => ActiveState.TextEntered(E.Unicode);
			KeyPressed += (S, E) => ActiveState.Key(E, true);
			KeyReleased += (S, E) => ActiveState.Key(E, false);
			MouseMoved += (S, E) => ActiveState.MouseMove(E);
			MouseButtonPressed += (S, E) => ActiveState.MouseClick(E, true);
			MouseButtonReleased += (S, E) => ActiveState.MouseClick(E, false);
		}

		public void SwitchState(State NewState) {
			if (ActiveState != null)
				ActiveState.Deactivate(NewState);
			NewState.Activate(ActiveState);
			ActiveState = NewState;
		}

		public void Update(float T) {
			AudioMgr.Update(T);
			ActiveState.Update(T);
		}

		public void Render() {
			ActiveState.Render(this);
			Display();
		}
	}
}