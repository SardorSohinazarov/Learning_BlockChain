/// <summary>
/// Sodda blokchain dasturini yaratamiz, 2- sidan farqi shuki
/// Transaction (tranzaksiya) qo‘shish
/// Minerlarga mukofot berish
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Blockchain blockchain = new Blockchain();

        Console.WriteLine("Tranzaktsiyalarni yaratamiz...");
        blockchain.CreateTransaction(new Transaction("Ali", "Vali", 10));
        blockchain.CreateTransaction(new Transaction("Vali", "Guli", 5));

        Console.WriteLine("Bloklarni qazib olamiz...");
        blockchain.MinePendingTransactions("Miner1");

        Console.WriteLine($"Ali balans: {blockchain.GetBalance("Ali")} BTC");
        Console.WriteLine($"Vali balans: {blockchain.GetBalance("Vali")} BTC");
        Console.WriteLine($"Guli balans: {blockchain.GetBalance("Guli")} BTC");
        Console.WriteLine($"Miner1 balans: {blockchain.GetBalance("Miner1")} BTC");

        Console.WriteLine("Blockchain haqiqiymi? " + blockchain.IsValid());
    }
}
