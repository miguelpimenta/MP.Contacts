using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MP.Contacts.DAL;
using MP.Contacts.Models;
using MP.Contacts.Support;
using MP.Contacts.Views;
using MP.Contacts.Views.Flyouts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MP.Contacts.ViewModels
{
    public class PersonsViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dlgCoord;
        private readonly Dispatcher _dispatcher;
        private readonly DialogSettings _dlgSet;

        public ICommand LoadedCmd { get; }
        public ICommand RefreshCmd { get; }
        public ICommand NewContactCmd { get; }

        #region Singleton

        private static PersonsViewModel instance = null;
        private static object lockThis = new object();

        private PersonsViewModel()
        {
            _dlgCoord = DialogCoordinator.Instance;
            _dispatcher = Application.Current.Dispatcher;
            _dlgSet = DialogSettings.Instance;

            LoadedCmd = new RelayCommandAsync(LoadedAsync);
            RefreshCmd = new RelayCommandAsync(RefreshAsync);
            NewContactCmd = new RelayCommandAsync(NewContactAsync);
        }

        public static PersonsViewModel Instance
        {
            get
            {
                lock (lockThis)
                {
                    if (instance == null)
                    {
                        instance = new PersonsViewModel();
                    }
                    return instance;
                }
            }
        }

        #endregion Singleton

        #region Props

        private string _searchTerms = string.Empty;

        public string SearchTerms
        {
            get => _searchTerms;
            set { _searchTerms = value; RaisePropertyChanged(nameof(SearchTerms)); }
        }

        #endregion Props

        #region Collections

        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();

        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set { _persons = value; RaisePropertyChanged(nameof(Persons)); }
        }

        #endregion Collections

        private async Task LoadedAsync(object arg)
        {
            await RefreshAsync(null).ConfigureAwait(false);
        }

        private async Task RefreshAsync(object arg)
        {
            await Task.Run(() =>
            {
                using (ILitedbDAL dal = new LitedbDAL())
                {
                    Persons = dal.ListPersons(SearchTerms, string.Empty, string.Empty, string.Empty);
                }
            }).ConfigureAwait(false);
        }

        private async Task NewContactAsync(object arg)
        {
            await Task.Run(() =>
            {
                _dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    var obj = Application.Current.MainWindow.FindName("FlyoutNewContact");
                    var flyout = (Flyout)obj;
                    flyout.Content = new PersonView();
                    flyout.IsOpen = !flyout.IsOpen;
                }));
            }).ConfigureAwait(false);
        }
    }
}