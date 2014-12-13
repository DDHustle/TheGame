namespace DDVG {
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

    static class GraphicsMgr {
        static Dictionary<string, Texture> Textures;
        static string BasePath = "data/textures";

        static GraphicsMgr() {
            Textures = new Dictionary<string, Texture>();

            if (!Settings.LazyLoading) {
                string[] Files = Directory.GetFiles(BasePath, "*.*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < Files.Length; i++) {
                    try {
                        GetTexture(Path.GetFileName(Files[i]));
                    } catch (Exception E) {
                        Console.WriteLine(E.Message);
                    }
                }
            }
        }

        public static Texture GetTexture(string Name) {
            if (Textures.ContainsKey(Name))
                return Textures[Name];
            Textures.Add(Name, new Texture(Path.Combine(BasePath, Name)));
            Console.WriteLine("Loaded texture {0}", Path.Combine(BasePath, Name));
            return GetTexture(Name);
        }

        public static void UnloadTextures() {
            foreach (var T in Textures)
                T.Value.Dispose();
            Textures = new Dictionary<string, Texture>();
        }
    }
}