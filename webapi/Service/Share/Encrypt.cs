using System.Text;

namespace Wallet.Service.Util
{
    public class Encrypt
    {
        private string Base64Decode(string sourceString)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(sourceString));
        }
        private string Base64Encode(string base64String)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(base64String));
        }

        /// <summary>
        /// MD5 Encrypt
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ToMD5(string str)
        {
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                //將字串編碼成 UTF8 位元組陣列
                var bytes = Encoding.UTF8.GetBytes(str);

                //取得雜湊值位元組陣列
                var hash = cryptoMD5.ComputeHash(bytes);

                //取得 MD5
                var md5 = BitConverter.ToString(hash)
                  .Replace("-", String.Empty)
                  .ToLower();

                return md5;
            }
        }
    }
}
