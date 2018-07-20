using System.Text;

namespace MP.Contacts.DAL
{
    internal static class LitedbConn
    {
        public static string ConnString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Settings.Default.DBPath))
            {
                sb.Append(Settings.Default.DBPath).Append("\\");
            }
            sb.Append(Settings.Default.DBFilename);
            return sb.ToString();

            //return "ICDCodes.db4";
        }
    }
}