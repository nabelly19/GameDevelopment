public class ItemCritChance : Item
{
    private float percentageIncrease;
    public ItemCritChance
    (
        string name, 
        float x, 
        float y, 
        float width, 
        float height
    ) : base(name, x, y, width, height) { }

    public ItemCritChance
    (
        string name, 
        float x, 
        float y, 
        float width, 
        float height,
        float percentage
    ) : base(name, x, y, width, height) 
        => this.percentageIncrease = percentage;

    public ItemCritChance
    (
        string name,
        float x, 
        float y,
        float percentage
    ) : base(name, x, y, "../../../assets/Sprites/Card/c11.png")
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
        var futureCritChance = player.CritChance + this.percentageIncrease;
        if (futureCritChance >= 0.70)
            return;

        player.CritChance = futureCritChance;
    }
}