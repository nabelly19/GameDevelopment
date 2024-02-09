public class ItemHp : Item
{
    private int hpIncrease;
    public ItemHp
    (
        string name, 
        float x, 
        float y, 
        float width, 
        float height
    ) : base(name, x, y, width, height) { }

    public ItemHp
    (
        string name, 
        float x, 
        float y, 
        float width, 
        float height,
        int hp
    ) : base(name, x, y, width, height) 
        => this.hpIncrease = hp;

    public ItemHp
    (
        string name,
        float x, 
        float y,
        int hp
    ) : base(name, x, y, "assets/Sprites/Card/c0.png")
        => this.hpIncrease = hp;

    

    public override void ApplyBuff()
    {
        var player = GameEngine.Current.Player;
        player.baseHp += hpIncrease;
        player.Hp = player.baseHp;
    }
}