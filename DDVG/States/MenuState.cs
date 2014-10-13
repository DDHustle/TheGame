using System;
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

using TheGame.GUI;

namespace TheGame.States {
	class MenuState : State {
        Font f;
        Music soundTrack;
        bool fontLoaded;
        bool soundtrackLoaded;
        Button StartButton;
        Button OptionsButton;
        Button QuitButton;
        Renderer renderer;

        //These variables are used by the Update function
        int Elapsed = 0;
        int Goal = 9999999;
        Button target;
        bool countingUpStart;
        bool countingUpOpts;
        bool countingUpQuit;
        Sound tick;
        

        public MenuState(Renderer R)
        {
            
            renderer = R;
            if (File.Exists(Directory.GetCurrentDirectory() + "/data/fonts/framd.ttf") && fontLoaded != true)
            {
                Console.WriteLine("Font found, loading...");
                f = new Font(Directory.GetCurrentDirectory() + "/data/fonts/framd.ttf");
                fontLoaded = true;
                
            }
            else if (fontLoaded != true)
            {
                Console.WriteLine("Could not find font file...");
            }

            if (File.Exists(Directory.GetCurrentDirectory() + "/data/sounds/soundtrack.ogg") && soundtrackLoaded != true)
            {
                Console.WriteLine("Soundtrack found");
                soundTrack = new Music(Directory.GetCurrentDirectory() + "/data/sounds/soundtrack.ogg");
                soundTrack.Play();
                soundTrack.Loop = true;
                soundtrackLoaded = true;

                SoundBuffer soundBuffer = new SoundBuffer(Directory.GetCurrentDirectory() + "/data/sounds/tick.wav");
                tick = new Sound(soundBuffer);
            }
            else if (soundtrackLoaded != true)
            {
                Console.WriteLine("Could not find soundtrack file...");
            }

            StartButton = new Button(R, new Vector2f(375f, 200f), new Vector2f(95f, 50f), "Start", f);
            StartButton.MouseEnter += (S, E) => MouseEnterBtn(S, E);
            StartButton.MouseLeave += (S, E) => MouseLeaveBtn(S, E);
            StartButton.MouseRelease += StartButton_MouseRelease;
            OptionsButton = new Button(R, new Vector2f(365f, 300f), new Vector2f(120f, 50f), "Options", f);
            OptionsButton.MouseEnter += (S, E) => MouseEnterBtn(S, E);
            OptionsButton.MouseLeave += (S, E) => MouseLeaveBtn(S, E);
            QuitButton = new Button(R, new Vector2f(385f, 400f), new Vector2f(85f, 50f), "Quit", f);
            QuitButton.MouseEnter += (S, E) => MouseEnterBtn(S, E);
            QuitButton.MouseLeave += (S, E) => MouseLeaveBtn(S, E);
            R.MouseMoved += (S, E) => MouseMoved(E);
        }

        private void StartButton_MouseRelease(object sender, EventArgs e)
        {
            renderer.SwitchState(new GameState());
        }

        private void MouseEnterBtn(object S, EventArgs E)
        {
            if(((Button)S).soundEffectPlayed != true)
            {
                ((Button)S).soundEffectPlayed = true;
                tick.Play();
            }
            ((Button)S).increasingInSize = true;
            target = (Button)S;
        }

        private void MouseLeaveBtn(object S, EventArgs E)
        {
            ((Button)S).increasingInSize = false;
            ((Button)S).soundEffectPlayed = false;
        }

        private void StartBtnClick(object S, EventArgs E)
        {

        }


        private void MouseMoved(MouseMoveEventArgs e)
        {
            
        }

        public override void Render(Renderer R)
        {
            R.Clear(Color.White);
            StartButton.Render(R);
            OptionsButton.Render(R);
            QuitButton.Render(R);
        }

        public override void Update(float T)
        {
            if (target != null)
            {
               // Console.WriteLine(target.TextStr);
                if (target.increasingInSize && Elapsed <= Goal && target.rect.OutlineThickness < 20)
                {
                    Elapsed += 1;
                    if (Elapsed % 100 == 0) { target.rect.OutlineThickness += 1; }
                }
                else if (target.increasingInSize == false && Elapsed < Goal && target.rect.OutlineThickness > 4)
                {
                    Elapsed += 1;
                    if (Elapsed % 109 == 0) { target.rect.OutlineThickness -= 1; }
                }
                else if (Elapsed > Goal)
                {
                    Elapsed = 0;
                    target.increasingInSize = false;
                }
            }
            
        }
	}
}