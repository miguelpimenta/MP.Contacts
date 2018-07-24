using LiteDB;
using MP.Contacts.Support;
using System.Collections.Generic;
using System.IO;

//using System.Windows.Controls;
using System.Windows.Media;
using System.Drawing;
using System.Drawing.Imaging;
using MP.Contacts.Utils;
using System;

namespace MP.Contacts.Models
{
    public class Person : BindableBase
    {
        private ObjectId _pkIdPerson = ObjectId.NewObjectId();
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
        private DateTime _insertDate = DateTime.Now;
        private bool _active = true;
        private Binary _binary;

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

        public DateTime InsertDate
        {
            get => _insertDate;
            set { _insertDate = value; RaisePropertyChanged(nameof(InsertDate)); }
        }

        public bool Active
        {
            get => _active;
            set { _active = value; RaisePropertyChanged(nameof(Active)); }
        }

        //[BsonRef("Binaries")]
        public Binary Binary
        {
            get => _binary;
            set { _binary = value; RaisePropertyChanged(nameof(Foto)); }
        }

        [BsonIgnore] //For now
        public List<Contact> Contacts { get; set; }

        [BsonIgnore]
        public ImageSource Foto
        {
            get
            {
                if (Binary?.FileBytes.Length > 50)
                {
                    var s = new ImageSourceConverter();
                    return (ImageSource)s.ConvertFrom(Binary.FileBytes);
                }
                else
                {
                    try
                    {
                        Image img = Properties.Resources.User;
                        byte[] buffer;
                        var memoryStream = new MemoryStream();
                        img.Save(memoryStream, ImageFormat.Png);
                        buffer = memoryStream.ToArray();
                        return buffer.ByteToImageSource();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }
    }
}