/// <summary>
/// Sodda blokchain dasturini yaratamiz, 1- sidan farqi shuki
/// Mining (qazib olish) jarayonini qo‘shamiz
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Blockchain blockchain = new Blockchain();

        Console.WriteLine("1-blok qo‘shilmoqda...");
        blockchain.AddBlock("Ali → Vali: 1 BTC");

        Console.WriteLine("2-blok qo‘shilmoqda...");
        blockchain.AddBlock("Vali → Guli: 0.5 BTC");

        foreach (var block in blockchain.Chain)
        {
            Console.WriteLine($"Blok: {block.Index}, Xesh: {block.Hash}, Ma'lumot: {block.Data}");
        }

        Console.WriteLine("Blockchain haqiqiymi? " + blockchain.IsValid());
    }
}
