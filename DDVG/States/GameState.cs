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

using OpenTK.Graphics.OpenGL4;

using TheGame.GUI;
using TheGame.Entities;

namespace TheGame.States {
	class GameState : State {
		Color ClearColor;

		List<Entity> Ents;
		List<Light> Lights;

		bool W, A, S, D;
		World Wrld;

		Light MLight;

		public GameState(Renderer R)
			: base(R) {
			ClearColor = new Color(50, 12, 12); // Something redish

			Ents = new List<Entity>();
			Lights = new List<Light>();

			MLight = new Light(new Vector2f(0, 0), 200, Color.Yellow);

			/*Lights.Add(new Light(new Vector2f(250, 250), 500, Color.Red));
			Lights.Add(new Light(new Vector2f(180, 350), 500, Color.Green));
			Lights.Add(new Light(new Vector2f(320, 350), 500, Color.Blue));*/
			Lights.Add(MLight);

			Wrld = new World();
		}

		public T AddEntity<T>(T E) where T : Entity {
			Ents.Add(E);
			return E;
		}

		public void RemoveEntity(Entity E) {
			Ents.Remove(E);
		}

		public override void MouseMove(MouseMoveEventArgs E) {
			MLight.Position = View.ToWorld(E.ToVector2f());
			base.MouseMove(E);
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

			base.Key(K, Down);
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

			View.Move(new Vector2f(SpeedX * T, SpeedY * T));

			for (int i = 0; i < Ents.Count; i++)
				Ents[i].Update(T);
			base.Update(T);
		}

		public override void Render(Renderer R) {
			R.Clear(ClearColor);
			Wrld.Render(R, View);

			for (int i = 0; i < Ents.Count; i++)
				Ents[i].Render(R);

			R.PushGLStates();
			R.LightBuffer.Clear(new Color(12, 12, 15));
			R.LightBuffer.SetView(View);
			GL.BlendEquation(BlendEquationMode.Max);

			for (int i = 0; i < Lights.Count; i++)
				Lights[i].Render(Wrld, View, R);

			R.SetActive(true);
			GL.BlendFunc(BlendingFactorSrc.Zero, BlendingFactorDest.SrcColor);
			GL.BlendEquation(BlendEquationMode.FuncAdd);
			R.RenderRT(R.LightBuffer);
			R.PopGLStates();

			base.Render(R);
		}
	}
}