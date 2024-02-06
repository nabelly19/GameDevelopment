public class ItemWindBlade : Item
{
    public ItemWindBlade(string name, float x, float y, float width, float height)
        : base(name, x, y, width, height) { }

    public override void ApplyBuff()
    {
        GameEngine.Current.Player.Weapon.WindBlade = true;
    }
}
