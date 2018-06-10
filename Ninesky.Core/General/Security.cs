using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Ninesky.Core.General
{
    public class Security
    {
        ///<summary>
        ///256位散列加密
        ///</summary>
        ///<param name="plainText">明文</param>
        ///<returns>密文</returns>
        public static string Sha256(string plainText)
        {
            SHA256Managed _sha256 = new SHA256Managed();
            byte[] _cipherText = _sha256.ComputeHash(Encoding.Default.GetBytes(plainText));
            //string mima = Convert.ToBase64String(_cipherText);
            //Console.Write(mima);
            return Convert.ToBase64String(_cipherText);
        }
    }
}
