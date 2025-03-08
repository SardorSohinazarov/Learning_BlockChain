public class Transaction
{
    public string FromAddress { get; }  // 🔄 Kimdan
    public string ToAddress { get; }    // 🔄 Kimga
    public decimal Amount { get; }      // 🔄 Miqdor

    public Transaction(string fromAddress, string toAddress, decimal amount)
    {
        FromAddress = fromAddress;
        ToAddress = toAddress;
        Amount = amount;
    }
}
