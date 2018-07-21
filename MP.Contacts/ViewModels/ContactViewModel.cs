using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MP.Contacts.DAL;
using MP.Contacts.Models;
using MP.Contacts.Support;
using MP.Contacts.Utils;
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
    public class ContactViewModel
    {
        private readonly IDialogCoordinator _dlgCoord;
        private readonly Dispatcher _dispatcher;
        private readonly DialogSettings _dlgSet;
        private readonly MsgText _msgTxt;
        public ICommand SaveCmd { get; }
        public ICommand CancelCmd { get; }

        #region props

        private Person _person;

        public Person Person
        {
            get => _person;
            set => _person = value;
        }

        #endregion props

        public ContactViewModel()
        {
            _dlgCoord = DialogCoordinator.Instance;
            _dispatcher = Application.Current.Dispatcher;
            _dlgSet = DialogSettings.Instance;
            _msgTxt = MsgText.Instance;
            SaveCmd = new RelayCommandAsync(SaveAsync);
            CancelCmd = new RelayCommand(Cancel);

            Person = new Person();
        }

        private async Task SaveAsync(object arg)
        {
            var ctrl = await _dlgCoord.ShowProgressAsync(this, _msgTxt.PleaseWait, _msgTxt.Waiting,
                false, _dlgSet.DlgDefaultSets).ConfigureAwait(false);
            ctrl.SetIndeterminate();
            await Task.Run(() =>
            {
                using (ILitedbDAL dal = new LitedbDAL())
                {
                    dal.InsertPerson(Person);
                }
            }).ConfigureAwait(false);
            await ctrl.CloseAsync().ConfigureAwait(false);
        }

        private void Cancel(object obj)
        {
            if (obj != null)
            {
                try
                {
                    if (obj is Flyout flyout)
                    {
                        _dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => flyout.IsOpen = !flyout.IsOpen));
                    }
                }
                catch (Exception ex)
                {
                    Log2Txt.Instance.ErrorLog(ex.ToString());
                    throw;
                }
            }
        }
    }
}