using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDVG.GUI;

using SFML.Graphics;
using SFML.Window;

namespace DDVG.States {
    public class State {
        internal Main RendererBase;
        internal GUIBase UI;

        public View View;

        public State(Main R) {
            RendererBase = R;
            UI = new GUIBase(R);
            View = new View(R.GetView());
        }

        public virtual void Deactivate(State NewState) {
        }

        public virtual void Activate(State OldState) {
        }

        public virtual void TextEntered(string S) {
            UI.OnString(S);
        }

        public virtual void Key(KeyEventArgs K, bool Down) {
            UI.OnKey(K.Code, K.Control, K.Shift, K.Alt, K.System, Down);
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

        public virtual void OnRender(GameObject R) {
            PreRender(R);
            Render(R);
            PostRender(R);
        }

        public virtual void PreRender(GameObject R) {
            R.Window.SetView(View);
        }

        public virtual void Render(GameObject R) {
            UI.OnRender(R.Window);
        }

        public virtual void PostRender(GameObject R) {

        }
    }
}