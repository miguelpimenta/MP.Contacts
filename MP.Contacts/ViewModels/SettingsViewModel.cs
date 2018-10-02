using MP.Contacts.Support;
using System.Windows.Input;

namespace MP.Contacts.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        public ICommand SaveCmd { get; }

        public SettingsViewModel()
        {
            SaveCmd = new RelayCommand(Save);
        }

        private string _language = Settings.Default.Culture;

        public string Language
        {
            get => _language;
            set { _language = value; RaisePropertyChanged(nameof(Language)); }
        }

        private void Save(object arg)
        {
            Settings.Default.Culture = Language;
            Properties.Settings.Default.Save();
        }
    }
}