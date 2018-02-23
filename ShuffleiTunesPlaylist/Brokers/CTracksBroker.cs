using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesLib;
using ShuffleiTunesPlaylist.Model;

namespace ShuffleiTunesPlaylist.Brokers
{
    class CTracksBroker
    {
        public static CTracks LoadTracks(IITTrackCollection tracks)
        {
            CTracks retTracks = new CTracks();
            foreach (IITTrack track in tracks)
            {
                retTracks.Tracks.Add(new CTrack(track));
            }
            return retTracks;
        }
    }
}
