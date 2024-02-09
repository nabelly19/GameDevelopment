public class ItemHp : Item
{
    private int hpIncrease;

    public ItemHp(string name, float x, float y, int hp)
        : base(name, x, y, Resources.Cards[0])
    {
        this.hpIncrease = hp;
        this.Value = hp * 2;
    }

    public override void ApplyBuff()
    {
        var player = GameEngine.Current.Player;
        player.baseHp += hpIncrease;
        player.Hp = player.baseHp;
    }
}
