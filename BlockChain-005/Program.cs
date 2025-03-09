/// <summary>
/// 🏗 Oxirgi versiyada nimalar bor:
/// 
/// </summary>

using BlockChain_005;

class Program
{
    static void Main()
    {
        var blockchain = new Blockchain();
        var wallet = new Wallet();

        var transaction = new Transaction
        {
            FromAddress = wallet.PublicKey,
            ToAddress = "ValiUmumiyKalit",
            Amount = 10
        };

        transaction.SignTransaction(wallet.PrivateKey);

        blockchain.AddTransaction(transaction);
        blockchain.MinePendingTransactions(wallet.PublicKey);

        Console.WriteLine("Meni balans: " + blockchain.GetBalance(wallet.PublicKey) + " BTC");
        Console.WriteLine("Vali balans: " + blockchain.GetBalance("ValiUmumiyKalit") + " BTC");
        Console.WriteLine("Blockchain haqiqiymi? " + blockchain.IsChainValid());
    }
}