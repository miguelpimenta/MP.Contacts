using MP.Contacts.ViewModels;
using System.Windows.Controls;

namespace MP.Contacts.Views.Flyouts
{
    /// <summary>
    /// Interaction logic for ContactView.xaml
    /// </summary>
    public partial class ContactView : UserControl
    {
        public ContactView()
        {
            InitializeComponent();
            DataContext = new ContactViewModel();
        }
    }
}