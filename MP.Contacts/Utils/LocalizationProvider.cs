using System.Reflection;
using WPFLocalizeExtension.Extensions;

namespace MP.Contacts.Utils
{
    /// <summary>
    /// http://stackoverflow.com/questions/28137882/how-to-use-wpflocalizeextension-in-code-behind
    /// </summary>
    public static class LocalizationProvider
    {
        public static T GetLocalizedValue<T>(string key)
        {
            return LocExtension.GetLocalizedValue<T>(Assembly.GetCallingAssembly().GetName().Name + ":Resources:" + key);
        }
    }
}