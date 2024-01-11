public abstract class Hittable : Entity
{
    protected Hittable(string path) : base(path)
    {
    }

    public int X { get; set; }
    public int Y { get; set; }
    public bool Colided { get; set; }

    public abstract bool Colision(Hittable hittable);
}