using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesLib;

namespace ShuffleiTunesPlaylist.Model
{
    class CTracks
    {
        List<CTrack> _tracks = new List<CTrack>();
        public List<CTrack> Tracks { get { return _tracks; } }

    }
}
