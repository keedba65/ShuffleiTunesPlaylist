using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using iTunesLib;

namespace ShuffleiTunesPlaylist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static iTunesApp _itunes = new iTunesApp();
        public static iTunesApp iTunes { get { return _itunes; } }

    }
}
