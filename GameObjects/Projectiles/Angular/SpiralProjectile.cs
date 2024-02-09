using System;
using System.Drawing;
using System.Windows.Forms;

public class SpiralProjectile : RotateProjectile
{
    public SpiralProjectile(
        string name,
        float x,
        float y,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, Resources.Spiral, direction, owner)
    {
        this.center = new(x, y);
        this.radius = 35;
    }

    public SpiralProjectile(
        string name,
        float x,
        float y,
        float width,
        float height,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, width, height, direction, owner)
    {
        this.center = new(x, y);
        this.radius = 35;
    }

    public override void Move()
    {
        RotatePoints();
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawImage(this.Sprite, this.X - this.Width / 2, this.Y - this.Height / 2);
    }

    public override void RotatePoints()
    {
        float radians = ToRadians(Angle);
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);

        this.X = this.center.X + this.radius * cos;
        this.Y = this.center.Y + this.radius * sin;
        GoTo(Direction);
        this.Angle += 10f;

        return;
    }

    public override void GoTo(float angle)
    {
        var radians = ToRadians(angle);
        this.center.X += BaseAcceleration * MathF.Cos(radians);
        this.center.Y += BaseAcceleration * MathF.Sin(radians);
    }
}
