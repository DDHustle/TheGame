using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using SFML;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace DDVG {
    static class FontMgr {
        static Dictionary<string, Font> Fonts;
        static string BasePath = "data/fonts";

        static FontMgr() {
            Fonts = new Dictionary<string, Font>();

            if (!Settings.LazyLoading) {
                string[] Files = Directory.GetFiles(BasePath, "*.*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < Files.Length; i++) {
                    try {
                        GetFont(Path.GetFileName(Files[i]));
                    } catch (Exception E) {
                        Console.WriteLine(E.Message);
                    }
                }
            }
        }

        public static Font GetFont(string Name) {
            if (Fonts.ContainsKey(Name))
                return Fonts[Name];
            Fonts.Add(Name, new Font(Path.Combine(BasePath, Name)));
            Console.WriteLine("Loaded font {0}", Path.Combine(BasePath, Name));
            return GetFont(Name);
        }

        public static void UnloadFonts() {
            foreach (var F in Fonts)
                F.Value.Dispose();
            Fonts = new Dictionary<string, Font>();
        }

        public static Vector2f GetSize(Text T) {
            FloatRect B = T.GetLocalBounds();
            return new Vector2f(B.Width, B.Height);
        }
    }
}