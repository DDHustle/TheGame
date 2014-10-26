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
			Background.Position = new Vector2f();
			Background.Size = new Vector2f(B.Renderer.Size.X, 300);
			Background.FillColor = new Color(12, 12, 12, 100);

		}

		public override void Render(Renderer R) {
			R.Draw(Background);
			base.Render(R);
		}
	}
}