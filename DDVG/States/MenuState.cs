﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

using SFML;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace TheGame.States {
	class MenuState : State {
        Font f;
        Music soundTrack;
        bool fontLoaded;
        bool soundtrackLoaded;
        RectangleShape startBounds = new RectangleShape();

        public override void Render(Renderer R)
        {
            R.Clear(Color.White);
            
            
            if (File.Exists(Directory.GetCurrentDirectory() + "/data/fonts/framd.ttf") && fontLoaded != true)
            {
                Console.WriteLine("Font found, loading...");
                f = new Font(Directory.GetCurrentDirectory() + "/data/fonts/framd.ttf");
                fontLoaded = true;
            }
            else if(fontLoaded != true)
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

            Text start = new Text();
            start.Font = f;
            start.Style = Text.Styles.Bold;
            start.CharacterSize = 30;
            start.DisplayedString = "Start";
            start.Color = Color.Black;
            start.Position = new Vector2f(375f, 200f);
            startBounds.Position = start.Position;
            startBounds.Size = new Vector2f(50f, 20f);
            
            R.Draw(start);
            //R.Draw(startBounds);

            Text options = new Text();
            options.Font = f;
            options.Style = Text.Styles.Bold;
            options.CharacterSize = 30;
            options.DisplayedString = "Options";
            options.Color = Color.Black;
            options.Position = new Vector2f(360f, 300f);
            R.Draw(options);

            Text quit = new Text();
            quit.Font = f;
            quit.Style = Text.Styles.Bold;
            quit.CharacterSize = 30;
            quit.DisplayedString = "Quit";
            quit.Color = Color.Black;
            quit.Position = new Vector2f(380f, 400f);
            R.Draw(quit);
        }

	}
}