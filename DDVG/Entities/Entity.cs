using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.Entities {
	class Entity {
		public virtual void Update(float T) {
		}

		public virtual void Render(Renderer R) {
		}
	}
}
