using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

using SFML;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

using TheGame.GUI;
using TheGame.Entities;

namespace TheGame.States {
	class GameState : State {
		Color ClearColor;

		List<Entity> Ents;

		public GameState(Renderer R)
			: base(R) {
			ClearColor = new Color(12, 12, 12);

			Ents = new List<Entity>();

			AddEntity(new World());
		}

		public void AddEntity(Entity E) {
			Ents.Add(E);
		}

		public void RemoveEntity(Entity E) {
			Ents.Remove(E);
		}

		public override void Update(float T) {
			for (int i = 0; i < Ents.Count; i++)
				Ents[i].Update(T);
			base.Update(T);
		}

		public override void Render(Renderer R) {
			R.Clear(ClearColor);
			for (int i = 0; i < Ents.Count; i++)
				Ents[i].Render(R);
			base.Render(R);
		}
	}
}