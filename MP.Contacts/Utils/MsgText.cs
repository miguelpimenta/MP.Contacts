using MP.Contacts.Support;
using System;

namespace MP.Contacts.Utils
{
    public sealed class MsgText : BindableBase
    {
        #region Singleton

        private static MsgText instance;
        private static readonly object lockThis = new object();

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Lang { get; set; } = Settings.Default.Culture;

        private MsgText()
        {
        }

        public static MsgText Instance
        {
            get
            {
                lock (lockThis)
                {
                    if (instance == null)
                    {
                        instance = new MsgText();
                    }
                    return instance;
                }
            }
        }

        #endregion Singleton

        public string Title { get => Lang.Equals("pt-PT") ? "Contactos" : "Contacts"; }
        public string Welcome01 { get => Lang.Equals("pt-PT") ? "Bemvido" + Environment.NewLine + "a aplicação MP.Contacts" : "Welcome" + Environment.NewLine + "to MP.Contacts App"; }
        public string Welcome02 { get => Lang.Equals("pt-PT") ? "Tenha um Bom Dia" : "Have a nice day :)"; }
        public string Info { get => Lang.Equals("pt-PT") ? "Informação!" : "Information!"; }
        public string Error { get => Lang.Equals("pt-PT") ? "Erro!" : "Error!"; }
        public string ErrorMSgDefault { get => Lang.Equals("pt-PT") ? "Algo não funcionou como era suposto." : "Something didn't work!"; }
        public string ErrorMSgFileSize { get => Lang.Equals("pt-PT") ? "Ficheiro com tamanho superior a 250kb." : "File with more than 250kb."; }
        public string PleaseWait { get => Lang.Equals("pt-PT") ? "Por favor Aguarde" : "Please Wait..."; }
        public string Waiting { get => Lang.Equals("pt-PT") ? "A processar informação..." : "Processing..."; }
        public string Loading { get => Lang.Equals("pt-PT") ? "A carregar informação..." : "Loading..."; }
        public string SaveSuccess { get => Lang.Equals("pt-PT") ? "Gravado com sucesso." : "Saved successfully."; }
        public string UpdateSuccess { get => Lang.Equals("pt-PT") ? "Actualizado com sucesso." : "Updated successfully."; }
        public string Name { get => Lang.Equals("pt-PT") ? "Nome" : "Name"; }
        public string Company { get => Lang.Equals("pt-PT") ? "Empresa" : "Company"; }
        public string Phone { get => Lang.Equals("pt-PT") ? "Telefone" : "Phone"; }
        public string CellPhone { get => Lang.Equals("pt-PT") ? "Telemóvel" : "CellPhone"; }
        public string Email { get => Lang.Equals("pt-PT") ? "E-Mail" : "E-Mail"; }
        public string WebPage { get => Lang.Equals("pt-PT") ? "Endereço Web" : "Webpage"; }
        public string Address { get => Lang.Equals("pt-PT") ? "Morada" : "Address"; }
        public string Locality { get => Lang.Equals("pt-PT") ? "Localidade" : "Locality"; }
        public string PostalCode { get => Lang.Equals("pt-PT") ? "Código Postal" : "Postal Code"; }
        public string Obs { get => Lang.Equals("pt-PT") ? "Observações" : "Observations"; }
        public string Save { get => Lang.Equals("pt-PT") ? "Gravar" : "Save"; }
        public string Cancel { get => Lang.Equals("pt-PT") ? "Cancelar" : "Cancel"; }
        public string Exit { get => Lang.Equals("pt-PT") ? "Sair" : "Exit"; }
        public string Search { get => Lang.Equals("pt-PT") ? "Pesquisar" : "Search"; }
        public string Refresh { get => Lang.Equals("pt-PT") ? "Refrescar" : "Refresh"; }
        public string NewPerson { get => Lang.Equals("pt-PT") ? "Novo Contacto" : "New Contact"; }
        public string OpenPhoto { get => Lang.Equals("pt-PT") ? "Adicionar Foto" : "Add Photo"; }
        public string Home { get => Lang.Equals("pt-PT") ? "Início" : "Home"; }
        public string OpenMenu { get => Lang.Equals("pt-PT") ? "Abrir Menu" : "Open Menu"; }
        public string FileMenu { get => Lang.Equals("pt-PT") ? "" : ""; }
        public string Contacts { get => Lang.Equals("pt-PT") ? "Contactos" : "Contacts"; }
        public string NewContact { get => Lang.Equals("pt-PT") ? "Novo Contacto" : "New Contacts"; }
        public string EditContact { get => Lang.Equals("pt-PT") ? "Alterar Contacto" : "Edit Contact"; }

        public void UpdateLanguage()
        {
            RaisePropertyChanged(null);
        }
    }
}