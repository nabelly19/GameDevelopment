using System.Drawing;

public abstract class Hittable : Entity
{
    protected Hittable(string path, int width, int height)
        : base(path) {
            CreateHitbox(
                this.X, this.Y,
                width, height);
            this.Width = width;
            this.Height = height;
         }

    public float X { get; set; }
    public float Y { get; set; }
    public float Old_X { get; set; }
    public float Old_Y { get; set; }
    public RectangleF Hitbox { get; set; }
    public bool HittableHitbox { get; private set; }
    public bool Colided { get; set; }

    public virtual void CreateHitbox(
        float x, float y, 
        int width, int height)
    {
        this.Hitbox = new RectangleF(
                x - (width / 2), y - (height / 2),
                width,  height
            );
    }

    public virtual void EnableHitbox() => this.HittableHitbox = true;

    public virtual void DisableHitbox() => this.HittableHitbox = false;
}
