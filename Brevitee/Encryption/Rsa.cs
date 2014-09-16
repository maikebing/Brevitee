using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Encryption
{
    [Obsolete("This class should be moved to the Encryption project and switched over to BouncyCastle")]
    public static class Rsa
    {
        public static string Encrypt(string value)
        {
            return RsaKeyPair.Default.EncryptWithPublicKey(value);
        }

        public static string Decrypt(string cipherText)
        {
            return RsaKeyPair.Default.DecryptWithPrivateKey(cipherText);
        }

        public static string GetPublicKey()
        {
            return RsaKeyPair.Default.PublicKeyXml;
        }
    }
}
