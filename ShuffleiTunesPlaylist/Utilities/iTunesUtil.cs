using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesLib;

namespace ShuffleiTunesPlaylist.Utilities
{
    class iTunesUtil
    {
        public static IITUserPlaylist ShufflePlayList(IITUserPlaylist playlist)
        {
            IITTrackCollection tracks = playlist.Tracks;
            Random rndm = new Random((int)DateTime.Now.Ticks);
            int nTracks = tracks.Count;

            // Generate shuffled track order
            int[] shuffle = new int[nTracks];
            for (int n = 0; n < nTracks; n++) { shuffle[n] = n + 1; }
            for (int n = 0; n < nTracks; n++)
            {
                int i1 = rndm.Next(nTracks);
                int i2 = rndm.Next(nTracks);
                int t = shuffle[i1];
                shuffle[i1] = shuffle[i2];
                shuffle[i2] = t;
            }

            // Create temporary playlist
            string plName = playlist.Name;
            string tplName = string.Format("{0}_shfl",plName);
            IITUserPlaylist parent = playlist.get_Parent();
            IITUserPlaylist pl1 = (parent == null) ? App.iTunes.CreatePlaylist(tplName) as IITUserPlaylist : parent.CreatePlaylist(tplName) as IITUserPlaylist;
            // populate playlist with shuffled track order
            for (int n = 0; n < nTracks; n++)
            {
                IITTrack trk = tracks[shuffle[n]];
                pl1.AddTrack(trk);
            }

            // delete original playlist
            playlist.Delete();

            // rename new playlist to original name
            pl1.Name = plName;

            // Retuen new playlist so tree can be updated.
            return pl1;
        }
    }
}
