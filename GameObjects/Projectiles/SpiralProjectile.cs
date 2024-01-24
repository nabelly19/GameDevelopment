using System;
using System.Drawing;

public class SpiralProjectile : Projectile
{
    public PointF center = new(100, 100);
    public float angle = 0;
    public float radius = 35;

    public SpiralProjectile(string name, int x, int y, string sprite)
        : base(name, x, y, sprite) { }

    public SpiralProjectile(string name, float x, float y, float width, float height)
        : base(name, x, y, width, height) { }

    public override void Move()
    {
        RotatePoints();
    }

    public void RotatePoints()
    {
        float radians = MathF.PI / 180 * angle;
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);

        this.X = this.center.X + this.radius * cos;
        this.Y = this.center.Y + this.radius * sin;

        this.center.X += 1.5f;
        this.center.Y += 1.5f;
        this.angle += 10f;

        return;
    }
}
