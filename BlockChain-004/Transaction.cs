// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;

public class Transaction
{
    public string FromAddress { get; set; }  // 🔄 Kimdan
    public string ToAddress { get; set; }    // 🔄 Kimga
    public decimal Amount { get; set; }      // 🔄 Miqdor
    public string Signature { get; set; }    // 🆕 Imzo (Yangi xususiyat)

    // ✅ Imzoni tekshirish
    public bool IsValid()
    {
        if (string.IsNullOrEmpty(FromAddress))
            return true;  // Mukofot tranzaktsiyasi

        if (string.IsNullOrEmpty(Signature))
            throw new Exception("Imzo mavjud emas!");

        using (ECDsaCng ecdsa = new ECDsaCng())
        {
            byte[] transactionData = Encoding.UTF8.GetBytes(GetTransactionData());
            byte[] signature = Convert.FromBase64String(Signature);

            ecdsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(FromAddress), out _);
            return ecdsa.VerifyData(transactionData, signature, HashAlgorithmName.SHA256);
        }
    }

    // 🔏 Tranzaktsiyani imzolash
    public void SignTransaction(string privateKey)
    {
        if (string.IsNullOrEmpty(FromAddress))
            throw new Exception("Mukofot tranzaktsiyasi uchun imzo talab qilinmaydi.");

        using (ECDsaCng ecdsa = new ECDsaCng())
        {
            ecdsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
            byte[] transactionData = Encoding.UTF8.GetBytes(GetTransactionData());
            byte[] signature = ecdsa.SignData(transactionData, HashAlgorithmName.SHA256);
            Signature = Convert.ToBase64String(signature);
        }
    }

    // 🔄 Tranzaktsiya ma'lumotlarini qaytaruvchi metod
    public string GetTransactionData()
    {
        return $"{FromAddress}{ToAddress}{Amount}";
    }

    public Transaction()
    {
        
    }

    public Transaction(string fromAddress, string toAddress, decimal amount)
    {
        FromAddress = fromAddress;
        ToAddress = toAddress;
        Amount = amount;
    }
}