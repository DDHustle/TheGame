namespace DDVG.Entities {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenTK.Graphics.OpenGL4;
    using SFML.Graphics;
    using SFML.Window;

    public class Light {
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

        public void Render(World W, RenderTarget R) {
            R.Draw(LSprite);
        }

        public void RenderShadows(World W, RenderTexture R) {
            Polymesh[] ShadowShapes = W.GetShadowMesh(Position, Range);
            for (int i = 0; i < ShadowShapes.Length; i++)
                ShadowShapes[i].Draw(R);
        }

        // TODO: Render lights inside solid blocks
        public void RenderSolidLights(World W, RenderTexture R) {
            FloatRect[] AABBs = W.GetSolidAABBs(Position, Range);
            R.SetActive(true);
            GL.Enable(EnableCap.ScissorTest);
            for (int i = 0; i < AABBs.Length; i++) {
                Vector2i Pos = R.MapCoordsToPixel(AABBs[i].GetPosition());
                GL.Scissor((int)Pos.X, (int)R.Size.Y - (int)Pos.Y - (int)AABBs[i].Height - 1,
                    (int)AABBs[i].Width + 1, (int)AABBs[i].Height + 1);
                Render(W, R);
            }

            GL.Disable(EnableCap.ScissorTest);
        }
    }
}