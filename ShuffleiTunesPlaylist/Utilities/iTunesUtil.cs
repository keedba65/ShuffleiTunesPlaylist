using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesLib;

namespace ShuffleiTunesPlaylist.Utilities
{
    internal class iTunesUtil
    {
        public static IITPlaylistCollection GetPlaylistCollection(iTunesApp itunes)
        {
            foreach (IITSource source in itunes.Sources)
            {
                if (source.Kind == ITSourceKind.ITSourceKindLibrary)
                {
                    return source.Playlists;
                }
            }

            return null;
        }

        public static IITUserPlaylist ShufflePlayList(IITUserPlaylist playlist)
        {
            try
            {
                var tracks = playlist.Tracks;
                var rndm = new Random((int)DateTime.Now.Ticks);
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

                var sourceTracks = new List<iTunesPersistentId>();
                foreach (IITTrack trk in tracks)
                {
                    sourceTracks.Add(new iTunesPersistentId(trk));
                }

                var tplName = $"{playlist.Name}_shfl";
                var parent = playlist.get_Parent();
                var pl1 = (parent == null) ? App.iTunes.CreatePlaylist(tplName) as IITUserPlaylist : parent.CreatePlaylist(tplName) as IITUserPlaylist;
                for (var n = 0; n < nTracks; n++)
                {
                    var trk = sourceTracks[shuffle[n]].GetTrackFromList(App.iTunes.LibraryPlaylist.Tracks);
                    pl1?.AddTrack(trk);
                }

                foreach (var id in sourceTracks)
                {
                    var trk = id.GetTrackFromList(playlist.Tracks);
                    trk.Delete();
                }

                for (var n = 0; n < nTracks; n++)
                {
                    var trk = sourceTracks[shuffle[n]].GetTrackFromList(App.iTunes.LibraryPlaylist.Tracks);
                    playlist.AddTrack(trk);
                }

                pl1?.Delete();
                return playlist;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return playlist;
            }
        }
    }

    internal class iTunesPersistentId
    {
        private readonly int _mLowId;
        private readonly int _mHighId;

        public iTunesPersistentId(object iObject)
        {
            App.iTunes.GetITObjectPersistentIDs(iObject, out var highId, out var lowId);
            _mHighId = highId;
            _mLowId = lowId;
        }

        public IITTrack GetTrackFromList(IITTrackCollection tracks)
        {
            return tracks.ItemByPersistentID[_mHighId, _mLowId];
        }

        public IITPlaylist GetPlaylistFromCollection(IITPlaylistCollection playlists)
        {
            return playlists.ItemByPersistentID[_mHighId, _mLowId];
        }
    }

}
