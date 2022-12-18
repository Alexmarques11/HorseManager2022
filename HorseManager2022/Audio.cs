using System.Media;
using System.Reflection;

namespace HorseManager2022
{
    static class Audio
    {
        public static Stream GetAudioStream(string songName) => Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(Audio).Namespace + ".Resources." + songName) ?? throw new Exception("Song not found");

        private static void PlaySong(string songName, bool isLooping = false)
        {
            Stream audioStream = GetAudioStream(songName);
            SoundPlayer player = new(audioStream);

            if (isLooping)
                player.PlayLooping();
            else
                player.Play();
        }

        public static void PlayRaceSong() => PlaySong("RaceSong.wav");
        
        public static void PlayRaceEndSong() => PlaySong("Final.wav");
        
        public static void PlayTownSong() => PlaySong("TownSong.wav", true);
    }
}
