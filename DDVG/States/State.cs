using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.GUI;

using SFML.Graphics;
using SFML.Window;

namespace TheGame.States {
	class State {
		internal Renderer RendererBase;
		internal GUIBase UI;

		public View View;

		public State(Renderer R) {
			RendererBase = R;
			UI = new GUIBase(R);
			View = new View(R.GetView());
		}

		public virtual void Deactivate(State NewState) {
		}

		public virtual void Activate(State OldState) {
		}

		public virtual void TextEntered(string S) {
		}

		public virtual void Key(KeyEventArgs K, bool Down) {
		}

		public virtual void MouseMove(MouseMoveEventArgs E) {
			UI.OnMouseMove(E.X, E.Y, true);
		}

		public virtual void MouseClick(MouseButtonEventArgs E, bool Down) {
			UI.OnMouseClick(E.Button, E.X, E.Y, Down);
		}

		public virtual void Update(float T) {
			UI.Update(T);
		}

		public virtual void OnRender(Renderer R) {
			PreRender(R);
			Render(R);
			PostRender(R);
		}

		public virtual void PreRender(Renderer R) {
			R.SetView(View);
		}

		public virtual void Render(Renderer R) {
			UI.Render(R);
		}

		public virtual void PostRender(Renderer R) {

		}
	}
}