using System;
using System.IO;
using NAudio.Vorbis;
using NAudio.Wave;

namespace FileOrganizer2.Models
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class SoundPlayer : IDisposable
    {
        private readonly WaveOutEvent waveOut = new ();
        private WaveStream reader;

        public SoundPlayer()
        {
            waveOut.PlaybackStopped += WaveOutOnPlaybackStopped;
        }

        public bool Playing { get; set; }

        /// <summary>
        /// 指定したファイルパスの音声ファイルを再生します。
        /// </summary>
        /// <param name="filePath">再生する音声ファイルのパス。</param>
        public void PlayAudio(string filePath)
        {
            waveOut.Stop();

            var f = new FileInfo(filePath);

            reader = f.Extension switch
            {
                ".ogg" => new VorbisWaveReader(filePath),
                ".mp3" => new Mp3FileReader(filePath),
                ".wav" => new WaveFileReader(filePath),
                _ => null,
            };

            if (reader == null)
            {
                return;
            }

            Playing = true;
            waveOut.Init(reader);
            waveOut.Play();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            reader.Dispose();
            waveOut.Dispose();
        }

        private void WaveOutOnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            Playing = false;
            System.Diagnostics.Debug.WriteLine($"stop (SoundPlayer : 68)");
        }
    }
}