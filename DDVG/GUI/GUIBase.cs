﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace DDVG.GUI {
    class GUIBase : UIElement {
        public new Main Renderer;

        internal UIElement InFocus;
        public override UIElement FocusedElement {
            get {
                return InFocus;
            }
            set {
                InFocus = value;
            }
        }

        public override bool Focused {
            get {
                return InFocus == this;
            }
            set {
                InFocus = this;
            }
        }

        public override GUIBase UI {
            get {
                return this;
            }
            internal set {
                base.UI = this;
            }
        }

        GConsole Con;

        public GUIBase(Main R)
            : base(null) {
            Renderer = R;
            Active = true;
            Focused = true;

            Con = new GConsole(this);
            Con.Active = false;
            AddElement(Con);

            KeyPressed += KeyPressedEvent;
        }

        void KeyPressedEvent(Keyboard.Key Code, bool Ctrl, bool Shift, bool Alt, bool System, bool Down) {
            if (Down && Code == Keyboard.Key.F1) {
                Con.Focused = true;
            }
        }
    }
}