using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ShuffleiTunesPlaylist.Model;
using System.Windows;
using ShuffleiTunesPlaylist.Brokers;
using ShuffleiTunesPlaylist.Utilities;

namespace ShuffleiTunesPlaylist.ViewModel
{
    class MainWindowVM : BaseWindowViewModel
    {
        readonly ObservableCollection<CPlaylistVM> _playlists;
        readonly CPlaylistVM _rootPlaylist;
        ObservableCollection<CTrack> _tracks;

        public MainWindowVM(CPlaylist rootPlaylist)
        {
            _rootPlaylist = new CPlaylistVM(rootPlaylist);

            _playlists = new ObservableCollection<CPlaylistVM>(_rootPlaylist.Children);
            //_playlists = new ObservableCollection<CPlaylistVM>(
            //    new CPlaylistVM[] 
            //    { 
            //        _rootPlaylist 
            //    });
            InitCommands();
        }

        #region Playlists

        /// <summary>
        /// Returns a read-only collection containing the first person 
        /// in the family tree, to which the TreeView can bind.
        /// </summary>
        public ObservableCollection<CPlaylistVM> Playlists
        {
            get { return _playlists; }
        }

        #endregion // Playlists

        public ObservableCollection<CTrack> Tracks { get { return _tracks; } }

        public CPlaylistVM SelectedPlaylist { get; set; }

        #region Commands
        RelayCommand _ExitCommand;
        RelayCommand _AboutCommand;
        RelayCommand _ShuffleCommand;
        RelayCommand _SelectionChanged;
        public ICommand ExitCommand { get { return _ExitCommand; } }
        public ICommand AboutCommand { get { return _AboutCommand; } }
        public ICommand ShuffleCommand { get { return _ShuffleCommand; } }
        public ICommand SelectionChanged { get { return _SelectionChanged; } }
        void InitCommands()
        {
            _ExitCommand = new RelayCommand(param => DoExitCommand());
            _AboutCommand = new RelayCommand(param => DoAboutCommand());
            _ShuffleCommand = new RelayCommand(param => DoShuffleCommand(), o => CanShuffleCommand());
            _SelectionChanged = new RelayCommand(DoSelectionChanged);
        }

        void DoExitCommand()
        {
            CloseCommand.Execute(null);
        }

        void DoAboutCommand()
        {
            //About dlg = new About();
            //dlg.Owner = App.Current.MainWindow;
            //dlg.ShowDialog();
        }

        bool CanShuffleCommand()
        {
            if (SelectedPlaylist != null)
            {
                return !SelectedPlaylist.IsFolder;
            }
            return false;
        }

        void DoShuffleCommand()
        {
            if (SelectedPlaylist != null)
            {
                CPlaylistVM pl = SelectedPlaylist;
                pl.Playlist = iTunesUtil.ShufflePlayList(pl.Playlist);
                pl.UpdateChildren();
                DoSelectionChanged(null);
                DoSelectionChanged(pl);
            }
        }

        void DoSelectionChanged(object selectedPlaylist)
        {
            CPlaylistVM pl = selectedPlaylist as CPlaylistVM;
            if (pl != SelectedPlaylist)
            {
                bool clear = true;
                SelectedPlaylist = pl;
                if (SelectedPlaylist != null)
                {
                    if (!SelectedPlaylist.IsFolder)
                    {
                        CTracks tracks = CTracksBroker.LoadTracks(SelectedPlaylist.Playlist.Tracks);
                        _tracks = null;
                        _tracks = new ObservableCollection<CTrack>(tracks.Tracks);
                        clear = false;
                    }
                }
                if (clear)
                {
                    _tracks = null;
                }
                OnPropertyChanged("Tracks");
            }
        }

        #endregion

        protected override void CleanUp()
        {
        }
    }
}
 