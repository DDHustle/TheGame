using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

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

        public MenuState(Renderer R)
        {
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
            }
            else if (soundtrackLoaded != true)
            {
                Console.WriteLine("Could not find soundtrack file...");
            }

            StartButton = new Button(R, new Vector2f(375f, 200f), new Vector2f(95f, 50f), "Start", f);
            StartButton.MouseEnter += (S, E) => MouseEnterStartBtn(S, E);
            OptionsButton = new Button(R, new Vector2f(365f, 300f), new Vector2f(120f, 50f), "Options", f);
            QuitButton = new Button(R, new Vector2f(385f, 400f), new Vector2f(85f, 50f), "Quit", f);

            R.MouseMoved += (S, E) => MouseMoved(E);
        }

        public override void Render(Renderer R)
        {
            R.Clear(Color.White);
            StartButton.Render(R);
            OptionsButton.Render(R);
            QuitButton.Render(R);
        }

        private void MouseEnterStartBtn(object sender, MouseMoveEventArgs e)
        {

        }

        private void MouseMoved(MouseMoveEventArgs e)
        {
            
        }

	}
}