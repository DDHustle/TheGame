using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;

namespace TheGame.Entities {
	class RStateEntity {
		public virtual Vector2f Position {
			get;
			set;
		}

		public virtual void Render(Renderer R, List<Entity> Ents) {
		}
	}

	class DefaultRStateEntity : RStateEntity {
		public DefaultRStateEntity() {
		}

		public override void Render(Renderer R, List<Entity> Ents) {
			View V = R.GetView();
			for (int i = 0; i < Ents.Count; i++) {
				if (Ents[i].InRange(V.Center, (V.Size.X.Pow() + V.Size.Y.Pow()).Sqrt() / 2))
					Ents[i].Render(R);
			}

			base.Render(R, Ents);
		}
	}
}