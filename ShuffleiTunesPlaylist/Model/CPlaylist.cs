using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesLib;

namespace ShuffleiTunesPlaylist.Model
{
    public class CPlaylist
    {
        readonly List<CPlaylist> _children = new List<CPlaylist>();
        private IITUserPlaylist _playlist = null;

        public IITUserPlaylist Playlist { get { return _playlist; } set { _playlist = value; } }

        public IList<CPlaylist> Children { get { return _children; } }

        public string Name { get { return _playlist == null ? "" : _playlist.Name; } }

        public bool IsFolder { get; set; }

        public CPlaylist(IITUserPlaylist playlist, bool isfolder = false)
        {
            _playlist = playlist;
            IsFolder = isfolder;
        }
    }
}
