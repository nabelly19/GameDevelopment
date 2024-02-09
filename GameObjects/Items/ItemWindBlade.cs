public class ItemWindBlade : Item
{
    public ItemWindBlade(string name, float x, float y)
        : base(name, x, y, Resources.Cards[3]) => this.Value = 4;

    public override void ApplyBuff()
    {
        GameEngine.Current.Player.Weapon.WindBlade = true;
    }
}
