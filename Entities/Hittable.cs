public abstract class Hittable : Entity
{
    protected Hittable(string path) : base(path) { }

    public float X { get; set; }
    public float Y { get; set; }
    public bool Colided { get; set; }

    public abstract bool Colision(Hittable hittable);
}