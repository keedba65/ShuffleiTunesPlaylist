
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace ShuffleiTunesPlaylist.ViewModel
{
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(msg);
                }
            }
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnAllPropertiesChanged()
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(this))
                {
                    if (!pd.Name.Equals("ThrowOnInvalidPropertyName"))
                    {
                        handler(this, new PropertyChangedEventArgs(pd.Name));
                    }
                }
            }
        }

        /// <summary>
        /// Sets a property to a value and calls OnPropertyChanged for one or more property names.
        /// This is a helper so that we can make simple property setters be single line.
        /// </summary>
        /// <returns>True if the value changed, false otherwise.</returns>
        protected virtual bool SetValue<T>(ref T field, T value, params string[] propertyNames)
        {
            bool hasChanged = false;

            if (field == null)
            {
                hasChanged = (value != null);
            }
            else if (value == null)
            {
                hasChanged = true;
            }
            else
            {
                IEquatable<T> equatable = field as IEquatable<T>;

                if (equatable != null)
                {
                    // use IEquatable.Equals
                    hasChanged = !equatable.Equals(value);
                }
                else
                {
                    // use Object.Equals
                    hasChanged = !field.Equals(value);
                }
            }

            if (hasChanged)
            {
                field = value;

                if (this.PropertyChanged != null)
                {
                    foreach (string propertyName in propertyNames)
                    {
                        OnPropertyChanged(propertyName);
                    }
                }
            }

            return hasChanged;
        }

        #endregion Methods
    }
}
