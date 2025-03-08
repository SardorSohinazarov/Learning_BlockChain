public class Blockchain
{
    public List<Block> Chain { get; } = new List<Block>();
    public int Difficulty { get; } = 4;          // 🔄 Qiyinchilik darajasi: 4 ta 0 bilan boshlansin
    public int Reward { get; } = 50;             // 🔄 Mukofot: 50 BTC

    public Blockchain()
    {
        Chain.Add(CreateGenesisBlock());
    }

    private Block CreateGenesisBlock()
    {
        return new Block(0, "0", "Genesis Block");
    }

    public Block GetLatestBlock()
    {
        return Chain[Chain.Count - 1];
    }

    public void AddBlock(string data)
    {
        Block latestBlock = GetLatestBlock();
        Block newBlock = new Block(latestBlock.Index + 1, latestBlock.Hash, data);
        newBlock.MineBlock(Difficulty);  // 🔄 Qazib olish jarayoni
        Chain.Add(newBlock);
    }

    public bool IsValid()
    {
        for (int i = 1; i < Chain.Count; i++)
        {
            Block current = Chain[i];
            Block previous = Chain[i - 1];

            if (current.Hash != current.CalculateHash() || current.PreviousHash != previous.Hash)
                return false;
        }
        return true;
    }
}
