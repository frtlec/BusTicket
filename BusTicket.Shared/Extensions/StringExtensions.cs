using System.Text;

namespace BusTicket.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string Crop(this string str, int maxLength = 50)
        {
            if (str.Length > 50)
                return str.Substring(0, maxLength);

            return str;
        }
        public static string EncodeBase64(this string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
        public static string DecodeBase64(this string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
