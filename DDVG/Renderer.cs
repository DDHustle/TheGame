using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace DDVG {
	class Renderer : RenderWindow {
		List<Sprite> Sprites;

		public Renderer(int W, int H)
			: base(new VideoMode((uint)W, (uint)H), "The Game", Styles.Close) {
			Closed += (S, E) => Close();

			Sprites = new List<Sprite>();
			Random Rand = new Random();

			for (int i = 0; i < 4; i++) {
				Sprite S = new Sprite(new Texture("data/textures/car.png"));
				S.Position = new Vector2f((float)W / 4 * i, (float)H / 5);
				S.Scale = new Vector2f(0.65f, 0.65f);
				S.Color = new Color((byte)Rand.Next(0, 255), (byte)Rand.Next(0, 255), (byte)Rand.Next(0, 255));

				Sprites.Add(S);
			}
		}

		public void Update(float T) {

		}

		public void Render() {
			Clear(Color.Black);
			for (int i = 0; i < Sprites.Count; i++)
				Draw(Sprites[i]);

			Display();
		}
	}
}
