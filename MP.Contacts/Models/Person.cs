using LiteDB;
using MP.Contacts.Support;
using System.Collections.Generic;

namespace MP.Contacts.Models
{
    public class Person : BindableBase
    {
        private ObjectId _pkIdPerson = new ObjectId();
        private string _name = string.Empty;
        private string _company = string.Empty;
        private string _address = string.Empty;
        private string _locality = string.Empty;
        private string _postalCode = string.Empty;
        private string _email = string.Empty;
        private string _phone = string.Empty;
        private string _cellPhone = string.Empty;
        private string _webSite = string.Empty;
        private string _obs = string.Empty;
        private bool _active = true;

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

        public string Company
        {
            get => _company;
            set { _company = value; RaisePropertyChanged(nameof(Company)); }
        }

        public string Address
        {
            get => _address;
            set { _address = value; RaisePropertyChanged(nameof(Address)); }
        }

        public string Locality
        {
            get => _locality;
            set { _locality = value; RaisePropertyChanged(nameof(Locality)); }
        }

        public string PostalCode
        {
            get => _postalCode;
            set { _postalCode = value; RaisePropertyChanged(nameof(PostalCode)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; RaisePropertyChanged(nameof(Email)); }
        }

        public string Phone
        {
            get => _phone;
            set { _phone = value; RaisePropertyChanged(nameof(Phone)); }
        }

        public string CellPhone
        {
            get => _cellPhone;
            set { _cellPhone = value; RaisePropertyChanged(nameof(CellPhone)); }
        }

        public string WebSite
        {
            get => _webSite;
            set { _webSite = value; RaisePropertyChanged(nameof(WebSite)); }
        }

        public string Obs
        {
            get => _obs;
            set { _obs = value; RaisePropertyChanged(nameof(Obs)); }
        }

        public bool Active
        {
            get => _active;
            set { _active = value; RaisePropertyChanged(nameof(Active)); }
        }

        public List<Contact> Contacts { get; set; }
    }
}