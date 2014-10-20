using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform;

using SFML.Graphics;
using SFML.Window;
using TheGame.States;

namespace TheGame {
	class Renderer : RenderWindow {
		public State ActiveState;
		public Stopwatch GameTime;

		public Renderer(int W, int H)
			: base(new VideoMode((uint)W, (uint)H), "The Game", Styles.Close) {
			Terminal.Print("Initializing OpenTK context ... ");
			Error.GLTry(() => {
				IWindowInfo Inf = Utilities.CreateWindowsWindowInfo(this.SystemHandle);
				GraphicsContext Ctx = new GraphicsContext(GraphicsMode.Default, Inf);
				Ctx.MakeCurrent(Inf);
				Ctx.LoadAll();
				Terminal.PrintLn("OK");
			});

			Terminal.PrintLn("Setting up OpenGL");
			GL.Enable(EnableCap.StencilTest);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GameTime = new Stopwatch();
			GameTime.Start();

			Closed += (S, E) => Close();
			TextEntered += (S, E) => ActiveState.TextEntered(E.Unicode);
			KeyPressed += (S, E) => ActiveState.Key(E, true);
			KeyReleased += (S, E) => ActiveState.Key(E, false);
			MouseMoved += (S, E) => ActiveState.MouseMove(E);
			MouseButtonPressed += (S, E) => ActiveState.MouseClick(E, true);
			MouseButtonReleased += (S, E) => ActiveState.MouseClick(E, false);
		}

		public void SwitchState(State NewState) {
			if (ActiveState != null)
				ActiveState.Deactivate(NewState);
			NewState.Activate(ActiveState);
			ActiveState = NewState;
		}

		public void Update(float T) {
			AudioMgr.Update(T);
			ActiveState.Update(T);
		}

		public void Render() {
			ActiveState.Render(this);
			Display();
		}

		public new void Clear(Color Clr) {
			GL.Clear(ClearBufferMask.StencilBufferBit);
			base.Clear(Clr);
		}

		public void AlphaMask(Sprite S, Texture Alpha) {
			GL.BlendFuncSeparate(BlendingFactorSrc.Zero, BlendingFactorDest.One, BlendingFactorSrc.SrcColor, BlendingFactorDest.Zero);

			// Swap to alpha texture and draw it
			Texture Orig = S.Texture;
			S.Texture = Alpha;
			Draw(S);

			GL.BlendFunc(BlendingFactorSrc.DstAlpha, BlendingFactorDest.OneMinusDstAlpha);

			// Swap to normal texture and draw with alpha of alpha texture
			S.Texture = Orig;
			Draw(S);

			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}
	}
}