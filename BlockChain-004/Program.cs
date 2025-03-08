using BlockChain_004;
using System;
using System.Reflection.Metadata;

/// <summary>
/// Sodda blokchain dasturini yaratamiz, 3- sidan farqi shuki
/// 🛡️ 1. Tranzaktsiyalarni tasdiqlash (Validatsiya):
/// Hozircha, kim bo'lsa ham tranzaktsiya yarata oladi. Buni oldini olish uchun:
/// E-imzo (Digital Signature): Har bir tranzaktsiya imzo bilan tasdiqlanishi kerak.
/// Umumiy kalit (Public Key): Kimdan kelganini tekshirish uchun kerak.
/// </summary>

class Program
{
    static void Main()
    {
        var blockchain = new Blockchain();
        var wallet = new Wallet();

        Console.WriteLine("Shaxsiy kalit: " + wallet.PrivateKey);
        Console.WriteLine("Umumiy kalit: " + wallet.PublicKey);

        // 🤑 Tranzaktsiya yaratamiz va imzolaymiz
        var transaction = new Transaction
        {
            FromAddress = wallet.PublicKey,
            ToAddress = "ValiUmumiyKalit",
            Amount = 10
        };
        transaction.SignTransaction(wallet.PrivateKey);

        // ➕ Tranzaktsiyani qo'shamiz
        blockchain.AddTransaction(transaction);

        // ⛏️ Tranzaktsiyalarni qazib olamiz
        blockchain.MinePendingTransactions(wallet.PublicKey);

        // 🔍 Balansni tekshiramiz
        Console.WriteLine("Mening balans: " + blockchain.GetBalance(wallet.PublicKey) + " BTC");
        Console.WriteLine("Vali balans: " + blockchain.GetBalance("ValiUmumiyKalit") + " BTC");

        // ✅ Blockchain haqiqiymi?
        Console.WriteLine("Blockchain haqiqiymi? " + blockchain.IsChainValid());
    }
}

//Shaxsiy kalit: MIGiAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQggLihx12zR6bPYkMlU6fPZJsvTdb4rEx7AvoN5e + yaXKgCgYIKoZIzj0DAQehRANCAAQp0YqVQaTE2i67NdQb7tQZfYi9s8 / Duif4HqJQN2CHHQEJO69gNPT6pYxxw9kXjJHu / lDKLuswKt1kMtCQajHLoA0wCwYDVR0PMQQDAgCA
//Umumiy kalit: MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEKdGKlUGkxNouuzXUG + 7UGX2IvbPPw7on + B6iUDdghx0BCTuvYDT0 + qWMccPZF4yR7v5Qyi7rMCrdZDLQkGoxyw ==
//Bloklarni qazib olamiz...
//Blok 1 qazib olindi! Xesh: 000028743D34DDA6DC3D15C2E2A31B0E9212CB90949A920D64C6083F024374CF
//Mening balans: -10 BTC
//Vali balans: 10 BTC
//Blockchain haqiqiymi? True
