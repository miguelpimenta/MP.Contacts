using MP.Contacts.Models;
using MP.Contacts.ViewModels;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace MP.Contacts.Views.Flyouts
{
    /// <summary>
    /// Interaction logic for PersonView.xaml
    /// </summary>
    public partial class PersonView : UserControl
    {
        //public PersonView()
        //{
        //    InitializeComponent();
        //    DataContext = new PersonViewModel();
        //}

        public PersonView(bool newPerson, Person person = null)
        {
            InitializeComponent();
            DataContext = new PersonViewModel(newPerson, person);
        }

        private void OnlyDigits(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}