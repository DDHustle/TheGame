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

		List<RStateEntity> RStateEnts;
		List<Entity> Ents;

		bool W, A, S, D;
		World Wrld;

		public GameState(Renderer R)
			: base(R) {
			ClearColor = new Color(12, 12, 12);

			RStateEnts = new List<RStateEntity>();
			Ents = new List<Entity>();

			RStateEnts.Add(new DefaultRStateEntity());
			Wrld = AddEntity(new World());
		}

		public T AddEntity<T>(T E) where T : Entity {
			Ents.Add(E);
			return E;
		}

		public void RemoveEntity(Entity E) {
			Ents.Remove(E);
		}

		public override void Key(KeyEventArgs K, bool Down) {
			if (K.Code == Keyboard.Key.W) // TODO: Strip off, better input system
				W = Down;
			if (K.Code == Keyboard.Key.S)
				S = Down;
			if (K.Code == Keyboard.Key.A)
				A = Down;
			if (K.Code == Keyboard.Key.D)
				D = Down;
		}

		public override void Update(float T) {
			float SpeedX = 0;
			float SpeedY = 0;
			float Speed = 200;

			if (W)
				SpeedY -= Speed;
			if (S)
				SpeedY += Speed;
			if (A)
				SpeedX -= Speed;
			if (D)
				SpeedX += Speed;

			Wrld.V.Move(new Vector2f(SpeedX * T, SpeedY * T));

			for (int i = 0; i < Ents.Count; i++)
				Ents[i].Update(T);
			base.Update(T);
		}

		public override void Render(Renderer R) {
			R.Clear(ClearColor);
			for (int i = 0; i < RStateEnts.Count; i++)
				RStateEnts[i].Render(R, Ents);
			base.Render(R);
		}
	}
}