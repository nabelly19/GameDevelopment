using System;
using System.Drawing;
using System.Windows.Forms;

public class SpiralProjectile : Projectile
{
    public PointF center = new(960, 540);
    public float radius = 35;

    public SpiralProjectile(string name, int x, int y, string sprite)
        : base(name, x, y, sprite) { }

    public SpiralProjectile(string name, float x, float y, float width, float height)
        : base(name, x, y, width, height) { }

    public override void Move()
    {
        RotatePoints();
    }
    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
        g.DrawRectangle(Pens.Red, new RectangleF(this.center.X,this.center.Y, 10, 10));
        
    }

    public void RotatePoints()
    {
        float radians = ToRadians(angle);
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);

        this.X = this.center.X + this.radius * cos;
        this.Y = this.center.Y + this.radius * sin;

        GoTo(0);
        this.angle += 10f;

        return;
    }

    public override void GoTo(float angle)
    {
        var radians = ToRadians(angle);
        this.center.X += BaseAcceleration * MathF.Cos(radians);
        this.center.Y += BaseAcceleration * MathF.Sin(radians);
    }
}
