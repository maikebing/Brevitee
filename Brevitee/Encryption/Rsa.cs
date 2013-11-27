using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Encryption
{
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
    }
}
