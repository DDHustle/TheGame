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

using DDVG.GUI;

namespace DDVG.States {
    class MenuState : State {
        Music SoundTrack;

        public MenuState(GameObject R)
            : base(R.Window) {
            SoundTrack = AudioMgr.GetMusic("soundtrack.ogg");

            ButtonOutlined StartButton = new ButtonOutlined(UI, "Start", FontMgr.GetFont("framd.ttf"));
            StartButton.Position = new Vector2f(375f, 200f);
            StartButton.Size = new Vector2f(95f, 50f);
            StartButton.MouseClick += (B, X, Y, Down) => {
                if (!Down)
                    R.SwitchState(new GameState(R.Window));
            };

            ButtonOutlined OptionsButton = new ButtonOutlined(UI, "Options", FontMgr.GetFont("framd.ttf"));
            OptionsButton.Position = new Vector2f(365f, 300f);
            OptionsButton.Size = new Vector2f(120f, 50f);
            OptionsButton.MouseClick += (B, X, Y, Down) => {
                Console.WriteLine("You clicked on the fucking settings button. Are you happy now?");
            };

            ButtonOutlined QuitButton = new ButtonOutlined(UI, "Quit", FontMgr.GetFont("framd.ttf"));
            QuitButton.Position = new Vector2f(385f, 400f);
            QuitButton.Size = new Vector2f(85f, 50f);
            QuitButton.MouseClick += (B, X, Y, Down) => {
                if (!Down)
                    R.Window.Close();
            };
        }

        public override void Deactivate(State NewState) {
            // Temporarily disabled both
            //AudioMgr.DoOperation(AudioMgr.MusicOp.FadeOut, SoundTrack, 100);
            base.Deactivate(NewState);
        }

        public override void Activate(State OldState) {
            /*SoundTrack.Loop = true;
            AudioMgr.DoOperation(AudioMgr.MusicOp.FadeIn, SoundTrack, 100);*/
            base.Activate(OldState);
        }

        public override void Render(GameObject R) {
            R.Window.Clear(Color.White);
            base.Render(R);
        }
    }
}