using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

using OpenTK.Graphics.OpenGL4;

namespace TheGame.Entities {
	class Light {
		public float Range;
		public Vector2f Position {
			get {
				return LSprite.Position;
			}
			set {
				LSprite.Position = value;
			}
		}

		Sprite LSprite;

		public Light(Vector2f Pos, float Range, Color Clr) {
			this.Range = Range;

			LSprite = new Sprite(GraphicsMgr.GetTexture("lightmask.png"));
			Position = Pos;
			LSprite.Origin = new Vector2f(LSprite.Texture.Size.X, LSprite.Texture.Size.Y) / 2;
			LSprite.Scale = new Vector2f(Range / LSprite.Texture.Size.X, Range / LSprite.Texture.Size.Y);
			LSprite.Color = Clr;
		}

		public void Render(World W, View V, Renderer R) {
			if (!Position.InRange(V.Center, (V.Size.X.Pow() + V.Size.Y.Pow()).Sqrt()))
				return;

			Shape[] ShadowShapes = W.GetShadowMesh(Position, Range);
			foreach (var SS in ShadowShapes)
				R.Draw(SS);

			R.LightBuffer.Draw(LSprite);
		}
	}
}