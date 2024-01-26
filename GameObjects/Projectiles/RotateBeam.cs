using System;
using System.Drawing;
using System.Windows.Forms;

public class RotateBeam : Projectile
{
    public PointF center = new(960, 540);
    public float radius = 0;

    public RotateBeam(string name, int x, int y, string sprite, float direction, IAttackable owner) : base(name, x, y, sprite, direction, owner)
    {
    }

    public RotateBeam(string name, float x, float y, float width, float height, float direction, IAttackable owner) : base(name, x, y, width, height, direction, owner)
    {
    }

     public override void Move()
    {
       
    }
    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
        g.DrawRectangle(Pens.Red, new RectangleF(this.center.X,this.center.Y, 10, 10));
        
    }

    public void RotatePoints()
    {
        float radians = ToRadians(Angle);
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);

        return;
    }

    public override void GoTo(float angle)
    {
        var radians = ToRadians(angle);
        this.center.X += BaseAcceleration * MathF.Cos(radians);
        this.center.Y += BaseAcceleration * MathF.Sin(radians);
    }
}
