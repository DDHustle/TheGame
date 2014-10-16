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
	static class AudioMgr {
		public enum MusicOp {
			FadeOut,
			FadeIn,
		}

		static Dictionary<string, Music> Musics; // Intentional
		static Dictionary<string, Sound> Sounds;

		static List<Tuple<MusicOp, float, Music>> OpList;
		static List<Tuple<MusicOp, float, Music>> OpQueue;

		static AudioMgr() {
			Musics = new Dictionary<string, Music>();
			Sounds = new Dictionary<string, Sound>();

			OpList = new List<Tuple<MusicOp, float, Music>>();
			OpQueue = new List<Tuple<MusicOp, float, Music>>();
		}


		public static void Update(float T) {
			if (OpList.Count > 0) {
				foreach (var O in OpList) {
					if (O.Item1 == MusicOp.FadeOut) {
						if (O.Item3.Volume <= 1) {
							O.Item3.Loop = false;
							O.Item3.Stop();
							OpQueue.Add(O);
						} else
							O.Item3.Volume -= O.Item2 * T;
					} else if (O.Item1 == MusicOp.FadeIn) {
						if (O.Item3.Volume >= 100)
							OpQueue.Add(O);
						else {
							if (O.Item3.Status != SoundStatus.Playing)
								O.Item3.Play();
							O.Item3.Volume += O.Item2 * T;
						}
					}
				}

				if (OpQueue.Count > 0) {
					foreach (var Q in OpQueue)
						OpList.Remove(Q);
					OpQueue.Clear();
				}
			}
		}

		public static Music GetMusic(string Name) {
			if (Musics.ContainsKey(Name))
				return Musics[Name];
			Musics.Add(Name, new Music("data/sounds/" + Name));
			Terminal.PrintLn("Loaded music data/sounds/{0}", Name);
			return GetMusic(Name);
		}

		public static Sound GetSound(string Name) {
			if (Sounds.ContainsKey(Name))
				return Sounds[Name];
			Sounds.Add(Name, new Sound(new SoundBuffer("data/sounds/" + Name)));
			Terminal.PrintLn("Loaded sound data/sounds/{0}", Name);
			return GetSound(Name);
		}

		public static void UnloadMusic() {
			foreach (var M in Musics)
				M.Value.Dispose();
			Musics = new Dictionary<string, Music>();
		}

		public static void UnloadSound() {
			foreach (var S in Sounds)
				S.Value.Dispose();
			Sounds = new Dictionary<string, Sound>();
		}

		public static void PlayOnce(Sound S) {
			if (S.Status != SoundStatus.Stopped)
				S.Stop();
			S.Loop = false;
			S.Play();
		}

		public static void DoOperation(MusicOp O, Music M, float Amount) {
			if (O == MusicOp.FadeIn)
				M.Volume = 1;
			OpList.Add(new Tuple<MusicOp, float, Music>(O, Amount, M));
		}
	}
}