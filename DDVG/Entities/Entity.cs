using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;
using SFML.Graphics;
using SFML.Window;

namespace TheGame.Entities {
	class Entity {
		public virtual Vector2f Position {
			get;
			set;
		}

		public virtual Polymesh GetMesh() {
			return Polymesh.Empty;
		}

		public virtual void Update(float T) {
		}

		public virtual void Render(Renderer R) {
		}
	}
}
