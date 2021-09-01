using System;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Tools
{
    public class GeneralTool
    {
        public static string UrlConvert(string _text)
        {
            return _text.ToLower().Replace(" ", "-").Replace("ç", "c").Replace("ş", "s").Replace("ğ", "g").Replace("ü", "u").Replace("ö", "o").Replace("ı", "i");
        }

        public static string getMD5(string _text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_text));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}
