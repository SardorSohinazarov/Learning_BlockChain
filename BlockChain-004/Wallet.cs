using System.Security.Cryptography;

namespace BlockChain_004
{
    public class Wallet
    {
        public string PrivateKey { get; private set; }  // Shaxsiy kalit
        public string PublicKey { get; private set; }   // Umumiy kalit

        public Wallet()
        {
            using (ECDsaCng ecdsa = new ECDsaCng())
            {
                ecdsa.KeySize = 256;
                PrivateKey = Convert.ToBase64String(ecdsa.ExportPkcs8PrivateKey());
                PublicKey = Convert.ToBase64String(ecdsa.ExportSubjectPublicKeyInfo());
            }
        }
    }
}
