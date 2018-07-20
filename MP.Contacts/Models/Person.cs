using LiteDB;
using MP.Contacts.Support;
using System.Collections.Generic;

namespace MP.Contacts.Models
{
    public class Person : BindableBase
    {
        private ObjectId _pkIdPerson = new ObjectId();
        private string _name = string.Empty;

        [BsonId]
        [BsonField("_id")]
        public ObjectId PkIdPerson
        {
            get => _pkIdPerson;
            set { _pkIdPerson = value; RaisePropertyChanged(nameof(PkIdPerson)); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; RaisePropertyChanged(nameof(Name)); }
        }

        public List<Contact> Contacts { get; set; }
    }
}