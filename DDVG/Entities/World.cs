using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

using SFML;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

using TheGame.GUI;
using TheGame.Entities;

namespace TheGame.Entities {
	class World : Entity {
		Sprite A, B, C;
		Texture Trans;

		public World() {
			A = new Sprite(new Texture("data/textures/tiles/a.png"));
			A.Scale = new Vector2f(4, 4);

			B = new Sprite(new Texture("data/textures/tiles/b.png"));
			B.Rotation = 20;
			B.Scale = A.Scale;

			C = new Sprite(new Texture("data/textures/tiles/c.png"));
			C.Scale = B.Scale;

			Trans = new Texture("data/textures/tiles/alpha.png");
		}

		Sprite P(Sprite S, Vector2f Pos) {
			S.Position = Pos;
			return S;
		}

		public override void Render(Renderer R) {
			R.Draw(P(A, new Vector2f(100, 100)));
			R.AlphaMask(P(B, new Vector2f(150, 150)), Trans);

			R.Draw(P(C, new Vector2f(250, 230)));

			base.Render(R);
		}
	}
}