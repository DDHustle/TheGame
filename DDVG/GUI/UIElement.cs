using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.Graphics;
using SFML.Window;

namespace TheGame.GUI {
	class UIElement {
		List<UIElement> Elements;
		UIElement Parent;

		public GUIBase UI {
			get;
			internal set;
		}

		public Renderer Renderer {
			get {
				return UI.Renderer;
			}
		}

		public event Action<int, int> MouseEnter;
		public event Action<int, int> MouseLeave;
		public event Action<Mouse.Button, int, int, bool> MouseClick;
		public event Action<int, int> MouseMove;

		public bool MouseInside {
			get;
			internal set;
		}

		public virtual Vector2f Position {
			get;
			set;
		}

		public virtual Vector2f Size {
			get;
			set;
		}

		public UIElement(GUIBase UI) {
			Elements = new List<UIElement>();
			this.UI = UI;
		}

		public void AddElement(UIElement E) {
			if (E.Parent != null)
				throw new Exception("Cannot add owned element");
			E.Parent = this;
			Elements.Add(E);
		}

		public void RemoveElement(UIElement E) {
			if (E.Parent != this || !Elements.Contains(E))
				throw new Exception("Cannot remove not owned element");
			E.Parent = null;
			Elements.Remove(E);
		}

		public UIElement[] GetElements() {
			return Elements.ToArray();
		}

		public T GetTopParent<T>() where T : UIElement {
			if (Parent != null)
				return Parent.GetTopParent<T>();
			return (T)this;
		}

		public bool IsInside(float X, float Y) {
			return X > Position.X && Y > Position.Y && X < (Position.X + Size.X) && Y < (Position.Y + Size.Y);
		}

		public virtual void Update(float T) {
			foreach (UIElement E in Elements)
				E.Update(T);
		}

		public virtual void Render(Renderer R) {
			foreach (UIElement E in Elements)
				E.Render(R);
		}

		public virtual void OnMouseEnter(int X, int Y) {
			if (MouseEnter != null)
				MouseEnter(X, Y);
		}

		public virtual void OnMouseLeave(int X, int Y) {
			if (MouseLeave != null)
				MouseLeave(X, Y);
		}

		public virtual void OnMouseClick(Mouse.Button B, int X, int Y, bool Down) {
			foreach (UIElement E in Elements)
				if (E.IsInside(X, Y))
					E.OnMouseClick(B, X, Y, Down);
			if (MouseClick != null)
				MouseClick(B, X, Y, Down);
		}

		public virtual void OnMouseMove(int X, int Y, bool Inside) {
			if (Inside && !MouseInside)
				OnMouseEnter(X, Y);
			else if (!Inside && MouseInside)
				OnMouseLeave(X, Y);
			MouseInside = Inside;

			foreach (UIElement E in Elements)
				E.OnMouseMove(X, Y, E.IsInside(X, Y));

			if (Inside && MouseMove != null)
				MouseMove(X, Y);
		}
	}
}