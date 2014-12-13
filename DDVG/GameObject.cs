namespace DDVG {
    using System;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Platform;
    using SFML.Graphics;
    using SFML.Window;
    using DDVG.States;

    /// <summary>
    /// 
    /// </summary>
    public class GameObject {
        /// <summary>
        /// 
        /// </summary>
        private Main _window;

        /// <summary>
        /// 
        /// </summary>
        public Main Window {
            get {
                return this._window;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GameObject() {
            _window = new Main(800, 600);

            _window.SetVisible(true);
            _window.SetVerticalSyncEnabled(true);

            _window.LightBuffer = new RenderTexture(_window.Size.X, _window.Size.Y);
            _window.LightBuffer2 = new RenderTexture(_window.Size.X, _window.Size.Y);
            _window.BufferSprite = new Sprite(_window.LightBuffer.Texture);

            // Set up event handlers
            _window.Closed += _window_Closed;
            _window.KeyPressed += _window_KeyPressed;

            _window.Closed += (S, E) => _window.Close();
            _window.TextEntered += (S, E) => _window.ActiveState.TextEntered(E.Unicode);
            _window.KeyPressed += (S, E) => _window.ActiveState.Key(E, true);
            _window.KeyReleased += (S, E) => _window.ActiveState.Key(E, false);
            _window.MouseMoved += (S, E) => _window.ActiveState.MouseMove(E);
            _window.MouseButtonPressed += (S, E) => _window.ActiveState.MouseClick(E, true);
            _window.MouseButtonReleased += (S, E) => _window.ActiveState.MouseClick(E, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewState"></param>
        public void SwitchState(State NewState) {
            if (_window.ActiveState != null) {
                _window.ActiveState.Deactivate(NewState);
            }
            NewState.Activate(_window.ActiveState);
            _window.ActiveState = NewState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="T"></param>
        public void Update(float T) {
            AudioMgr.Update(T);
            _window.ActiveState.Update(T);

            _window.SetTitle("FrameTime: " + (T * 1000).ToString() + " ms");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RT"></param>
        /// <param name="SuppressDisplay"></param>
        public void RenderRT(RenderTexture RT, bool SuppressDisplay = false) {
            RenderRTTo(RT, _window, SuppressDisplay);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RT"></param>
        /// <param name="Target"></param>
        /// <param name="SuppressDisplay"></param>
        public void RenderRTTo(RenderTexture RT, RenderTarget Target, bool SuppressDisplay = false) {
            if (!SuppressDisplay) {
                RT.Display();
            }

            View view = _window.GetView();
            Target.SetView(_window.DefaultView);
            _window.BufferSprite.Texture = RT.Texture;
            Target.Draw(_window.BufferSprite);
            Target.SetView(view);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Render() {
            if (_window.FrameByte == 1) {
                _window.FrameByte = 2;
            } else {
                _window.FrameByte = 1;
            }

            _window.SetActive(true);
            _window.ActiveState.OnRender(this);
            _window.Display();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="alpha"></param>
        public void AlphaMask(Sprite sprite, Texture alpha) {
            GL.BlendFuncSeparate(BlendingFactorSrc.Zero, BlendingFactorDest.One, BlendingFactorSrc.SrcColor, BlendingFactorDest.Zero);
            Texture Orig = sprite.Texture;
            sprite.Texture = alpha;
            _window.Draw(sprite);
            GL.BlendFunc(BlendingFactorSrc.DstAlpha, BlendingFactorDest.OneMinusDstAlpha);
            sprite.Texture = Orig;
            _window.Draw(sprite);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        void _window_KeyPressed(object sender, KeyEventArgs e) {
            //SceneManager.Instance.CurrentScene.HandleInput(e);
        }

        void _window_Closed(object sender, EventArgs e) {
            _window.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Initialize() {
            Console.Write("Initializing OpenTK context ... ");
            IWindowInfo Inf = Utilities.CreateWindowsWindowInfo(this._window.SystemHandle);
            GraphicsContext Ctx = new GraphicsContext(GraphicsMode.Default, Inf);
            Ctx.MakeCurrent(Inf);
            Ctx.LoadAll();
            Console.WriteLine("OK");
            Console.WriteLine("Setting up OpenGL");
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run() {
            Console.Title = "The Game Console";
            _window.GameTime.Start();

            this.Initialize();
            this.SwitchState(new MenuState(this));

            while (_window.IsOpen()) {
                this._window.DispatchEvents();
                while (_window.GameTime.ElapsedMilliseconds < 16)
                    ;
                this.Update((float)_window.GameTime.ElapsedMilliseconds / 1000f);
                _window.GameTime.Restart();
                this.Render();
            }

            Console.WriteLine("Quitting...");
            Environment.Exit(0); // Required
        }

    }

}
