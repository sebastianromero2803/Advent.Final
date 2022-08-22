using System.Security.Cryptography;
using System.Text;

namespace Advent.Final.Core.V1
{
    public class EncryptCore
    {
        public string Encrypt_SHA256(string valueEncrypt, string claveEncrypt)
        {
            string vEncrypt = $"{valueEncrypt}, {claveEncrypt}-advent.com";

            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            StringBuilder sbEncrypt = new StringBuilder();
            byte[] streamEncrypt = null;

            streamEncrypt = sha256.ComputeHash(encoding.GetBytes(vEncrypt));
            for (int i = 0; i < streamEncrypt.Length; i++)
            {
                sbEncrypt.AppendFormat("{0:x2}", streamEncrypt[i]);
            }

            return sbEncrypt.ToString();
        }
    }

}
