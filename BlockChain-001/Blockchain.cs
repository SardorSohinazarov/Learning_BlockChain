// See https://aka.ms/new-console-template for more information
public class Blockchain
{
    public List<Block> Chain { get; } = new List<Block>();

    // Boshlang'ich blokni yaratamiz (Genesis Block)
    public Blockchain()
    {
        Chain.Add(CreateGenesisBlock());
    }

    private Block CreateGenesisBlock()
    {
        return new Block(0, "0", "Genesis Block");
    }

    // Oxirgi blokni olish
    public Block GetLatestBlock()
    {
        return Chain[Chain.Count - 1];
    }

    // Yangi blok qo'shish
    public void AddBlock(string data)
    {
        Block latestBlock = GetLatestBlock();
        Block newBlock = new Block(latestBlock.Index + 1, latestBlock.Hash, data);
        Chain.Add(newBlock);
    }

    // Blockchain haqiqiyligini tekshirish
    public bool IsValid()
    {
        for (int i = 1; i < Chain.Count; i++)
        {
            Block current = Chain[i];
            Block previous = Chain[i - 1];

            // Xeshlar to'g'ri ekanini tekshiramiz
            if (current.Hash != current.CalculateHash())
                return false;

            // Oldingi xesh mos kelishini tekshiramiz
            if (current.PreviousHash != previous.Hash)
                return false;
        }
        return true;
    }
}
