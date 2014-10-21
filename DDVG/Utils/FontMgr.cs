using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace TheGame {
	static class FontMgr {
		static Dictionary<string, Font> Fonts;

		static FontMgr() {
			Fonts = new Dictionary<string, Font>();
		}

		public static Font GetFont(string Name) {
			if (Fonts.ContainsKey(Name))
				return Fonts[Name];
			Fonts.Add(Name, new Font("data/fonts/" + Name));
			Console.WriteLine("Loaded font data/fonts/{0}", Name);
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
