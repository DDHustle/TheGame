namespace DDVG {
    using System.Diagnostics;

    using OpenTK.Graphics.OpenGL4;
    using SFML.Graphics;
    using SFML.Window;
    using DDVG.States;

    /// <summary>
    /// 
    /// </summary>
    public class Main : RenderWindow {
        /// <summary>
        /// 
        /// </summary>
        public State ActiveState;

        /// <summary>
        /// 
        /// </summary>
        public Stopwatch GameTime;

        /// <summary>
        /// 
        /// </summary>
        public sbyte FrameByte;

        /// <summary>
        /// 
        /// </summary>
        public RenderTexture LightBuffer;

        /// <summary>
        /// 
        /// </summary>
        public RenderTexture LightBuffer2;

        /// <summary>
        /// 
        /// </summary>
        internal Sprite BufferSprite;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="W"></param>
        /// <param name="H"></param>
        public Main(uint W, uint H)
            : base(new VideoMode(W, H), "The Game", Styles.Close) {
            this.GameTime = new Stopwatch();
            this.GameTime.Start();

            Console.WriteLine("OpenGL {2}.{3}, Depth: {0}, Stencil: {1}, AA: {4}", Settings.DepthBits, Settings.StencilBits, Settings.MajorVersion, Settings.MinorVersion, Settings.AntialiasingLevel);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Color"></param>
        public new void Clear(Color Color) {
            ClearStencil();
            base.Clear(Color);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearStencil() {
            GL.Clear(ClearBufferMask.StencilBufferBit);
        }

    }

}