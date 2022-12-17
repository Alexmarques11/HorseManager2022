using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace HorseManager2022
{
    static class Audio
    {
        public static string pathAudio = Directory.GetCurrentDirectory() + "\\audio";

        public static void PlayRaceSong()
        {
            SoundPlayer raceSong = new SoundPlayer(pathAudio + @"\RaceSong.wav");
            raceSong.Play();
        }

        public static void PlayRaceEndSong()
        {
            SoundPlayer raceSong = new SoundPlayer(pathAudio + @"\Final.wav");
            raceSong.Play();
        }
        public static void PlayTownSong()
        {
            SoundPlayer townSong = new SoundPlayer(pathAudio + @"\TownSong.wav");
            townSong.Play();
        }
    }
}
