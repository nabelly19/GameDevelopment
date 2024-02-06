public class ItemSpeed : Item
{
    private float percentageIncrease;
    public ItemSpeed
    (
        string name, 
        float x, 
        float y, 
        float width, 
        float height
    ) : base(name, x, y, width, height) { }

    public ItemSpeed
    (
        string name, 
        float x, 
        float y, 
        float width, 
        float height,
        float percentage
    ) : base(name, x, y, width, height) 
        => this.percentageIncrease = percentage;

    public override void ApplyBuff()
    {
        var player = GameEngine.Current.Player;
        player.BaseAcceleration *= 1 + this.percentageIncrease;
    }
}