using System.Security.Cryptography;
using System.Text;

public class Transaction
{
    public string FromAddress { get; set; }        // 🏠 Jo'natuvchi manzili (jamoat kaliti)
    public string ToAddress { get; set; }          // 🏠 Qabul qiluvchi manzili
    public decimal Amount { get; set; }            // 💸 Miqdor
    public string Signature { get; private set; }  // ✍️ Imzo

    // 🔏 Tranzaktsiyani imzolash
    public void SignTransaction(string privateKey)
    {
        if (string.IsNullOrEmpty(FromAddress))
            return;  // ⛏ Mukofot tranzaktsiyalari (Mining) imzolanmaydi

        using var ecdsa = ECDsa.Create();

        // 🔄 PEM formatdagi xususiy kalitni tozalash
        string cleanedKey = privateKey
            .Replace("-----BEGIN PRIVATE KEY-----", "")
            .Replace("-----END PRIVATE KEY-----", "")
            .Replace("\r", "")
            .Replace("\n", "");

        // 🛠 Base64 formatdagi kalitni byte[] ko‘rinishga o‘tkazish
        byte[] privateKeyBytes = Convert.FromBase64String(cleanedKey);

        // 🔄 `ImportECPrivateKey` bilan yuklash (EC kalitlari uchun)
        ecdsa.ImportECPrivateKey(privateKeyBytes, out _);

        string txData = GetTransactionData();
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(txData));  // 🔒 Tranzaktsiya xeshini olish

        byte[] signatureBytes = ecdsa.SignHash(hash);  // ✍️ Xeshni imzolash
        Signature = Convert.ToBase64String(signatureBytes);  // 📜 Imzoni Base64 formatga o'tkazish
    }

    // 🔍 Tranzaktsiyani tekshirish
    public bool IsValid()
    {
        if (string.IsNullOrEmpty(FromAddress))
            return true;  // ⛏ Mukofot uchun tranzaktsiyalar haqiqiy hisoblanadi

        if (string.IsNullOrEmpty(Signature))
            throw new Exception("Imzo yo'q!");  // ⚠️ Imzo mavjud emas

        // 📄 PEM formatdagi jamoat kalitini tozalash
        string base64Key = FromAddress
            .Replace("-----BEGIN PUBLIC KEY-----", "")
            .Replace("-----END PUBLIC KEY-----", "")
            .Replace("\n", "")
            .Replace("\r", "");

        byte[] publicKeyBytes = Convert.FromBase64String(base64Key);

        using var ecdsa = ECDsa.Create();
        ecdsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);  // 🛠 Jamoat kalitini yuklash

        string txData = GetTransactionData();
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(txData));  // 🔒 Tranzaktsiya xeshini olish

        byte[] signatureBytes = Convert.FromBase64String(Signature);  // 📜 Imzoni dekodlash
        return ecdsa.VerifyHash(hash, signatureBytes);  // ✅ Imzoni tekshirish
    }

    // 📄 Tranzaktsiya ma'lumotlarini olish
    public string GetTransactionData() 
        => $"{FromAddress}-{ToAddress}-{Amount}";  // 🔗 Tranzaktsiya ma'lumotlari
}
