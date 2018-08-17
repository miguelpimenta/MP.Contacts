using MP.Contacts.Support;
using MP.Contacts.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MP.Contacts.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        public ICommand SaveCmd { get; }

        public SettingsViewModel()
        {
            SaveCmd = new RelayCommandAsync(Save);
        }

        private string _language = Settings.Default.Culture;

        public string Language
        {
            get => _language;
            set { _language = value; RaisePropertyChanged(nameof(Language)); MsgText.Instance.UpdateLanguage(); }
        }

        private async Task Save(object arg)
        {
            Settings.Default.Culture = Language;
            Properties.Settings.Default.Save();
        }
    }
}