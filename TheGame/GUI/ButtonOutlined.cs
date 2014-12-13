using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace TheGame.GUI {
	class ButtonOutlined : Button {
		public ButtonOutlined(GUIBase UI, String Txt, Font F, float TextOffsetX = 0, float TextOffsetY = -10)
			: base(UI, Txt, F, TextOffsetX, TextOffsetY) {
		}

		public override void Update(float T) {
			if (MouseInside)
				Rect.OutlineThickness = (float)(Math.Sin((float)UI.Renderer.GameTime.ElapsedMilliseconds / 500) + 1.5) * 5;
			else
				Rect.OutlineThickness = 4;
			base.Update(T);
		}
	}
}