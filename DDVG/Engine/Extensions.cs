namespace DDVG {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SFML.Graphics;
    using SFML.Window;

    static class Extensions {
        public static Vector2f GetPosition(this FloatRect R) {
            return new Vector2f(R.Left, R.Top);
        }

        public static Vector2f GetSize(this FloatRect R) {
            return new Vector2f(R.Width, R.Height);
        }

        public static Vector2f ToWorld(this View V, Vector2f Pos) {
            return (V.Center - V.Size / 2) + Pos;
        }

        public static Vector2i ToScreen(this View V, Vector2f Pos) {
            Vector2f R = Pos - (V.Center - V.Size / 2);
            return new Vector2i((int)R.X, (int)R.Y);
        }

        public static Vector2f ToVector2f(this MouseMoveEventArgs E) {
            return new Vector2f(E.X, E.Y);
        }
    }
}