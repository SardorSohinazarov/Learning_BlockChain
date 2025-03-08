// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;

public class Wallet
{
    public string PrivateKey { get; private set; }
    public string PublicKey { get; private set; }

    public Wallet()
    {
        using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);

        // 🔑 Shaxsiy kalitni yaratish
        PrivateKey = ExportPrivateKey(ecdsa);

        // 🌐 Umumiy kalitni yaratish
        PublicKey = ExportPublicKey(ecdsa);
    }

    // 🔑 Private kalitni PEM formatida saqlash
    private string ExportPrivateKey(ECDsa ecdsa)
    {
        var privateKey = ecdsa.ExportECPrivateKey();
        var sb = new StringBuilder();
        sb.AppendLine("-----BEGIN PRIVATE KEY-----");
        sb.AppendLine(Convert.ToBase64String(privateKey, Base64FormattingOptions.InsertLineBreaks));
        sb.AppendLine("-----END PRIVATE KEY-----");
        return sb.ToString();
    }

    // 🌐 Public kalitni PEM formatida saqlash
    private string ExportPublicKey(ECDsa ecdsa)
    {
        var publicKey = ecdsa.ExportSubjectPublicKeyInfo();
        var sb = new StringBuilder();
        sb.AppendLine("-----BEGIN PUBLIC KEY-----");
        sb.AppendLine(Convert.ToBase64String(publicKey, Base64FormattingOptions.InsertLineBreaks));
        sb.AppendLine("-----END PUBLIC KEY-----");
        return sb.ToString();
    }
}
