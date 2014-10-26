using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace TheGame {
	class Polymesh {
		public static Polymesh Empty;


		static Polymesh() {
			Empty = new Polymesh(new Vector2f[] { });
		}

		Vector2f[] Points;
		VertexArray VAO;
		public Color PolyColor;

		public int Length {
			get {
				return Points.Length;
			}
		}

		public Polymesh(Vector2f[] Points)
			: this(Points, Color.Black) {
		}

		public Polymesh(Vector2f[] Points, Color Clr) {
			this.Points = Points;
			PolyColor = Clr;
		}

		public Polymesh Rebuild(PrimitiveType T = PrimitiveType.Triangles) {
			if (VAO != null)
				VAO.Dispose();
			VAO = new VertexArray(T, (uint)Points.Length);
			for (int i = 0; i < Points.Length; i++)
				VAO[(uint)i] = new Vertex(Points[i], PolyColor);
			return this;
		}

		public Vector2f this[int i] {
			get {
				return Points[i];
			}
			set {
				Points[i] = value;
			}
		}

		public Polymesh Multiply(Vector2f Num) {
			for (int i = 0; i < Points.Length; i++)
				Points[i] = new Vector2f(Points[i].X * Num.X, Points[i].Y * Num.Y);
			return this;
		}

		public Polymesh Add(Vector2f Num) {
			for (int i = 0; i < Points.Length; i++)
				Points[i] += Num;
			return this;
		}

		public Polymesh Merge(Vector2f[] S2) {
			Vector2f[] Pts = new Vector2f[Points.Length * 2];

			for (int i = 0, j = 0; i < Points.Length * 2; i += 2, j++) {
				Pts[i] = Points[j];
				Pts[i + 1] = S2[j];
			}

			return new Polymesh(Pts, PolyColor).Rebuild(PrimitiveType.TrianglesStrip);
		}

		public void Draw(RenderTarget R) {
			if (VAO == null)
				Rebuild();
			R.Draw(VAO);
		}
	}
}