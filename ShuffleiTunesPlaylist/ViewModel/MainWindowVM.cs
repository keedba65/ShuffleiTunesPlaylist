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
        readonly CPlaylistVM _rootPlaylist;

        public MainWindowVM(CPlaylist rootPlaylist)
        {
            _rootPlaylist = new CPlaylistVM(rootPlaylist);

            Playlists = new ObservableCollection<CPlaylistVM>(_rootPlaylist.Children);
            InitCommands();
        }

        #region Playlists

        /// <summary>
        /// Returns a read-only collection containing the first person 
        /// in the family tree, to which the TreeView can bind.
        /// </summary>
        public ObservableCollection<CPlaylistVM> Playlists { get; }

        #endregion // Playlists

        public ObservableCollection<CTrack> Tracks { get; private set; }

        public CPlaylistVM SelectedPlaylist { get; set; }

        #region Commands
        private RelayCommand _mExitCommand;
        private RelayCommand _mAboutCommand;
        private RelayCommand _mShuffleCommand;
        private RelayCommand _mSelectionChanged;

        public ICommand ExitCommand => _mExitCommand;
        public ICommand AboutCommand => _mAboutCommand;
        public ICommand ShuffleCommand => _mShuffleCommand;
        public ICommand SelectionChanged => _mSelectionChanged;

        private void InitCommands()
        {
            _mExitCommand = new RelayCommand(param => DoExitCommand());
            _mAboutCommand = new RelayCommand(param => DoAboutCommand());
            _mShuffleCommand = new RelayCommand(param => DoShuffleCommand(), o => CanShuffleCommand());
            _mSelectionChanged = new RelayCommand(DoSelectionChanged);
        }

        private void DoExitCommand()
        {
            CloseCommand.Execute(null);
        }

        private void DoAboutCommand()
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

        private void DoShuffleCommand()
        {
            if (SelectedPlaylist == null)
            {
                return;
            }

            var pl = SelectedPlaylist;
            var newPl = iTunesUtil.ShufflePlayList(pl.IPlaylist);
            var playlist = new CPlaylist(newPl);
            pl.UpdatePlaylist(playlist);
            DoSelectionChanged(null);
            DoSelectionChanged(pl);
        }

        private void DoSelectionChanged(object selectedPlaylist)
        {
            var pl = selectedPlaylist as CPlaylistVM;
            if (pl == SelectedPlaylist)
            {
                return;
            }

            var clear = true;
            SelectedPlaylist = pl;
            if (SelectedPlaylist != null)
            {
                if (!SelectedPlaylist.IsFolder)
                {
                    var tracks = CTracksBroker.LoadTracks(SelectedPlaylist.IPlaylist.Tracks);
                    Tracks = null;
                    Tracks = new ObservableCollection<CTrack>(tracks.Tracks);
                    clear = false;
                }
            }
            if (clear)
            {
                Tracks = null;
            }
            OnPropertyChanged("Tracks");
        }

        #endregion

        protected override void CleanUp()
        {
        }
    }
}
 