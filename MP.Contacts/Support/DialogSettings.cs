using MahApps.Metro.Controls.Dialogs;

namespace MP.Contacts.Support
{
    public sealed class DialogSettings
    {
        #region Singleton

        private static DialogSettings instance = null;
        private static object lockThis = new object();

        private DialogSettings()
        {
        }

        public static DialogSettings Instance
        {
            get
            {
                lock (lockThis)
                {
                    if (instance == null)
                    {
                        instance = new DialogSettings();
                    }

                    return instance;
                }
            }
        }

        #endregion Singleton

        public MetroDialogSettings DlgDefaultSets => new MetroDialogSettings()
        {
            AffirmativeButtonText = "OK",
            DialogTitleFontSize = 32,
            DialogMessageFontSize = 24,
            AnimateHide = true,
            AnimateShow = true,
            ColorScheme = MetroDialogColorScheme.Theme,
        };

        public MetroDialogSettings DlgErrorSets => new MetroDialogSettings()
        {
            AffirmativeButtonText = "OK",
            DialogTitleFontSize = 32,
            DialogMessageFontSize = 24,
            AnimateHide = true,
            AnimateShow = true,
            ColorScheme = MetroDialogColorScheme.Inverted,
        };

        public MetroDialogSettings DlgYesNoSets => new MetroDialogSettings()
        {
            AffirmativeButtonText = "Sim",
            NegativeButtonText = "Não",
            DialogTitleFontSize = 32,
            DialogMessageFontSize = 24,
            AnimateHide = true,
            AnimateShow = true,
            ColorScheme = MetroDialogColorScheme.Theme,
        };
    }
}