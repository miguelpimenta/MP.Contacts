using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using MP.Contacts.DAL;
using MP.Contacts.Models;
using MP.Contacts.Support;
using MP.Contacts.Utils;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ToastNotifications.Messages;

namespace MP.Contacts.ViewModels
{
    public class PersonViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dlgCoord;
        private readonly Dispatcher _dispatcher;
        private readonly DialogSettings _dlgSet;
        private readonly MsgText _msgTxt;
        public ICommand SaveCmd { get; }
        public ICommand CancelCmd { get; }
        public ICommand OpenPhotoCmd { get; }

        #region Props

        private Person _person;
        private bool _newPerson;
        private string _title;

        public Person Person
        {
            get => _person;
            set => _person = value;
        }

        public bool NewPerson
        {
            get => _newPerson;
            set => _newPerson = value;
        }

        public string Title
        {
            get => _title;
            set { _title = value; RaisePropertyChanged(nameof(Title)); }
        }

        #endregion Props

        //public PersonViewModel()
        //{
        //    _dlgCoord = DialogCoordinator.Instance;
        //    _dispatcher = Application.Current.Dispatcher;
        //    _dlgSet = DialogSettings.Instance;
        //    _msgTxt = MsgText.Instance;
        //    SaveCmd = new RelayCommandAsync(SaveAsync);
        //    CancelCmd = new RelayCommand(Cancel);
        //    OpenPhotoCmd = new RelayCommandAsync(OpenPhotoAsync);

        //    Person = new Person();

        //    TestData();
        //}

        public PersonViewModel(bool newPerson, Person person = null)
        {
            NewPerson = newPerson;

            _dlgCoord = DialogCoordinator.Instance;
            _dispatcher = Application.Current.Dispatcher;
            _dlgSet = DialogSettings.Instance;
            _msgTxt = MsgText.Instance;
            SaveCmd = new RelayCommandAsync(SaveAsync);
            CancelCmd = new RelayCommand(Cancel);
            OpenPhotoCmd = new RelayCommandAsync(OpenPhotoAsync);

            if (NewPerson)
            {
                Title = "New Contact";
                Person = new Person();

                TestData();
            }
            else
            {
                Title = "Edit Contact";
                Person = person;
            }
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
            await Task.Run(async () =>
            {
                using (ILitedbDAL dal = new LitedbDAL())
                {
                    if (NewPerson)
                    {
                        if (dal.InsertPerson(Person))
                        {
                            await ctrl.CloseAsync().ConfigureAwait(false);
                            //await _dispatcher.BeginInvoke(new Action(() => MainViewModel.Instance.Notifier.ShowInformation(_msgTxt.SaveSuccess)), DispatcherPriority.Background);
                            await Task.Factory.StartNew(() => MainViewModel.Instance.MessageQueue.Enqueue(_msgTxt.SaveSuccess)).ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        dal.UpdatePerson(Person);
                    }
                }
            }).ConfigureAwait(false);
            //            await ctrl.CloseAsync().ConfigureAwait(false);
            //await _dlgCoord.ShowMessageAsync(this, _msgTxt.Info, _msgTxt.SaveSuccess,
            //    MessageDialogStyle.Affirmative, _dlgSet.DlgDefaultSets).ConfigureAwait(false);

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

        private async Task OpenPhotoAsync(object obj)
        {
            var ofd = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Image Files| *.jpg; *.jpeg; *.png; *.gif; *.tif;",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (ofd.ShowDialog() == true)
            {
                var file = ofd.FileNames[0];
                if (file.Length <= Settings.Default.DBBinFileSizeLimit)
                {
                    Person.Binary = new Binary
                    {
                        FileBytes = File.ReadAllBytes(file),
                        FileType = Path.GetExtension(file).Replace(".", "")
                    };
                }
                else
                {
                    await _dlgCoord.ShowMessageAsync(this, _msgTxt.Info, "Ficheiro com tamanho superior a 250kb.",
                        MessageDialogStyle.Affirmative, _dlgSet.DlgErrorSets).ConfigureAwait(false);
                }
            }
        }
    }
}