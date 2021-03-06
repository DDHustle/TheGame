﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

using SFML;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

using OpenTK.Graphics.OpenGL4;

using DDVG.GUI;
using DDVG.Entities;

namespace DDVG.States {
    class GameState : State {
        Color ClearColor;

        List<Entity> Ents;
        List<Light> Lights;

        bool W, A, S, D;
        World Wrld;

        Light MLight;

        public GameState(Main R)
            : base(R) {
            ClearColor = new Color(50, 12, 12); // Something redish

            Ents = new List<Entity>();
            Lights = new List<Light>();

            MLight = new Light(new Vector2f(0, 0), 500, Color.White);

            Lights.Add(new Light(new Vector2f(350, 150), 500, Color.Red));
            //Lights.AddRange(Penumbrae(new Vector2f(350, 150), 500, Color.White));
            Lights.Add(new Light(new Vector2f(400, 550), 500, Color.Green));
            Lights.Add(new Light(new Vector2f(500, 300), 500, Color.Blue));
            Lights.Add(MLight);

            Wrld = new World();
        }

        static Light[] Penumbrae(Vector2f Pos, float Range, Color Clr) {
            Light[] Ls = new Light[3];
            float Dist = 20;
            float Ang = 0;

            Ls[0] = new Light(Pos - new Vector2f((float)Math.Cos(0 + Ang) * Dist, (float)Math.Sin(0 + Ang) * Dist), Range, Clr);
            Ls[1] = new Light(Pos - new Vector2f((float)Math.Cos(120 + Ang) * Dist, (float)Math.Sin(120 + Ang) * Dist), Range, Clr);
            Ls[2] = new Light(Pos - new Vector2f((float)Math.Cos(240 + Ang) * Dist, (float)Math.Sin(240 + Ang) * Dist), Range, Clr);
            return Ls;
        }

        public T AddEntity<T>(T E) where T : Entity {
            Ents.Add(E);
            return E;
        }

        public void RemoveEntity(Entity E) {
            Ents.Remove(E);
        }

        public override void MouseMove(MouseMoveEventArgs E) {
            MLight.Position = View.ToWorld(E.ToVector2f());
            base.MouseMove(E);
        }

        public override void Key(KeyEventArgs K, bool Down) {
            base.Key(K, Down);
            if (!UI.Focused)
                return;

            if (K.Code == Keyboard.Key.W) // TODO: Strip off, better input system
                W = Down;
            if (K.Code == Keyboard.Key.S)
                S = Down;
            if (K.Code == Keyboard.Key.A)
                A = Down;
            if (K.Code == Keyboard.Key.D)
                D = Down;
        }

        public override void Update(float T) {
            float SpeedX = 0;
            float SpeedY = 0;
            float Speed = 200;

            if (W)
                SpeedY -= Speed;
            if (S)
                SpeedY += Speed;
            if (A)
                SpeedX -= Speed;
            if (D)
                SpeedX += Speed;

            View.Move(new Vector2f(SpeedX * T, SpeedY * T));

            for (int i = 0; i < Ents.Count; i++)
                Ents[i].Update(T);
            base.Update(T);
        }

        public override void Render(GameObject renderer) {
            renderer.Window.Clear(ClearColor);
            renderer.Window.LightBuffer.SetView(View);
            renderer.Window.LightBuffer2.SetView(View);
            Wrld.Render(renderer.Window);

            for (int i = 0; i < Ents.Count; i++) {
                Ents[i].Render(renderer.Window);
            }

            renderer.Window.PushGLStates();
            renderer.Window.LightBuffer.Clear(new Color(20, 20, 24));
            GL.BlendEquation(BlendEquationMode.Max);
            for (int i = 0; i < Lights.Count; i++)
                if (Lights[i].Position.InRange(View.Center, (View.Size.X.Pow() + View.Size.Y.Pow()).Sqrt())) {
                    renderer.Window.LightBuffer2.Clear(Color.Black);
                    renderer.Window.LightBuffer2.PushGLStates();
                    Lights[i].Render(Wrld, renderer.Window.LightBuffer2);
                    Lights[i].RenderShadows(Wrld, renderer.Window.LightBuffer2);
                    Lights[i].RenderSolidLights(Wrld, renderer.Window.LightBuffer2);
                    renderer.Window.LightBuffer.SetActive(true);
                    renderer.Window.LightBuffer.PushGLStates();
                    GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
                    GL.BlendEquation(BlendEquationMode.FuncAdd);
                    renderer.RenderRTTo(renderer.Window.LightBuffer2, renderer.Window.LightBuffer);
                    renderer.Window.LightBuffer.PopGLStates();
                    renderer.Window.LightBuffer2.PopGLStates();
                }
            renderer.Window.SetActive(true);
            GL.BlendFunc(BlendingFactorSrc.DstColor, BlendingFactorDest.Zero);
            GL.BlendEquation(BlendEquationMode.FuncAdd);
            renderer.RenderRT(renderer.Window.LightBuffer);
            renderer.Window.PopGLStates();

            base.Render(renderer);
        }
    }
}