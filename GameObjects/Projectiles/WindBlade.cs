public class WindBlade : Projectile

{
    public WindBlade(string name, int x, int y, string sprite, float direction, IAttackable owner) : base(name, x, y, sprite, direction, owner)
    {
    }

    public WindBlade(string name, float x, float y, float width, float height, float direction, IAttackable owner) : base(name, x, y, width, height, direction, owner)
    {
    }
} 