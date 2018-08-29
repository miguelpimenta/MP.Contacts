using MP.Contacts.Views;
using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using WPFLocalizeExtension.Engine;

namespace MP.Contacts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex mutex = null;

        #region Starting

        protected override void OnStartup(StartupEventArgs e)
        {
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = new CultureInfo(Settings.Default.Culture);

            CheckInstance();

            bool debug = false;
#if DEBUG
            debug = true;
#else
            debug = false;
#endif

            // Run startup code first
            base.OnStartup(e);

            // Exceptions
            Current.DispatcherUnhandledException += AppDispatcher_UnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Main
            MainView wndMain = new MainView();
            wndMain.ShowDialog();
        }

        #endregion Starting

        #region Check if App is running

        private void CheckInstance()
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name;
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Application is already running.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        #endregion Check if App is running

        #region Exceptions

        private void AppDispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            StringBuilder errorMsg = new StringBuilder();
            errorMsg.Append("An application error occurred.\n");
            errorMsg.Append("Please check whether your data is correct and repeat the action.");
            errorMsg.Append("If this error occurs again there seems to be a more serious malfunction in the application, and you better close it.\n\n");
            errorMsg.Append("Error: ").Append(e.Exception.Message).Append(e.Exception.InnerException != null ? "\n" + e.Exception.InnerException.Message : null).Append("\n\nDo you want to continue?\n");
            errorMsg.Append("(if you click Yes you will continue with your work, if you click No the application will close)");

            if (MessageBox.Show(errorMsg.ToString(), "Application Error", MessageBoxButton.YesNoCancel, MessageBoxImage.Error) == MessageBoxResult.No)
            {
                if (MessageBox.Show("WARNING: The application will close. Any changes will not be saved!\nDo you really want to close it?", "Close the application!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder errorMsg = new StringBuilder();
            errorMsg.Append("An application error occurred.\n");
            errorMsg.Append("Please check whether your data is correct and repeat the action.");
            errorMsg.Append("If this error occurs again there seems to be a more serious malfunction in the application, and you better close it.\n\n");
            errorMsg.Append("Error: ").Append(e.ToString()).Append("\n\nDo you want to continue?\n");
            errorMsg.Append("(if you click Yes you will continue with your work, if you click No the application will close)");

            if (MessageBox.Show(errorMsg.ToString(), "Application Error", MessageBoxButton.YesNoCancel, MessageBoxImage.Error) == MessageBoxResult.No)
            {
                if (MessageBox.Show("WARNING: The application will close. Any changes will not be saved!\nDo you really want to close it?", "Close the application!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
        }

        #endregion Exceptions

        #region Utils

        public static string AssemblyGuid
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(GuidAttribute), false);
                if (attributes.Length == 0)
                { return String.Empty; }
                return ((GuidAttribute)attributes[0]).Value.ToUpper();
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
        }

        public static string AssemblyName
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Name;
            }
        }

        public static string AssemblyFullName
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().FullName;
            }
        }

        #endregion Utils
    }
}