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
		public Polymesh SShape;

		public Tile(Texture T, bool Wall) {
			Enabled = true;
			Tex = T;
			IsWall = Wall;
			SShape = null;
		}

		static Tile() {
			T = new Sprite(TileMgr.GetTileTex(0));
		}

		internal static Sprite T;

		public static void Draw(Renderer R, Tile[,] Tiles, int W, int H, View V) {
			Vector2f LeftViewCorner = (V.Center - (new Vector2f(V.Viewport.Width * V.Size.X, V.Viewport.Height * V.Size.Y) / 2));
			LeftViewCorner /= TileMgr.TileSize;

			int VX = MathHelper.Clamp((int)LeftViewCorner.X, 0, W);
			int VXm = MathHelper.Clamp(VX + 14, 0, W);
			int VY = MathHelper.Clamp((int)LeftViewCorner.Y, 0, H);
			int VYm = MathHelper.Clamp(VY + 11, 0, H);

			for (int x = VX; x < VXm; x++)
				for (int y = VY; y < VYm; y++)
					if (Tiles[x, y].Enabled) {
						T.Position = new Vector2f(x * TileMgr.TileSize, y * TileMgr.TileSize);
						T.Texture = Tiles[x, y].Tex;
						R.Draw(T);
					}
		}

		public Shape GetShadowMesh(int X, int Y, Vector2f Pos, float Rad) {
			if (!IsWall)
				return null;

			if (SShape == null) {
				SShape = new Polymesh(new Vector2f[] {
					new Vector2f(0, 0),
					new Vector2f(TileMgr.TileSize, 0),
					new Vector2f(TileMgr.TileSize, TileMgr.TileSize),
					new Vector2f(0, TileMgr.TileSize),
				});
				SShape.Add(new Vector2f(X * TileMgr.TileSize, Y * TileMgr.TileSize));
			}

			Vector2f[] Pts2 = new Vector2f[SShape.Length];
			for (int i = 0; i < Pts2.Length; i++){
				float Ang = Pos.Angle(SShape[i]);
				Pts2[i] = new Vector2f(SShape[i].X + Rad * (float)Math.Cos(Ang), SShape[i].Y + Rad * (float)Math.Sin(Ang));
			}

			Shape SS = new Polymesh(Pts2).ToShape();
			//Shape SS = SShape.ToShape();
			SS.FillColor = Color.Black;
			SS.OutlineThickness = 2;
			SS.OutlineColor = Color.White;
			return SS;
		}
	}

	class World {
		public Tile[,] Tiles;
		int W, H;

		public World(int W = 20, int H = 20) {
			Tiles = new Tile[W, H];
			this.W = W;
			this.H = H;

			Tile Bottom = new Tile(TileMgr.GetTileTex(1), false);
			Tile Wall = new Tile(TileMgr.GetTileTex(2), true);

			for (int x = 0; x < W; x++)
				for (int y = 0; y < H; y++)
					Tiles[x, y] = Bottom;

			Tiles[5, 5] = Wall;
		}

		public Shape[] GetShadowMesh(Vector2f Pos, float Rad) {
			// TODO: Clip properly
			List<Shape> Shapes = new List<Shape>();

			for (int x = 0; x < W; x++)
				for (int y = 0; y < H; y++) {
					Shape S = Tiles[x, y].GetShadowMesh(x, y, Pos, Rad);
					if (S != null)
						Shapes.Add(S);
				}

			return Shapes.ToArray();
		}

		public void Render(Renderer R, View V) {
			Tile.Draw(R, Tiles, W, H, V);
		}
	}
}