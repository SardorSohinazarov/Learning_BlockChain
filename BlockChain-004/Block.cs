// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

public class Block
{
    public int Index { get; set; }
    public string PreviousHash { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    public DateTime Timestamp { get; set; }
    public string Hash { get; set; }
    public int Nonce { get; set; }

    public Block()
    {
        Timestamp = DateTime.UtcNow;
        Nonce = 0;
        Hash = CalculateHash();
    }

    public Block(int index, string previousHash, List<Transaction> transactions)
    {
        Index = index;
        PreviousHash = previousHash;
        Transactions = transactions;
        Timestamp = DateTime.UtcNow;
        Nonce = 0;
        Hash = CalculateHash();
    }

    // 🔄 Blok uchun xesh yaratamiz
    public string CalculateHash()
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string rawData = $"{Index}{Timestamp}{PreviousHash}{Nonce}{string.Join("", Transactions.Select(t => t.GetTransactionData()))}";
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return Convert.ToHexString(bytes);
        }
    }

    // 🔒 Blokdagi tranzaktsiyalarni tekshirish
    public bool HasValidTransactions()
    {
        return Transactions.All(tx => tx.IsValid());
    }

    public void MineBlock(int difficulty)
    {
        string target = new string('0', difficulty);
        while (!Hash.StartsWith(target))
        {
            Nonce++;
            Hash = CalculateHash();
        }
        Console.WriteLine($"Blok {Index} qazib olindi! Xesh: {Hash}");
    }
}
