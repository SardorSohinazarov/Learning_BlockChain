/// <summary>
/// Sodda blokchain dasturini yaratamiz
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Blockchain blockchain = new Blockchain();

        // Yangi bloklar qo‘shamiz
        blockchain.AddBlock("Ali → Vali: 1 BTC");
        blockchain.AddBlock("Vali → Guli: 0.5 BTC");

        // Har bir blokni konsolda chiqaramiz
        foreach (var block in blockchain.Chain)
        {
            Console.WriteLine($"Blok: {block.Index}");
            Console.WriteLine($"Oldingi Xesh: {block.PreviousHash}");
            Console.WriteLine($"Xesh: {block.Hash}");
            Console.WriteLine($"Ma'lumot: {block.Data}");
            Console.WriteLine("----------------------");
        }

        // Blockchain haqiqiyligini tekshiramiz
        Console.WriteLine("Blockchain haqiqiymi? " + blockchain.IsValid());
    }
}
