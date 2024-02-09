public class ItemCritChance : Item
{
    private float percentageIncrease;

    public ItemCritChance(string name, float x, float y, float percentage)
        : base(name, x, y, Resources.Cards[3])
    {
        this.percentageIncrease = percentage;
        Value = (int)(100f * percentage);
    }

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
