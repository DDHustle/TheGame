using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using SFML;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace TheGame {
	static class TileMgr {
		public static int TileSize;
		static Image TileAtlas;
		static Dictionary<int, Texture> Tiles;

		static TileMgr() {
			TileSize = 64;

			TileAtlas = GraphicsMgr.GetTexture("worldset.png").CopyToImage();
			Tiles = new Dictionary<int, Texture>();
		}

		public static Texture GetTile(int X, int Y) {
			return GetTileTex(X + Y * TileSize);
		}

		public static Texture GetTileTex(int I) {
			if (Tiles.ContainsKey(I))
				return Tiles[I];
			Tiles.Add(I, new Texture(TileAtlas,
				new IntRect((I % TileSize) * TileSize, (I / TileSize) * TileSize, TileSize, TileSize)));
			return GetTileTex(I);
		}
	}
}