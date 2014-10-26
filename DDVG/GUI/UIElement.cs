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
		LinkedList<UIElement> Elements;
		UIElement[] ElementsFlat;

		UIElement Parent;

		public Renderer Renderer {
			get {
				return UI.Renderer;
			}
		}

		public delegate void MouseDel(int X, int Y);
		public delegate void KeyPressedDel(Keyboard.Key Code, bool Ctrl, bool Shift, bool Alt, bool System, bool Down);
		public delegate void MouseClickDel(Mouse.Button B, int X, int Y, bool Down);
		public delegate void TextEnteredDel(string In);

		public event MouseDel MouseEnter;
		public event MouseDel MouseLeave;
		public event MouseClickDel MouseClick;
		public event MouseDel MouseMove;
		public event KeyPressedDel KeyPressed;
		public event TextEnteredDel TextEntered;

		public bool MouseInside {
			get;
			internal set;
		}

		public virtual bool Focused {
			get {
				return FocusedElement == this;
			}
			set {
				if (value) {
					FocusedElement = this;
					Active = true;
					Parent.ToFront(this);
				} else
					UI.Focused = true;
			}
		}

		public virtual UIElement FocusedElement {
			get {
				return Parent.FocusedElement;
			}
			set {
				Parent.FocusedElement = value;
			}
		}

		public virtual bool Active {
			get;
			set;
		}

		public virtual GUIBase UI {
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
			Elements = new LinkedList<UIElement>();
			this.UI = UI;
			Active = true;
		}

		public void AddElement(UIElement E) {
			if (E.Parent != null)
				throw new Exception("Cannot add owned element");
			E.Parent = this;

			Elements.AddFirst(E);
			ElementsFlat = GetElements();
		}

		public void RemoveElement(UIElement E) {
			if (E.Parent != this || !Elements.Contains(E))
				throw new Exception("Cannot remove not owned element");
			E.Parent = null;

			while (Elements.Contains(E))
				Elements.Remove(E);

			ElementsFlat = GetElements();
		}

		public void ToFront(UIElement E) {
			RemoveElement(E);
			AddElement(E);
		}

		public UIElement[] GetElements() {
			return Elements.ToArray();
		}

		public bool IsInside(float X, float Y) {
			return X > Position.X && Y > Position.Y && X < (Position.X + Size.X) && Y < (Position.Y + Size.Y);
		}

		public virtual void Update(float T) {
			if (!Active)
				return;

			foreach (UIElement E in Elements)
				if (E.Active)
					E.Update(T);
		}


		public virtual void OnRender(Renderer R) {
			if (!Active)
				return;
			PreRender(R);
			Render(R);
			PostRender(R);
		}

		public virtual void PreRender(Renderer R) {
			for (int i = Elements.Count - 1; i >= 0; i--)
				if (ElementsFlat[i].Active)
					ElementsFlat[i].PreRender(R);
		}

		public virtual void Render(Renderer R) {
			for (int i = Elements.Count - 1; i >= 0; i--)
				if (ElementsFlat[i].Active)
					ElementsFlat[i].Render(R);
		}

		public virtual void PostRender(Renderer R) {
			for (int i = Elements.Count - 1; i >= 0; i--)
				if (ElementsFlat[i].Active)
					ElementsFlat[i].PostRender(R);
		}

		public virtual void OnMouseEnter(int X, int Y) {
			if (!Active)
				return;

			if (MouseEnter != null)
				MouseEnter(X, Y);
		}

		public virtual void OnMouseLeave(int X, int Y) {
			if (!Active)
				return;

			if (MouseLeave != null)
				MouseLeave(X, Y);
		}

		public virtual void OnMouseClick(Mouse.Button B, int X, int Y, bool Down) {
			if (!Active)
				return;

			foreach (UIElement E in Elements)
				if (E.Active && E.IsInside(X, Y)) {
					E.OnMouseClick(B, X, Y, Down);
					break;
				}

			if (MouseClick != null)
				MouseClick(B, X, Y, Down);
		}

		public virtual void OnMouseMove(int X, int Y, bool Inside) {
			if (!Active)
				return;

			if (Inside && !MouseInside)
				OnMouseEnter(X, Y);
			else if (!Inside && MouseInside)
				OnMouseLeave(X, Y);
			MouseInside = Inside;

			bool WasIn = false;
			for (int i = 0; i < Elements.Count; i++)
				if (ElementsFlat[i].Active) {
					bool In = false;
					if (!WasIn)
						In = ElementsFlat[i].IsInside(X, Y);
					if (In)
						WasIn = true;
					ElementsFlat[i].OnMouseMove(X, Y, In);
				}

			if (Inside && MouseMove != null)
				MouseMove(X, Y);
		}

		public virtual void OnKey(Keyboard.Key Code, bool Ctrl, bool Shift, bool Alt, bool System, bool Down) {
			if (!Active)
				return;
			UIElement E = FocusedElement;
			if (E != null && E.Active && E.KeyPressed != null)
				E.KeyPressed(Code, Ctrl, Shift, Alt, System, Down);
		}

		public virtual void OnString(string Str) {
			if (!Active)
				return;
			UIElement E = FocusedElement;
			if (E != null && E.Active && E.TextEntered != null)
				E.TextEntered(Str);
		}
	}
}