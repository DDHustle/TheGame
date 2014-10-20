using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace TheGame {
	public static class Error {
		
		public static void GLTry(Action A) {
			try {
				A();
			} catch (Exception E) {
				throw new Exception(GL.GetError().ToString(), E);
			}
		}
	}
}