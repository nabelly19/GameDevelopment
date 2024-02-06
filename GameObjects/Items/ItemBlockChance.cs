public class ItemBlockChance : Item
{
    private float percentageIncrease;
    public ItemBlockChance
    (
        string name, 
        float x, 
        float y, 
        float width, 
        float height
    ) : base(name, x, y, width, height) { }

    public ItemBlockChance
    (
        string name, 
        float x, 
        float y, 
        float width, 
        float height,
        float percentage
    ) : base(name, x, y, width, height) 
        => this.percentageIncrease = percentage;

    public override void BuyIt()
    {
        var player = GameEngine.Current.Player;

        if (player.CoinWallet < Value)
            return;

        player.CoinWallet -= Value;
        ItemManager.Bought.Add(this);
        ApplyBuff();
    }

    public override void ApplyBuff()
    {
        var player = GameEngine.Current.Player;
        var futureBlockChance = player.BlockChance + this.percentageIncrease;
        if (futureBlockChance >= 0.70)
            return;

        player.BlockChance = futureBlockChance;
    }
}