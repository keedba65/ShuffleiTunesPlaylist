using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesLib;

namespace ShuffleiTunesPlaylist.Model
{
    internal class CTrack
    {
        public CTrack(IITTrack track)
        {
            if (track == null)
            {
                return;
            }

            Album = track.Album;
            Artist = track.Artist;
            BitRate = track.BitRate;
            BPM = track.BPM;
            Comment = track.Comment;
            Compilation = track.Compilation;
            Composer = track.Composer;
            DateAdded = track.DateAdded;
            DiscCount = track.DiscCount;
            DiscNumber = track.DiscNumber;
            Duration = track.Duration;
            Enabled = track.Enabled;
            EQ = track.EQ;
            Finish = track.Finish;
            Genre = track.Genre;
            Grouping = track.Grouping;
            Index = track.Index;
            KindAsString = track.KindAsString;
            ModificationDate = track.ModificationDate;
            Name = track.Name;
            PlayedCount = track.PlayedCount;
            PlayedDate = track.PlayedDate;
            PlayOrderIndex = track.PlayOrderIndex;
            Rating = track.Rating;
            SampleRate = track.SampleRate;
            Size = track.Size;
            Start = track.Start;
            Time = track.Time;
            TrackCount = track.TrackCount;
            TrackNumber = track.TrackNumber;
            VolumeAdjustment = track.VolumeAdjustment;
            Year = track.Year;
        }

        public string Album { get; set; }
        public string Artist { get; set; }
        public int BitRate { get; set; }
        public int BPM { get; set; }
        public string Comment { get; set; }
        public bool Compilation { get; set; }
        public string Composer { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.MinValue;
        public int DiscCount { get; set; }
        public int DiscNumber { get; set; }
        public int Duration { get; set; }
        public bool Enabled { get; set; }
        public string EQ { get; set; }
        public int Finish { get; set; }
        public string Genre { get; set; }
        public string Grouping { get; set; }
        public int Index { get; set; }
        public string KindAsString { get; set; }
        public DateTime ModificationDate { get; set; } = DateTime.MinValue;
        public string Name { get; set; }
        public int PlayedCount { get; set; }
        public DateTime PlayedDate { get; set; } = DateTime.MinValue;
        public int PlayOrderIndex { get; set; }
        public int Rating { get; set; }
        public int SampleRate { get; set; }
        public int Size { get; set; }
        public int Start { get; set; }
        public string Time { get; set; }
        public int TrackCount { get; set; }
        public int TrackNumber { get; set; }
        public int VolumeAdjustment { get; set; }
        public int Year { get; set; }
    }
}
