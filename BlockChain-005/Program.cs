/// <summary>
/// 🏗 Oxirgi versiyada nimalar bor:
/// Kriptografik imzolar bilan xavfsizlikni oshirish.
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

        var transaction2 = new Transaction
        {
            FromAddress = wallet.PublicKey,
            ToAddress = "ValiUmumiyKalit",
            Amount = 5
        };
        transaction2.SignTransaction(wallet.PrivateKey);
        blockchain.AddTransaction(transaction2);

        // ⛏️ Mining
        blockchain.MinePendingTransactions(wallet.PublicKey);

        Console.WriteLine("Meni balans: " + blockchain.GetBalance(wallet.PublicKey) + " BTC");
        Console.WriteLine("Vali balans: " + blockchain.GetBalance("ValiUmumiyKalit") + " BTC");
        Console.WriteLine("Blockchain haqiqiymi? " + blockchain.IsChainValid());

        // ✅ Blockchains
        blockchain.PrintBlockchain();

        // 📜 Tranzaktsiyalar tarixi
        Console.WriteLine($"\n\n\nTransaktsiya tarixi, Address:{wallet.PublicKey}:");
        var history = blockchain.GetTransactionHistory(wallet.PublicKey);
        foreach (var tx in history)
        {
            Console.WriteLine(tx.GetTransactionData());
        }
    }
}

//Bloklarni qazib olamiz...
//Blok 1 qazib olindi! Xesh: 00006B3EF2FF0B5EA96AF51C01544CA78EDA46FE5E3D6FC9892D8DDB189F541A
//Meni balans: -15 BTC
//Vali balans: 15 BTC
//Blockchain haqiqiymi? True
//Blok: 0, Xesh: DD2AD62730EF0D46D51FC0BFD122744B1C83CB1BF2A9BD111C32A9D458334936, Oldingi Xesh: 0
//Tranzaktsiyalar:
//-------------------------------------
//------------------------------------------------------------------------------
//Blok: 1, Xesh: 00006B3EF2FF0B5EA96AF51C01544CA78EDA46FE5E3D6FC9892D8DDB189F541A, Oldingi Xesh: DD2AD62730EF0D46D51FC0BFD122744B1C83CB1BF2A9BD111C32A9D458334936
//Tranzaktsiyalar:
//-------------------------------------
//Id:b63fb0d7 - 1aac - 477d - 9c04 - 45e02e4c21b1
//From:-----BEGIN PUBLIC KEY-----
//MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEwR7oApFwqaPAjKKowo7o0uHMaEhv91UOjMi8Er7l
//ga68kpjiawjdbYkXPr3XXZWDBzNmcb9OkMKqRRPyCUMx2g==
//-----END PUBLIC KEY-----

//To:ValiUmumiyKalit
//Amount:10
//Comission: 0.1
//------------------------------------ -
//Id:5609dafe - 9384 - 4649 - aa6c - cd71b220b488
//From: -----BEGIN PUBLIC KEY-----
//MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEwR7oApFwqaPAjKKowo7o0uHMaEhv91UOjMi8Er7l
//ga68kpjiawjdbYkXPr3XXZWDBzNmcb9OkMKqRRPyCUMx2g==
//-----END PUBLIC KEY-----

//To:ValiUmumiyKalit
//Amount:5
//Comission: 0.1
//------------------------------------ -
//------------------------------------------------------------------------------



//Transaktsiya tarixi, Address:-----BEGIN PUBLIC KEY-----
//MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEwR7oApFwqaPAjKKowo7o0uHMaEhv91UOjMi8Er7l
//ga68kpjiawjdbYkXPr3XXZWDBzNmcb9OkMKqRRPyCUMx2g==
//-----END PUBLIC KEY-----
//:
//Id: b63fb0d7 - 1aac - 477d - 9c04 - 45e02e4c21b1
//From:-----BEGIN PUBLIC KEY-----
//MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEwR7oApFwqaPAjKKowo7o0uHMaEhv91UOjMi8Er7l
//ga68kpjiawjdbYkXPr3XXZWDBzNmcb9OkMKqRRPyCUMx2g==
//-----END PUBLIC KEY-----

//To:ValiUmumiyKalit
//Amount:10
//Comission: 0.1
//Id: 5609dafe - 9384 - 4649 - aa6c - cd71b220b488
//From: -----BEGIN PUBLIC KEY-----
//MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEwR7oApFwqaPAjKKowo7o0uHMaEhv91UOjMi8Er7l
//ga68kpjiawjdbYkXPr3XXZWDBzNmcb9OkMKqRRPyCUMx2g==
//-----END PUBLIC KEY-----

//To:ValiUmumiyKalit
//Amount:5
//Comission: 0.1