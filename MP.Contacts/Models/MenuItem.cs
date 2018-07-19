using MahApps.Metro.Controls;
using MP.Contacts.Support;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MP.Contacts.Models
{
    public class MenuItem : HamburgerMenuIconItem, INotifyPropertyChanged
    {
        private object _name;
        private object _content;
        private string _text;
        private DelegateCommand _command;
        private Uri _navigationDestination;

        public object Name
        {
            get { return this._name; }
            set { this.SetProperty(ref this._name, value); }
        }

        public object Content
        {
            get { return _content; }
            set { this.SetProperty(ref _content, value); }
        }

        public string Text
        {
            get { return this._text; }
            set { this.SetProperty(ref this._text, value); }
        }

        public ICommand Command
        {
            get { return this._command; }
            set { this.SetProperty(ref this._command, (DelegateCommand)value); }
        }

        public Uri NavigationDestination
        {
            get { return this._navigationDestination; }
            set { this.SetProperty(ref this._navigationDestination, value); }
        }

        public bool IsNavigation => this._navigationDestination != null;

        //// Tooltip
        ////https://github.com/MahApps/MahApps.Metro/issues/2928
        //public static readonly DependencyProperty ToolTipProperty
        //    = DependencyProperty.Register("ToolTip",
        //        typeof(object),
        //        typeof(MenuItem),
        //        new PropertyMetadata(null));

        //public object ToolTip
        //{
        //    get { return (object)GetValue(ToolTipProperty); }
        //    set { SetValue(ToolTipProperty, value); }
        //}

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}