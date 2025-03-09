using System.Security.Cryptography;
using System.Text;

namespace BlockChain_005
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public int Nonce { get; set; }

        // 🏗 Blok yaratish
        public Block() 
            => Hash = CalculateHash();

        // 🔄 Blok uchun xesh yaratamiz
        public string CalculateHash()
        {
            using var sha256 = SHA256.Create();
            string rawData = $"{Index}{GetFormattedTimestamp()}{PreviousHash}{Nonce}{string.Join("", Transactions.Select(t => t.GetTransactionData()))}";
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return Convert.ToHexString(bytes);
        }

        public string GetFormattedTimestamp()
            => Timestamp.ToString("yyyy-MM-dd HH:mm:ss");

        // 🔒 Blokdagi tranzaktsiyalarni tekshirish
        public bool HasValidTransactions()
            => Transactions.All(tx => tx.IsValid());
    }
}
