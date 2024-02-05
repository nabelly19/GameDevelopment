public class ItemWindBlade : Item
{
    public ItemWindBlade(string name, float x, float y, float width, float height)
        : base(name, x, y, width, height) { }

    public override bool BuyIt()
    {
        if (base.BuyIt())
        {
            ItemManager.AllItens.Remove(this);
            return GameEngine.Current.Player.Weapon.WindBlade = true;
        }
        return false;
    }
}
