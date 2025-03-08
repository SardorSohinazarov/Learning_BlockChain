using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Index { get; }
    public string PreviousHash { get; }
    public string Data { get; }
    public DateTime Timestamp { get; }
    public string Hash { get; private set; }
    public int Nonce { get; private set; }  // 🔄 Yangi: Tasodifiy raqam

    public Block(int index, string previousHash, string data)
    {
        Index = index;
        PreviousHash = previousHash;
        Data = data;
        Timestamp = DateTime.UtcNow;
        Nonce = 0;  // 🔄 Boshlanishiga 0
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        string input = $"{Index}{PreviousHash}{Data}{Timestamp}{Nonce}";
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    // 🔄 Xesh yaratish: ma'lum miqdordagi 0 bilan boshlansin
    public void MineBlock(int difficulty)
    {
        string target = new string('0', difficulty);  // Masalan: "0000"
        while (!Hash.StartsWith(target))
        {
            Nonce++;
            Hash = CalculateHash();
        }
        Console.WriteLine($"Blok {Index} qazib olindi! Xesh: {Hash}");
    }
}
