using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using iTunesLib;
using ShuffleiTunesPlaylist.Brokers;
using ShuffleiTunesPlaylist.Model;
using ShuffleiTunesPlaylist.ViewModel;

namespace ShuffleiTunesPlaylist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowVM mwv;

        public MainWindow()
        {
            InitializeComponent();
            CPlaylist playlist = CPlaylistBroker.LoadPlaylists(App.iTunes);
            mwv = new MainWindowVM(playlist);
            DataContext = mwv;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            mwv.SelectionChanged.Execute(e.NewValue);
        }
    }
}
