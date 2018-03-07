using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesLib;
using ShuffleiTunesPlaylist.Utilities;

namespace ShuffleiTunesPlaylist.Model
{
    public class CPlaylist
    {
        readonly List<CPlaylist> _children = new List<CPlaylist>();
        private readonly iTunesPersistentId _mPersistentId;

        public IITUserPlaylist Playlist => (IITUserPlaylist) _mPersistentId?.GetPlaylistFromCollection(iTunesUtil.GetPlaylistCollection(App.iTunes));

        public IList<CPlaylist> Children => _children;

        public string Name { get; private set; }

        public bool IsFolder { get; set; }

        public CPlaylist(IITUserPlaylist playlist, bool isfolder = false)
        {
            if (playlist == null)
            {
                return;
            }
            //Playlist = playlist;
            _mPersistentId = new iTunesPersistentId(playlist);
            Name = playlist.Name;
            IsFolder = isfolder;
        }
    }
}
