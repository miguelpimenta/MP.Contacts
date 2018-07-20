using MP.Contacts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.Contacts.DAL
{
    internal interface ILitedbDAL : IDisposable
    {
        bool InsertPerson(Person person);

        bool UpdatePerson(Person person);

        bool DeletePerson(Person person);

        ObservableCollection<Person> ListPersons(string search, string empty1, string empty2, string empty3);

        bool InsertContact(Contact contact);

        bool UpdateContact(Contact contact);

        bool DeleteContact(Contact contact);

        ObservableCollection<Contact> ListContacts(string search, string empty1, string empty2, string empty3);
    }
}