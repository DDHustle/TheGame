using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.GUI {
	class GUIBase : UIElement {
		public new Renderer Renderer;

		public GUIBase(Renderer R) : base(null) {
			Renderer = R;
		}
	}
}