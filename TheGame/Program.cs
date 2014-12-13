namespace TheGame {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using SFML.Graphics;
    using SFML.Window;
    using TheGame.States;


    class Program {
        static void Main(string[] args) {

            var game = new GameObject();

            game.Run();
        }

    }
}