using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesLib;

namespace ShuffleiTunesPlaylist.Model
{
    class CTrack
    {
        private IITTrack _track = null;
        public CTrack(IITTrack track)
        {
            _track = track;
        }

        public string Album { get { return _track == null ? "" : _track.Album; } }
        public string Artist { get { return _track == null ? "" : _track.Artist; } }
        public int BitRate { get { return _track == null ? 0 : _track.BitRate; } }
        public int BPM { get { return _track == null ? 0 : _track.BPM; } }
        public string Comment { get { return _track == null ? "" : _track.Comment; } }
        public bool Compilation { get { return _track == null ? false : _track.Compilation; } }
        public string Composer { get { return _track == null ? "" : _track.Composer; } }
        public DateTime DateAdded { get { return _track == null ? DateTime.MinValue : _track.DateAdded; } }
        public int DiscCount { get { return _track == null ? 0 : _track.DiscCount; } }
        public int DiscNumber { get { return _track == null ? 0 : _track.DiscNumber; } }
        public int Duration { get { return _track == null ? 0 : _track.Duration; } }
        public bool Enabled { get { return _track == null ? false : _track.Enabled; } }
        public string EQ { get { return _track == null ? "" : _track.EQ; } }
        public int Finish { get { return _track == null ? 0 : _track.Finish; } }
        public string Genre { get { return _track == null ? "" : _track.Genre; } }
        public string Grouping { get { return _track == null ? "" : _track.Grouping; } }
        public int Index { get { return _track == null ? 0 : _track.Index; } }
        public string KindAsString { get { return _track == null ? "" : _track.KindAsString; } }
        public DateTime ModificationDate { get { return _track == null ? DateTime.MinValue : _track.ModificationDate; } }
        public string Name { get { return _track == null ? "" : _track.Name; } }
        public int PlayedCount { get { return _track == null ? 0 : _track.PlayedCount; } }
        public DateTime PlayedDate { get { return _track == null ? DateTime.MinValue : _track.PlayedDate; } }
        public int PlayOrderIndex { get { return _track == null ? 0 : _track.PlayOrderIndex; } }
        public int Rating { get { return _track == null ? 0 : _track.Rating; } }
        public int SampleRate { get { return _track == null ? 0 : _track.SampleRate; } }
        public int Size { get { return _track == null ? 0 : _track.Size; } }
        public int Start { get { return _track == null ? 0 : _track.Start; } }
        public string Time { get { return _track == null ? "" : _track.Time; } }
        public int TrackCount { get { return _track == null ? 0 : _track.TrackCount; } }
        public int TrackNumber { get { return _track == null ? 0 : _track.TrackNumber; } }
        public int VolumeAdjustment { get { return _track == null ? 0 : _track.VolumeAdjustment; } }
        public int Year { get { return _track == null ? 0 : _track.Year; } }
    }
}
