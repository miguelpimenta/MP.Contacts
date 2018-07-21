using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Globalization;

namespace MP.Contacts.Utils
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            ObservableCollection<T> list = new ObservableCollection<T>();

            foreach (T item in collection)
            {
                list.Add(item);
            }

            return list;
        }

        public static string RemoveDiacritics(this string text)
        {
            return string.Concat(text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark))
                .Normalize(NormalizationForm.FormC);
        }
    }
}