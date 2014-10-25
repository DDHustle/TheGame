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
		public sbyte FrameByte;

		public RenderTexture LightBuffer;
		internal Sprite BufferSprite;

		public Renderer(uint W, uint H)
			: base(new VideoMode(W, H), "The Game", Styles.Close) {
			GameTime = new Stopwatch();
			GameTime.Start();

			LightBuffer = new RenderTexture(Size.X, Size.Y);
			BufferSprite = new Sprite(LightBuffer.Texture);

			Closed += (S, E) => Close();
			TextEntered += (S, E) => ActiveState.TextEntered(E.Unicode);
			KeyPressed += (S, E) => ActiveState.Key(E, true);
			KeyReleased += (S, E) => ActiveState.Key(E, false);
			MouseMoved += (S, E) => ActiveState.MouseMove(E);
			MouseButtonPressed += (S, E) => ActiveState.MouseClick(E, true);
			MouseButtonReleased += (S, E) => ActiveState.MouseClick(E, false);
		}

		public void Init() {
			Console.Write("Initializing OpenTK context ... ");
			IWindowInfo Inf = Utilities.CreateWindowsWindowInfo(this.SystemHandle);
			GraphicsContext Ctx = new GraphicsContext(GraphicsMode.Default, Inf);
			Ctx.MakeCurrent(Inf);
			Ctx.LoadAll();
			Console.WriteLine("OK");

			/*Console.WriteLine("Setting up OpenGL");
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);*/
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

			SetTitle("FrameTime: " + (T * 1000).ToString() + " ms");
		}

		public void RenderRT(RenderTexture RT, bool SuppressDisplay = false) {
			if (!SuppressDisplay)
				RT.Display();
			View V = GetView();
			SetView(DefaultView);
			BufferSprite.Texture = RT.Texture;
			base.Draw(BufferSprite);
			SetView(V);
		}

		public void Render() {
			if (FrameByte == 1)
				FrameByte = 2;
			else
				FrameByte = 1;

			SetActive(true);
			ActiveState.OnRender(this);
			Display();
		}

		public new void Clear(Color Clr) {
			GL.Clear(ClearBufferMask.StencilBufferBit);
			base.Clear(Clr);
		}

		public void AlphaMask(Sprite S, Texture Alpha) {
			GL.BlendFuncSeparate(BlendingFactorSrc.Zero, BlendingFactorDest.One, BlendingFactorSrc.SrcColor, BlendingFactorDest.Zero);
			Texture Orig = S.Texture;
			S.Texture = Alpha;
			Draw(S);
			GL.BlendFunc(BlendingFactorSrc.DstAlpha, BlendingFactorDest.OneMinusDstAlpha);
			S.Texture = Orig;
			Draw(S);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}
	}
}