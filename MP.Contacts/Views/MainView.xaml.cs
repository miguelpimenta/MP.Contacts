using MahApps.Metro.Controls;
using MP.Contacts.Utils;
using MP.Contacts.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MP.Contacts.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : MetroWindow
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = MainViewModel.Instance;
            CancellationToken cancelToken = new CancellationToken();
            TaskCreationOptions taskCreatOpt = new TaskCreationOptions();

            Task.Factory.StartNew(() =>
            {
                MainViewSnackbar.MessageQueue.Enqueue(MsgText.Instance.TransLatedString("Welcome01"));
                Thread.Sleep(2500);
            }, cancelToken, taskCreatOpt, TaskScheduler.FromCurrentSynchronizationContext()).ContinueWith(t =>
            {
                MainViewSnackbar.MessageQueue.Enqueue(MsgText.Instance.TransLatedString("Welcome02"));
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar)
                    return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }
    }
}