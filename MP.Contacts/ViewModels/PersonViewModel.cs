using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MP.Contacts.DAL;
using MP.Contacts.Models;
using MP.Contacts.Support;
using MP.Contacts.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MP.Contacts.ViewModels
{
    public class PersonViewModel
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

        public PersonViewModel()
        {
            _dlgCoord = DialogCoordinator.Instance;
            _dispatcher = Application.Current.Dispatcher;
            _dlgSet = DialogSettings.Instance;
            _msgTxt = MsgText.Instance;
            SaveCmd = new RelayCommandAsync(SaveAsync);
            CancelCmd = new RelayCommand(Cancel);

            Person = new Person();

            TestData();
        }

        #region TestData

        private void TestData()
        {
            string[] names = { "Manuel", "Joaquim", "Rui", "José", "António", "Fagundes" };
            string[] surnames = { "Oliveira", "Ferreira", "Costa", "Antunes", "Morais", "Pinheiro" };
            string[] companies = { "Random SA", "Randonized LDA", "", "Neo-Geo Co.", "Nintendo", "Sega Corp." };
            string[] emails = { "qwfwgeg@wqrfqwq.com", "wqrqwfqwAGAV@wrqwr.pt", "ewrqwqg@gmail.com", "qgfsgqgasgsd@wrewt.es", "eagetweyew88@wf88saf.fr", "wegdsdg@wer.com" };

            Random rnd = new Random();

            Person.Name = names[rnd.Next(0, names.Length)] + " " + surnames[rnd.Next(0, names.Length)];
            Person.Company = companies[rnd.Next(0, companies.Length)];
            Person.Phone = rnd.Next(200000000, 299999999).ToString();
            Person.CellPhone = rnd.Next(910000000, 969999999).ToString();
            Person.Email = emails[rnd.Next(0, emails.Length)];

            const string chars01 = "abcd efgh ijkl mnop qrst uvwxyz";
            Person.Address = new string(Enumerable.Repeat(chars01, rnd.Next(5, 50)).Select(s => s[rnd.Next(s.Length)]).ToArray());

            const string chars02 = "abcdefghijklmnopqrstuvwxyz ";
            Person.Locality = new string(Enumerable.Repeat(chars02, rnd.Next(10, 25)).Select(s => s[rnd.Next(s.Length)]).ToArray());

            Person.PostalCode = rnd.Next(4000, 8000).ToString();
            const string chars03 = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789\n";
            Person.Obs = new string(Enumerable.Repeat(chars03, rnd.Next(20, 500)).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        #endregion TestData

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
            await _dlgCoord.ShowMessageAsync(this, _msgTxt.Info, _msgTxt.SaveSuccess,
                MessageDialogStyle.Affirmative, _dlgSet.DlgDefaultSets).ConfigureAwait(false);

            Cancel(arg);
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