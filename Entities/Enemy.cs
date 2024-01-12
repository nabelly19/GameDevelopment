/* The Enemy class is an abstract class that inherits from the Hittable class and defines properties
and methods for enemy objects. */
public abstract class Enemy : Hittable
{
    protected Enemy(string path, int width, int height) : base(path, width, height)
    {
    }

    public int HP { get; set; }

    public virtual void ReceiveDamage() => this.HP--;

    public abstract void Attack(Player player);
}
