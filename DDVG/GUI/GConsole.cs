using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace TheGame.GUI {
	class GConsole : UIElement {
		internal RectangleShape Background;

		public GConsole(GUIBase B)
			: base(B) {

			Background = new RectangleShape();
			Position = Background.Position = new Vector2f();
			Size = Background.Size = new Vector2f(B.Renderer.Size.X, 350);
			Background.FillColor = new Color(12, 12, 12, 100);

			KeyPressed += KeyPressedEvent;
		}

		void KeyPressedEvent(Keyboard.Key Code, bool Ctrl, bool Shift, bool Alt, bool System, bool Down) {
			if (Down && (Code == Keyboard.Key.F1 || Code == Keyboard.Key.Escape)) {
				Active = false;
				UI.Focused = true;
			}
		}

		public override void Render(Main R) {
			R.Draw(Background);
			base.Render(R);
		}
	}
}