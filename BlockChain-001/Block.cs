// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Index { get; }            // Blok raqami
    public string PreviousHash { get; }  // Oldingi blokning xeshi
    public string Data { get; }          // Ma'lumot (masalan, tranzaktsiya)
    public DateTime Timestamp { get; }   // Vaqt
    public string Hash { get; private set; }  // Joriy blokning xeshi

    public Block(int index, string previousHash, string data)
    {
        Index = index;
        PreviousHash = previousHash;
        Data = data;
        Timestamp = DateTime.UtcNow;
        Hash = CalculateHash();  // Xeshni hisoblaymiz
    }

    // Xesh yaratish uchun SHA-256 ishlatamiz
    public string CalculateHash()
    {
        string input = $"{Index}{PreviousHash}{Data}{Timestamp}";
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
