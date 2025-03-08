using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Index { get; }
    public string PreviousHash { get; }
    public List<Transaction> Transactions { get; }  // 🔄 Yangi: Tranzaktsiyalar
    public DateTime Timestamp { get; }
    public string Hash { get; private set; }
    public int Nonce { get; private set; }

    public Block(int index, string previousHash, List<Transaction> transactions)
    {
        Index = index;
        PreviousHash = previousHash;
        Transactions = transactions;
        Timestamp = DateTime.UtcNow;
        Nonce = 0;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        string transactionsData = string.Join(",", Transactions.Select(t => $"{t.FromAddress}-{t.ToAddress}-{t.Amount}"));
        string input = $"{Index}{PreviousHash}{transactionsData}{Timestamp}{Nonce}";
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
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
