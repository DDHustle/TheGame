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
	class Tile {
		public bool Enabled, IsWall;
		public Texture Tex;

		public Tile(Texture T, bool Wall) {
			Enabled = true;
			Tex = T;
			IsWall = Wall;
		}

		static Tile() {
			T = new Sprite(new Texture("data/textures/tiles/a.png"));
		}

		internal static Sprite T;

		public static void Draw(Renderer R, Tile[,] Tiles, int W, int H, View V) {
			int TexW = (int)T.Texture.Size.X;
			int TexH = (int)T.Texture.Size.Y;

			/*int VX = (int)(V.Center.X - V.Viewport.Width * V.Size.X / 3);
			int VY = (int)(V.Center.Y - V.Viewport.Height * V.Size.Y / 3);*/

			for (int x = 0; x < W; x++)
				for (int y = 0; y < H; y++) {
					T.Position = new Vector2f(x * TexW, y * TexH);

					if (Tiles[x, y].Enabled)
						Tiles[x, y].Draw(T, R);

				}
		}

		public void Draw(Sprite S, Renderer R) {
			S.Texture = Tex;
			R.Draw(S);
		}
	}

	class World : Entity {
		public Tile[,] Tiles;
		int W, H;

		public View V;

		public World(int W = 20, int H = 20) {
			Tiles = new Tile[W, H];
			this.W = W;
			this.H = H;

			V = new View(new FloatRect(0, 0, 800, 600));

			Texture A = GraphicsMgr.GetTexture("tiles/a.png");
			Texture B = GraphicsMgr.GetTexture("tiles/b.png");

			Tile Bottom = new Tile(B, false);
			Tile Wall = new Tile(A, true);

			for (int x = 0; x < W; x++)
				for (int y = 0; y < H; y++)
					Tiles[x, y] = Bottom;

			Tiles[5, 5] = Wall;
		}

		public override void Render(Renderer R) {
			R.SetView(V);
			Tile.Draw(R, Tiles, W, H, V);
			base.Render(R);
		}
	}
}