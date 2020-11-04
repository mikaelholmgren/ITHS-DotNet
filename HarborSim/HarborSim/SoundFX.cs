using System;
using System.Windows;
using System.Windows.Media;

namespace HarborSim
{
    public static class SoundFX
    {
        private static MediaPlayer startSound = new MediaPlayer();
        private static MediaPlayer ambienceSound = new MediaPlayer();
        private static MediaPlayer hornSound = new MediaPlayer();
        static SoundFX()
        {
            startSound.Open(new Uri(@"Sounds/BoatHorn.mp3", UriKind.Relative));
            ambienceSound.Open(new Uri(@"Sounds/HarborAmbience.mp3", UriKind.Relative));
            hornSound.Open(new Uri(@"Sounds/BoatHorn_short.mp3", UriKind.Relative));
            ambienceSound.MediaEnded += AmbienceEnded;
        }
        public static void PlayStartSound()
        {
            startSound.Play();
        }
        public static void StartAmbience()
        {
            ambienceSound.Play();
        }
        public static void PlayHorn()
        {
            hornSound.Position = TimeSpan.Zero;
            hornSound.Play();
        }
        private static void AmbienceEnded(object sender, EventArgs e)
        {
            ambienceSound.Position = TimeSpan.Zero;
            ambienceSound.Play();
        }
        public static void Close()
        {
            ambienceSound.Stop();
            startSound.Close();
            ambienceSound.Close();
            hornSound.Close();
        }
    }
}