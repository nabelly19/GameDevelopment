public class ItemSpeed : Item
{
    private float percentageIncrease;
    public ItemSpeed(string name, float x, float y, float percentage)
        : base(name, x, y, Resources.Cards[1])
    {
        this.percentageIncrease = percentage;
        this.Value = (int)(percentage*100);
    }

    public override void ApplyBuff()
    {
        var player = GameEngine.Current.Player;
        player.BaseAcceleration *= 1 + this.percentageIncrease;
    }
}