using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using TheGame.States;

namespace TheGame {
	class Renderer : RenderWindow {
		public State ActiveState;

		public Renderer(int W, int H)
			: base(new VideoMode((uint)W, (uint)H), "The Game", Styles.Close) {

			Closed += (S, E) => {
				ActiveState.Dispose();
				Close();
			};

			TextEntered += (S, E) => ActiveState.TextEntered(E.Unicode);
			KeyPressed += (S, E) => ActiveState.Key(E, true);
			KeyReleased += (S, E) => ActiveState.Key(E, false);
		}

		public void SwitchState(State NewState) {
			if (ActiveState != null)
				ActiveState.SwitchTo(NewState);
			NewState.SwitchTo(ActiveState);
			ActiveState = NewState;
		}

		public void Update(float T) {
			ActiveState.Update(T);
		}

		public void Render() {
			ActiveState.Render(this);
			Display();
		}
	}
}