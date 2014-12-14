namespace DDVG.Entities {
    using System;
    using SFML.Graphics;
    using SFML.Window;
    using OpenTK;

    /// <summary>
    /// 
    /// </summary>
    public class Tile {

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled, IsWall;

        /// <summary>
        /// 
        /// </summary>
        public Texture Tex;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="T"></param>
        /// <param name="Wall"></param>
        public Tile(Texture T, bool Wall) {
            Enabled = true;
            Tex = T;
            IsWall = Wall;
        }
        
        /// <summary>
        /// 
        /// </summary>
        static Tile() {
            T = new Sprite(TileMgr.GetTileTex(0));
        }

        /// <summary>
        /// 
        /// </summary>
        internal static Sprite T;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="R"></param>
        /// <param name="Tiles"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        public static void Draw(Main R, Tile[,] Tiles, int W, int H) {
            View V = R.GetView();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Pos"></param>
        /// <param name="Rad"></param>
        /// <returns></returns>
        public FloatRect GetAABB(int X, int Y, Vector2f Pos, float Rad) {
            return new FloatRect(X * TileMgr.TileSize, Y * TileMgr.TileSize, TileMgr.TileSize, TileMgr.TileSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Pos"></param>
        /// <param name="Rad"></param>
        /// <returns></returns>
        public Polymesh GetBaseMesh(int X, int Y, Vector2f Pos, float Rad) {
            if (!IsWall)
                return null;

            Polymesh SShape = new Polymesh(new Vector2f[] {
					new Vector2f(0, 0),
					new Vector2f(TileMgr.TileSize, 0),
					new Vector2f(TileMgr.TileSize, TileMgr.TileSize),
					new Vector2f(TileMgr.TileSize, TileMgr.TileSize),
					new Vector2f(0, TileMgr.TileSize),
					new Vector2f(0, 0),
				}, Color.White);
            return SShape.Add(new Vector2f(X * TileMgr.TileSize, Y * TileMgr.TileSize));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Pos"></param>
        /// <param name="Rad"></param>
        /// <returns></returns>
        public Polymesh GetShadowMesh(int X, int Y, Vector2f Pos, float Rad) {
            if (!IsWall)
                return null;

            Polymesh SShape = GetBaseMesh(X, Y, Pos, Rad);
            SShape.PolyColor = Color.Black;

            Vector2f[] Pts2 = new Vector2f[SShape.Length];
            for (int i = 0; i < Pts2.Length; i++) {
                float Ang = SShape[i].Angle(Pos);
                Pts2[i] = new Vector2f(SShape[i].X + Rad * (float)Math.Cos(Ang), SShape[i].Y + Rad * (float)Math.Sin(Ang));
            }

            return SShape.Merge(Pts2);
        }
    }

}
