using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;

namespace ShuffleiTunesPlaylist.ViewModel
{
    /// <summary>
    /// Base View model for view models of type windows can be closed.
    /// </summary>
    abstract class BaseWindowViewModel : BaseViewModel
    {
        private RelayCommand _closeCommand;
        public event EventHandler WindowClose;

        protected BaseWindowViewModel()
        {
        }

        protected System.Windows.Threading.DispatcherTimer _delayStartTimer = null;
        protected delegate void DelayStartEventDelegate();
        protected DelayStartEventDelegate _delayStartEvent;

        protected void DelayStart(DelayStartEventDelegate delayStartEvent)
        {
            _delayStartEvent += delayStartEvent;
            _delayStartTimer = new System.Windows.Threading.DispatcherTimer();
            _delayStartTimer.Tick += new System.EventHandler(_delayStartTimer_Tick);
            _delayStartTimer.Interval = System.TimeSpan.FromMilliseconds(100);
            _delayStartTimer.Start();
        }

        protected void _delayStartTimer_Tick(object sender, System.EventArgs e)
        {
            _delayStartTimer.Stop();
            _delayStartTimer = null;
            if (_delayStartEvent != null) _delayStartEvent();
        }

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => this.OnWindowClose());
                }

                return _closeCommand;
            }
        }

        protected abstract void CleanUp();

        
        /// <summary>
        /// Handles Window close event
        /// </summary>
        void OnWindowClose()
        {
            EventHandler windowCloseHandler = this.WindowClose;
            CleanUp();
            if (windowCloseHandler != null)
            {
                windowCloseHandler(this, EventArgs.Empty);
            }
        }
    }
}
