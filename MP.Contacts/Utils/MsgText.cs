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

        public string Info { get; internal set; } = "Informação!";
        public string Error { get; internal set; } = "Erro!";
        public string ErrorMSg { get; internal set; } = "Algo não funcionou como era suposto.";
        public string PleaseWait { get; internal set; } = "Por favor Aguarde";
        public string Waiting { get; internal set; } = "A processar informação...";
        public string Loading { get; internal set; } = "A carregar informação...";
        public string SaveSuccess { get; internal set; } = "Gravado com sucesso.";
        public string UpdateSuccess { get; internal set; } = "Actualizado com sucesso.";
    }
}