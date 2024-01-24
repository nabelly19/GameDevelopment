using System;
using System.Drawing;
using System.Windows.Forms;

// namespace Entity;
/* The Boss class is a subclass of the Enemy class and represents a boss enemy with a spawn point and
methods for attacking and handling collisions. */
public class Boss : GameObject, IAttackable
{
    public int Hp { get; set; } = 3;
    public StateManager Manager { get; private set; } = new();

    public Boss(string name, int x, int y, string sprite)
        : base(name, x, y, sprite) { }

    public override void Update()
    {
        this.Manager.Act();

        var collided = CollisionManager.Current.GetCollisions(this);
        foreach (var other in collided)
        {
            if (other is IAttackable player)
                player.ReceiveDamage();
        }
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        g.DrawString($"HP Boss: {this.Hp}", SystemFonts.DefaultFont, Brushes.White, 10, 20);
        g.DrawImage(this.Sprite, this.X - this.Width / 2, this.Y - this.Height / 2);
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }

    public void ReceiveDamage()
    {
        this.Hp--;
    }
}
