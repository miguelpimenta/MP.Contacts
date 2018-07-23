using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        /// http://www.extensionmethod.net/1592/csharp/array/toimage
        public static Image ToImage(this byte[] bytes)
        {
            if (bytes != null)
            {
                return Image.FromStream(new MemoryStream(bytes));
            }
            else
            {
                return null;
            }
        }

        public static byte[] ConvertToByteArray(this System.IO.Stream stream)
        {
            var streamLength = Convert.ToInt32(stream.Length);
            byte[] data = new byte[streamLength + 1];
            // Convert to to a byte array
            stream.Read(data, 0, streamLength);
            stream.Close();
            return data;
        }

        public static byte[] ImageToByteArray(this Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        public static ImageSource ByteToImageSource(this byte[] imageData)
        {
            BitmapImage bitImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            bitImg.BeginInit();
            bitImg.StreamSource = ms;
            bitImg.EndInit();
            ImageSource imgSrc = bitImg as ImageSource;
            return imgSrc;
        }
    }
}