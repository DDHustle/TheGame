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

using DDVG.GUI;
using DDVG.Entities;

namespace DDVG.Entities {
    public class World {
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

            Tiles[5, 3] = Tiles[5, 4] = Tiles[5, 5] = Tiles[5, 6] = Tiles[5, 7] = Wall;
            Tiles[6, 3] = Tiles[7, 3] = Tiles[8, 3] = Wall;
            Tiles[6, 7] = Tiles[7, 7] = Tiles[8, 7] = Wall;
        }

        public Polymesh[] GetShadowMesh(Vector2f Pos, float Rad) {
            // TODO: Clip properly
            List<Polymesh> Shapes = new List<Polymesh>();

            for (int x = 0; x < W; x++)
                for (int y = 0; y < H; y++) {
                    Polymesh S = Tiles[x, y].GetShadowMesh(x, y, Pos, Rad);
                    if (S != null)
                        Shapes.Add(S);
                }

            return Shapes.ToArray();
        }

        public FloatRect[] GetSolidAABBs(Vector2f Pos, float Rad) {
            List<FloatRect> AABBs = new List<FloatRect>();

            for (int x = 0; x < W; x++)
                for (int y = 0; y < H; y++)
                    if (Tiles[x, y].IsWall)
                        AABBs.Add(Tiles[x, y].GetAABB(x, y, Pos, Rad));

            return AABBs.ToArray();
        }

        public void Render(Main R) {
            Tile.Draw(R, Tiles, W, H);
        }
    }
}