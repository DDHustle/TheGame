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

		public virtual void Update(float T) {
		}

		public virtual bool InRange(RStateEntity E, float Range) {
			return InRange(E.Position, Range);
		}

		public virtual bool InRange(Vector2f Pos, float Range) {
			return Position.Distance(Pos) < Range;
		}

		public virtual void Render(Renderer R) {
		}
	}
}
