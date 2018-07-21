using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using MP.Contacts.Models;
using MP.Contacts.Utils;

namespace MP.Contacts.DAL
{
    internal class LitedbDAL : ILitedbDAL, IEquatable<LitedbDAL>
    {
        #region Props

        public string PersonsTable { get; set; } = "Persons";
        public string ContactsTable { get; set; } = "Contacts";

        #endregion Props

        #region Contructor

        public LitedbDAL()
        {
        }

        #endregion Contructor

        #region Persons

        bool ILitedbDAL.InsertPerson(Person person)
        {
            using (var db = new LiteDatabase(LitedbConn.ConnString()))
            {
                var personsTbl = db.GetCollection<Person>(PersonsTable);
                try
                {
                    personsTbl.Insert(person);
                    personsTbl.EnsureIndex(x => x.PkIdPerson);
                    return true;
                }
                catch (Exception ex)
                {
                    Log2Txt.Instance.ErrorLog(ex.ToString());
                    throw;
                }
            }
        }

        bool ILitedbDAL.UpdatePerson(Person person)
        {
            using (var db = new LiteDatabase(LitedbConn.ConnString()))
            {
                var personsTbl = db.GetCollection<Person>(PersonsTable);
                try
                {
                    personsTbl.Update(person);
                    personsTbl.EnsureIndex(x => x.PkIdPerson);
                    return true;
                }
                catch (Exception ex)
                {
                    Log2Txt.Instance.ErrorLog(ex.ToString());
                    throw;
                }
            }
        }

        bool ILitedbDAL.DeletePerson(Person person)
        {
            throw new NotImplementedException();
        }

        ObservableCollection<Person> ILitedbDAL.ListPersons(string search, string empty1, string empty2, string empty3)
        {
            var searchWords = search.ToUpper().Split(' ');
            using (var db = new LiteDatabase(LitedbConn.ConnString()))
            {
                var personsTbl = db.GetCollection<Person>(PersonsTable);
                try
                {
                    var results = personsTbl.FindAll()
                        .Where(x => searchWords.All(x.Name.ToUpper().Contains) || searchWords.All(x.Company.ToUpper().Contains))
                        .OrderBy(x => x.Name)
                        .Take(250);
                    return results.AsEnumerable().ToObservableCollection<Person>();
                }
                catch (Exception ex)
                {
                    Log2Txt.Instance.ErrorLog(ex.ToString());
                    throw;
                }
            }
        }

        #endregion Persons

        #region Contacts

        bool ILitedbDAL.InsertContact(Contact contact)
        {
            using (var db = new LiteDatabase(LitedbConn.ConnString()))
            {
                var contactsTbl = db.GetCollection<Contact>(ContactsTable);
                try
                {
                    contactsTbl.Insert(contact);
                    contactsTbl.EnsureIndex(x => x.PkIdContact);
                    return true;
                }
                catch (Exception ex)
                {
                    Log2Txt.Instance.ErrorLog(ex.ToString());
                    throw;
                }
            }
        }

        bool ILitedbDAL.UpdateContact(Contact contact)
        {
            using (var db = new LiteDatabase(LitedbConn.ConnString()))
            {
                var contactsTbl = db.GetCollection<Contact>(ContactsTable);
                try
                {
                    contactsTbl.Update(contact);
                    contactsTbl.EnsureIndex(x => x.PkIdContact);
                    return true;
                }
                catch (Exception ex)
                {
                    Log2Txt.Instance.ErrorLog(ex.ToString());
                    throw;
                }
            }
        }

        bool ILitedbDAL.DeleteContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        ObservableCollection<Contact> ILitedbDAL.ListContacts(string search, string empty1, string empty2, string empty3)
        {
            throw new NotImplementedException();
        }

        #endregion Contacts

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LitedbDAL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        public bool Equals(LitedbDAL other)
        {
            throw new NotImplementedException();
        }

        #endregion IDisposable Support
    }
}