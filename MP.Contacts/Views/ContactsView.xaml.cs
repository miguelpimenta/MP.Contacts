using MP.Contacts.ViewModels;
using System.Windows.Controls;

namespace MP.Contacts.Views
{
    /// <summary>
    /// Interaction logic for ContactsViewView.xaml
    /// </summary>
    public partial class ContactsView : UserControl
    {
        public ContactsView()
        {
            InitializeComponent();
            DataContext = ContactsViewModel.Instance;
        }
    }
}