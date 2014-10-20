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
	struct Tile {
		public bool Enabled;
		public Texture Tex;

		public Tile(Texture T) {
			Enabled = true;
			Tex = T;
		}

		static Tile() {
			T = new Sprite(new Texture("data/textures/tiles/a.png"));
		}

		internal static Sprite T;

		// TODO: Strip off and make better culling

		public static void Draw(Tile[] Tiles, Renderer R, View V, int W, int H) {
			int TexW = (int)T.Texture.Size.X;
			int TexH = (int)T.Texture.Size.Y;

			R.SetView(V);

			int VX = (int)(V.Center.X - V.Viewport.Width * V.Size.X / 3);
			int VY = (int)(V.Center.Y - V.Viewport.Height * V.Size.Y / 3);

			for (int w = VX / TexW, h = VY / TexH; h < H; w++) {
				if (w >= W) {
					w = 0;
					h++;
				}

				T.Position = new Vector2f(w * TexW, h * TexH);

				int i = w + h * W;
				if ((i < 0) || (i >= W * H))
					continue;

				if (Tiles[i].Enabled)
					Tiles[i].Draw(T, R);
			}
		}

		public void Draw(Sprite S, Renderer R) {
			S.Texture = Tex;
			R.Draw(S);
		}
	}

	class World : Entity {
		Tile[] Tiles;
		int W, H;

		public View V;

		public World(int W = 8, int H = 8) {
			Tiles = new Tile[W * H];
			this.W = W;
			this.H = H;

			V = new View(new FloatRect(0, 0, 800, 600));

			Texture A = new Texture("data/textures/tiles/a.png");
			Texture B = new Texture("data/textures/tiles/b.png");

			SetTile(new Tile(A), 1, 1);
			SetTile(new Tile(A), 1, 2);
			SetTile(new Tile(A), 1, 3);
			SetTile(new Tile(A), 1, 4);

			for (int i = 0; i < W * H; i++)
				SetTile(new Tile(A), i);

			for (int y = 1; y < 5; y++) {
				SetTile(new Tile(B), 2, y);
				SetTile(new Tile(B), 3, y);
				SetTile(new Tile(B), 4, y);
			}

		}

		public void SetTile(Tile T, int X, int Y) {
			SetTile(T, X + Y * W);
		}

		public void SetTile(Tile T, int I) {
			Tiles[I] = T;
		}

		public override void Render(Renderer R) {
			Tile.Draw(Tiles, R, V, W, H);
			base.Render(R);
		}
	}
}