using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.Entities {
	class RStateEntity {
		public virtual void Render(Renderer R, List<Entity> Ents) {
		}
	}

	class DefaultRStateEntity : RStateEntity {
		public DefaultRStateEntity() {
		}

		public override void Render(Renderer R, List<Entity> Ents) {
			for (int i = 0; i < Ents.Count; i++)
				Ents[i].Render(R);

			base.Render(R, Ents);
		}
	}
}