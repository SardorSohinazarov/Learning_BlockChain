namespace BlockChain_005
{
    public class Blockchain
    {
        public List<Block> Chain { get; set; } = new List<Block>();
        public List<Transaction> PendingTransactions { get; set; } = new List<Transaction>();
        public int Difficulty { get; set; } = 4;
        public decimal MiningReward { get; set; } = 50m;

        public Blockchain() 
            => Chain.Add(CreateGenesisBlock());

        private Block CreateGenesisBlock()
        {
            var genesisBlock = new Block
            {
                Index = 0,
                Timestamp = DateTime.UtcNow,
                PreviousHash = "0"
            };
            genesisBlock.Hash = genesisBlock.CalculateHash();
            return genesisBlock;
        }

        public void MinePendingTransactions(string minerAddress)
        {
            var block = new Block
            {
                Index = Chain.Count,
                Timestamp = DateTime.UtcNow,
                Transactions = new List<Transaction>(PendingTransactions),
                PreviousHash = Chain.Last().Hash
            };

            MineBlock(block);
            Chain.Add(block);

            // 🔄 Murakkablikni dinamik boshqarish
            AdjustDifficulty();

            // Joriy blokdagi barcha komissiyalarni hisoblash
            var totalFees = block.Transactions.Sum(tx => tx.Fee);

            // Mukofotni minerga o'tkazish
            PendingTransactions.Clear();
            PendingTransactions.Add(new Transaction
            {
                FromAddress = null,
                ToAddress = minerAddress,
                Amount = MiningReward + totalFees  // Mukofot + komissiyalar
            });
        }

        private void MineBlock(Block block)
        {
            Console.WriteLine("Bloklarni qazib olamiz...");
            while (!block.Hash.StartsWith(new string('0', Difficulty)) || !block.HasValidTransactions())
            {
                block.Nonce++;
                block.Hash = block.CalculateHash();
            }
            Console.WriteLine($"Blok {block.Index} qazib olindi! Xesh: {block.Hash}");
        }

        public void AddTransaction(Transaction transaction)
        {
            if (string.IsNullOrEmpty(transaction.FromAddress) || string.IsNullOrEmpty(transaction.ToAddress))
                throw new Exception("Tranzaktsiya uchun manzillar kerak!");
            if (!transaction.IsValid())
                throw new Exception("Tranzaktsiya imzosi noto'g'ri!");

            PendingTransactions.Add(transaction);
        }

        public decimal GetBalance(string address)
        {
            decimal balance = 0;
            foreach (var block in Chain)
            {
                foreach (var tx in block.Transactions)
                {
                    if (tx.FromAddress == address)
                        balance -= tx.Amount;
                    if (tx.ToAddress == address)
                        balance += tx.Amount;
                }
            }
            return balance;
        }

        public bool IsChainValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = Chain[i];
                var previousBlock = Chain[i - 1];
                if (currentBlock.Hash != currentBlock.CalculateHash() || currentBlock.PreviousHash != previousBlock.Hash || !currentBlock.HasValidTransactions())
                    return false;
            }
            return true;
        }

        public void PrintBlockchain()
        {
            foreach (var block in Chain)
            {
                Console.WriteLine($"Blok: {block.Index}, Xesh: {block.Hash}, Oldingi Xesh: {block.PreviousHash}");
                Console.WriteLine("Tranzaktsiyalar:");
                Console.WriteLine("—————————————————————————————————————");
                foreach (var tx in block.Transactions)
                {
                    Console.WriteLine(tx.GetTransactionData());
                    Console.WriteLine("—————————————————————————————————————");
                }
                Console.WriteLine("——————————————————————————————————————————————————————————————————————————————");
            }
        }

        public List<Transaction> GetTransactionHistory(string address)
        {
            return Chain.SelectMany(b => b.Transactions)
                        .Where(tx => tx.FromAddress == address || tx.ToAddress == address)
                        .ToList();
        }

        public Block GetBlockByHash(string hash)
            => Chain.FirstOrDefault(b => b.Hash == hash);

        public Block GetBlockByIndex(int index)
            => Chain.FirstOrDefault(b => b.Index == index);

        public void AdjustDifficulty()
        {
            if (Chain.Count % 5 == 0)
            {
                Difficulty++;
                Console.WriteLine($"Murakkablik oshirildi: {Difficulty}");
            }
        }
    }
}
