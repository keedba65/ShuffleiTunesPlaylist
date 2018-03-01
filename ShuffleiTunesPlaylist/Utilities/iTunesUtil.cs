using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesLib;

namespace ShuffleiTunesPlaylist.Utilities
{
    internal class iTunesUtil
    {
        private class PersistentId
        {
            private readonly int _mLowId;
            private readonly int _mHighId;

            public PersistentId(IITTrack track)
            {
                App.iTunes.GetITObjectPersistentIDs(track, out var highId, out var lowId);
                _mHighId = highId;
                _mLowId = lowId;
            }

            public IITTrack GetTrackFromList(IITTrackCollection tracks)
            {
                return tracks.ItemByPersistentID[_mHighId, _mLowId];
            }
        }

        public static IITUserPlaylist ShufflePlayList(IITUserPlaylist playlist)
        {
            IITTrackCollection tracks = playlist.Tracks;
            Random rndm = new Random((int)DateTime.Now.Ticks);
            var nTracks = tracks.Count;

            // Generate shuffled track order
            var shuffle = new int[nTracks];
            for (var n = 0; n < nTracks; n++) { shuffle[n] = n; }
            for (var n = 0; n < nTracks; n++)
            {
                var i1 = rndm.Next(nTracks);
                var i2 = rndm.Next(nTracks);
                var t = shuffle[i1];
                shuffle[i1] = shuffle[i2];
                shuffle[i2] = t;
            }

            var sourceTracks = new List<PersistentId>();
            foreach (IITTrack trk in tracks)
            {
                sourceTracks.Add(new PersistentId(trk));
            }

            foreach (var id in sourceTracks)
            {
                var trk = id.GetTrackFromList(playlist.Tracks);
                trk.Delete();
            }

            for (var n = 0; n < nTracks;n++)
            {
                var trk = sourceTracks[shuffle[n]].GetTrackFromList(App.iTunes.LibraryPlaylist.Tracks);
                playlist.AddTrack(trk);
            }

            return playlist;
        }
    }
}
