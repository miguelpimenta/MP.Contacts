using MP.Contacts.ViewModels;
using System.Windows.Controls;

namespace MP.Contacts.Views
{
    /// <summary>
    /// Interaction logic for PersonsViewView.xaml
    /// </summary>
    public partial class PersonsView : UserControl
    {
        public PersonsView()
        {
            InitializeComponent();
            DataContext = PersonsViewModel.Instance;
        }
    }
}