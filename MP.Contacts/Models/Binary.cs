using System;
using LiteDB;

namespace MP.Contacts.Models
{
    public class Binary
    {
        [BsonId]
        [BsonField("_id")]
        public ObjectId PkIdBinary { get; set; } = ObjectId.NewObjectId();

        public byte[] FileBytes { get; set; } = new byte[0];
        public string FileType { get; set; } = string.Empty;

        public Binary()
        {
        }
    }
}