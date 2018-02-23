﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using iTunesLib;
using ShuffleiTunesPlaylist.Model;

namespace ShuffleiTunesPlaylist.ViewModel
{
    class CPlaylistVM : BaseWindowViewModel
    {
            #region Data

        ObservableCollection<CPlaylistVM> _children;
        CPlaylistVM _parent;
        CPlaylist _playlist;

        bool _isExpanded;
        bool _isSelected;

        #endregion // Data

        #region Constructors

        public CPlaylistVM(CPlaylist playlist)
            : this(playlist, null)
        {
        }

        private CPlaylistVM(CPlaylist playlist, CPlaylistVM parent)
        {
            _parent = parent;
            _playlist = playlist;

            UpdateChildren();
        }

        public void UpdateChildren()
        {
            _children = null;
            _children = new ObservableCollection<CPlaylistVM>(
                    (from child in _playlist.Children
                     select new CPlaylistVM(child, this))
                     .ToList<CPlaylistVM>());
            OnPropertyChanged("Children");
        }

        #endregion // Constructors

        #region Person Properties

        public ObservableCollection<CPlaylistVM> Children
        {
            get { return _children; }
        }

        public string Name
        {
            get { return _playlist == null ? "" : _playlist.Name; }
        }

        public bool IsFolder { get { return _playlist == null ? false : _playlist.IsFolder; } }

        public IITUserPlaylist Playlist { get { return _playlist == null ? null : _playlist.Playlist; } set { if (_playlist != null) _playlist.Playlist = value; } }

        #endregion // Person Properties

        #region Presentation Members

        #region IsExpanded

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }

        #endregion // IsExpanded

        #region IsSelected

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected

        #region Parent

        public CPlaylistVM Parent
        {
            get { return _parent; }
        }

        #endregion // Parent

        #endregion // Presentation Members        
        protected override void CleanUp()
        {
        }
    }
}
