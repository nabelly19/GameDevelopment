using System;
using System.Drawing;
using System.Windows.Forms;

public class WallMoveable : RotateProjectile
{
    public WallMoveable(
        string name,
        float x,
        float y,
        string sprite,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner) { }

    public WallMoveable(
        string name,
        float x,
        float y,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, 400, 400, direction, owner)
    {
        this.center = new(x, y);
        this.radius = 300;
        EnableHitbox();
        RotatePoints();
    }

    public override void Render(Graphics g, PictureBox pb) =>
        g.DrawRectangle(Pens.White, this.Hitbox);

    public override void Move()
    {
        // RotatePoints();
    }

    public override void Update()
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        Move();
    }

    public override void RotatePoints()
    {
        float radians = ToRadians(Angle);
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);

        this.X = this.center.X + this.radius * cos;
        this.Y = this.center.Y + this.radius * sin;
        GoTo(Direction);
        this.Angle += 1f;

        return;
    }
}
