using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace TheGame.GUI {
	class Button : UIElement {
		public String ButtonText;
		public RectangleShape Rect;

		internal Text TextObj;
		internal Font FontObj;
		internal Sound Tick;

		public float TextOffsetX, TextOffsetY;

		public override Vector2f Position {
			get {
				return base.Position;
			}
			set {
				base.Position = value;
				Vector2f Bounds = FontMgr.GetSize(TextObj) / 2;
				TextObj.Position = Position + new Vector2f(TextOffsetX, TextOffsetY) - Bounds + Size / 2;
				Rect.Position = Position;
			}
		}

		public override Vector2f Size {
			get {
				return base.Size;
			}
			set {
				base.Size = value;
				Rect.Size = Size;
				Position = Position;
			}
		}

		public Button(GUIBase UI, String Txt, Font F, float TextOffsetX = 0, float TextOffsetY = -10)
			: base(UI) {
			ButtonText = Txt;
			FontObj = F;

			UI.AddElement(this);

			this.TextOffsetX = TextOffsetX;
			this.TextOffsetY = TextOffsetY;

			TextObj = new Text();
			TextObj.Font = F;
			TextObj.Style = Text.Styles.Bold;
			TextObj.CharacterSize = 30;
			TextObj.DisplayedString = ButtonText;
			TextObj.Color = Color.Red;

			Rect = new RectangleShape();
			Rect.OutlineColor = Color.Black;
			Rect.OutlineThickness = 4;

			Tick = AudioMgr.GetSound("tick.wav");
		}

		public override void OnMouseClick(Mouse.Button B, int X, int Y, bool Down) {
			if (!Down)
				AudioMgr.PlayOnce(Tick);
			base.OnMouseClick(B, X, Y, Down);
		}

		public override void Render(Main R) {
			R.Draw(Rect);
			R.Draw(TextObj);
			base.Render(R);
		}
	}
}