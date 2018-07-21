using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MP.Contacts.Support;
using MP.Contacts.Views;
using MP.Contacts.Views.Flyouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MP.Contacts.ViewModels
{
    public class ContactsViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dlgCoord;
        private readonly Dispatcher _dispatcher;
        private readonly DialogSettings _dlgSet;

        public ICommand RefreshCmd { get; }
        public ICommand NewContactCmd { get; }

        #region Singleton

        private static ContactsViewModel instance = null;
        private static object lockThis = new object();

        private ContactsViewModel()
        {
            _dlgCoord = DialogCoordinator.Instance;
            _dispatcher = Application.Current.Dispatcher;
            _dlgSet = DialogSettings.Instance;

            RefreshCmd = new RelayCommandAsync(Refresh);
            NewContactCmd = new RelayCommandAsync(NewContactAsync);
        }

        public static ContactsViewModel Instance
        {
            get
            {
                lock (lockThis)
                {
                    if (instance == null)
                    {
                        instance = new ContactsViewModel();
                    }
                    return instance;
                }
            }
        }

        #endregion Singleton

        #region Props

        private string _searchTerms;

        public string SearchTerms
        {
            get => _searchTerms;
            set { _searchTerms = value; RaisePropertyChanged(nameof(SearchTerms)); }
        }

        #endregion Props

        private Task Refresh(object arg)
        {
            throw new NotImplementedException();
        }

        private async Task NewContactAsync(object arg)
        {
            await Task.Run(() =>
            {
                _dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    object obj = Application.Current.MainWindow.FindName("FlyoutNewContact");
                    var flyout = (Flyout)obj;
                    flyout.Content = new ContactView();
                    flyout.IsOpen = !flyout.IsOpen;
                }));
            }).ConfigureAwait(false);
        }
    }
}