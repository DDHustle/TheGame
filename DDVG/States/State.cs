using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace TheGame.States {
	class State : IDisposable {
		public State() {
		}

		public virtual void Dispose() {
		}

		// Called on old state when switching to new state To
		public virtual void SwitchTo(State To) {
		}

		// Called on new state when switching from old state From
		public virtual void SwitchFrom(State From) {
		}

		public virtual void TextEntered(string S) {
		}

		public virtual void Key(KeyEventArgs K, bool Down = true) {
		}

		public virtual void Update(float T) {
		}

		public virtual void Render(Renderer R) {
		}
	}
}
