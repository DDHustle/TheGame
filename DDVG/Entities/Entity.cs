using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace TheGame.Entities {
	class Entity {
		public virtual Vector2f Position {
			get;
			set;
		}

		/*public virtual bool DrawOnce {
			get {
				return true;
			}
		}

		internal sbyte FrameByte;

		public Entity() {
			FrameByte = -1;
		}

		public virtual bool ShouldDraw(Renderer R) {
			bool Ret = FrameByte == R.FrameByte;
			FrameByte = R.FrameByte;

			if (Ret && DrawOnce)
				return false;
			return true;
		}*/

		public virtual void Update(float T) {
		}

		public virtual void Render(Renderer R) {
		}
	}
}
