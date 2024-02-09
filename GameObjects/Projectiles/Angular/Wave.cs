using System;
using System.Drawing;
using System.Windows.Forms;

public class Wave : Projectile
{
    public override float BaseAcceleration { get; set; } = 3;

    public Wave(string name, float x, float y, Image sprite, float direction, IAttackable owner)
        : base(name, x, y, sprite, direction, owner) { }

    public Wave(
        string name,
        float x,
        float y,
        float width,
        float height,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, width, height, direction, owner) { }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        if (this.Sprite is not null)
            g.DrawImage(this.Sprite, this.X - this.Width / 2, this.Y - this.Height / 2);
        else
            g.DrawRectangle(Pens.White, this.Hitbox);

    }
}
