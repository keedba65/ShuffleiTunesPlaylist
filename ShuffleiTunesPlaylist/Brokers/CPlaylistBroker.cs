using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using iTunesLib;
using ShuffleiTunesPlaylist.Model;

namespace ShuffleiTunesPlaylist.Brokers
{
    class CPlaylistBroker
    {
        public static CPlaylist LoadPlaylists(iTunesApp itunes)
        {
            CPlaylist retPlaylist = new CPlaylist(null);
            IITSourceCollection sources = itunes.Sources;
            IITPlaylistCollection playlists = null;
            IITSource plSource = null;
            foreach (IITSource source in sources)
            {
                if (source.Kind == ITSourceKind.ITSourceKindLibrary)
                {
                    plSource = source;
                    playlists = source.Playlists;
                    break;
                }
            }
            foreach (IITPlaylist pl in playlists)
            {
                if (pl.Kind != ITPlaylistKind.ITPlaylistKindUser) continue;
                try
                {
                    IITUserPlaylist upl = (IITUserPlaylist)pl;
                    if (upl.Smart) continue;
                    if (upl.SpecialKind == ITUserPlaylistSpecialKind.ITUserPlaylistSpecialKindPodcasts) continue;
                    string strDir = string.Empty;
                    Stack<IITUserPlaylist> parentStack = new Stack<IITUserPlaylist>();
                    IITUserPlaylist uplp = upl.get_Parent();
                    if (uplp != null)
                    {
                        parentStack.Push(uplp);
                        IITUserPlaylist uplc = uplp;
                        do
                        {
                            uplp = uplc.get_Parent();
                            if (uplp != null)
                            {
                                parentStack.Push(uplp);
                                uplc = uplp;
                            }
                        } while (uplp != null);
                    }
                    CPlaylist parentPL = retPlaylist;
                    bool bFoundLeaf = false;
                    do
                    {
                        uplp = (parentStack.Count > 0) ? parentStack.Pop() : null;
                        if (uplp == null)
                        {
                            bFoundLeaf = true;
                        }
                        else
                        {
                            CPlaylist childPL = null;
                            foreach (var item in parentPL.Children)
                            {
                                if (item.Name == uplp.Name)
                                {
                                    childPL = item;
                                    break;
                                }
                            }
                            if (childPL != null)
                            {
                                parentPL = childPL;
                            }
                            else
                            {
                                bFoundLeaf = true;
                            }
                        }
                    } while (!bFoundLeaf);
                    while (uplp != null)
                    {
                        CPlaylist plChild = new CPlaylist(uplp,true);
                        parentPL.Children.Add(plChild);
                        parentPL = plChild;
                        uplp = (parentStack.Count > 0) ? parentStack.Pop() : null;
                    }
                    parentPL.Children.Add(new CPlaylist(upl));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            return retPlaylist;
        }
    }
}
