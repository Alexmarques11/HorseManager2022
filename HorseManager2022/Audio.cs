using System.Media;
using System.Reflection;

namespace HorseManager2022
{
    static class Audio
    {
        // Properties
        public static bool isSoundOn = false;

        public static Stream GetAudioStream(string songName) => Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(Audio).Namespace + ".Resources." + songName) ?? throw new Exception("Song not found");

        private static void PlaySong(string songName, bool isLooping = false)
        {
            if (!isSoundOn)
                return;
            
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

        public static void PlayLootboxSong() => PlaySong("Lootbox.wav", true);

        public static void PlayCommonSong() => PlaySong("Common.wav", true);

        public static void PlayRareSong() => PlaySong("Rare.wav", true);

        public static void PlayEpicSong() => PlaySong("Epic.wav", true);

        public static void PlayLegendarySong() => PlaySong("Legendary.wav", true);
    }
}
