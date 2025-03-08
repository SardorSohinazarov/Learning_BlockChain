// See https://aka.ms/new-console-template for more information
public class Blockchain
{
    public List<Block> Chain { get; } = new List<Block>();
    public List<Transaction> CurrentTransactions { get; } = new List<Transaction>();  // 🔄 Yangi: Kutayotgan tranzaktsiyalar
    public int Difficulty { get; } = 4;
    public decimal Reward { get; } = 50m;  // 🔄 Mukofot: 50 BTC

    public Blockchain()
    {
        Chain.Add(CreateGenesisBlock());
    }

    private Block CreateGenesisBlock()
    {
        var genesisBlock = new Block(0, "0", new List<Transaction>());
        genesisBlock.MineBlock(Difficulty);
        return genesisBlock;
    }

    public Block GetLatestBlock()
    {
        return Chain[Chain.Count - 1];
    }

    public void CreateTransaction(Transaction transaction)
    {
        CurrentTransactions.Add(transaction);  // 🔄 Tranzaktsiyani qo‘shish
    }

    public void MinePendingTransactions(string minerAddress)
    {
        // Mukofot tranzaktsiyasini yaratamiz
        CreateTransaction(new Transaction(null, minerAddress, Reward));

        // 🔄 Yangi blokni yaratamiz, mukofot tranzaktsiyasi bilan
        Block block = new Block(GetLatestBlock().Index + 1, GetLatestBlock().Hash, new List<Transaction>(CurrentTransactions));
        block.MineBlock(Difficulty);

        Console.WriteLine("Blok muvaffaqiyatli qo‘shildi!");
        Chain.Add(block);

        // 🔄 Tranzaktsiyalarni tozalash (mukofotdan keyin!)
        CurrentTransactions.Clear();
    }


    public decimal GetBalance(string address)
    {
        decimal balance = 0;
        foreach (var block in Chain)
        {
            foreach (var transaction in block.Transactions)
            {
                if (transaction.FromAddress == address)
                    balance -= transaction.Amount;
                if (transaction.ToAddress == address)
                    balance += transaction.Amount;
            }
        }

        return balance;
    }

    public bool IsValid()
    {
        for (int i = 1; i < Chain.Count; i++)
        {
            Block current = Chain[i];
            Block previous = Chain[i - 1];

            if (current.Hash != current.CalculateHash())
            {
                Console.WriteLine($"Xato: Blok {current.Index} xeshi noto‘g‘ri!");
                return false;
            }

            if (current.PreviousHash != previous.Hash)
            {
                Console.WriteLine($"Xato: Blok {current.Index} oldingi xeshi noto‘g‘ri!");
                return false;
            }
        }
        return true;
    }

}
