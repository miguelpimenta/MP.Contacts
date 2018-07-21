namespace MP.Contacts.Utils
{
    public sealed class MsgText
    {
        #region Singleton

        private static MsgText instance;
        private static readonly object lockThis = new object();

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

        public string Info { get; set; } = "Informação!";
        public string Error { get; set; } = "Erro!";
        public string ErrorMSg { get; set; } = "Algo não funcionou como era suposto.";
        public string PleaseWait { get; set; } = "Por favor Aguarde";
        public string Waiting { get; set; } = "A processar informação...";
        public string Loading { get; set; } = "A carregar informação...";
    }
}