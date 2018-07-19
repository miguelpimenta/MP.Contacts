using LiteDB;
using MP.Contacts.Support;

namespace MP.Contacts.Models
{
    public class Contact : BindableBase
    {
        private ObjectId _pkIdContact = new ObjectId();

        [BsonId]
        [BsonField("_id")]
        public ObjectId PkIdContact
        {
            get => _pkIdContact;
            set { _pkIdContact = value; RaisePropertyChanged(nameof(PkIdContact)); }
        }

        [BsonRef("Persons")]
        public virtual Person Person { get; set; }

        [BsonRef("Binaries")]
        public virtual Binary Binary { get; set; }
    }
}