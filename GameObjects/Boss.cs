using System;
using System.Drawing;
using System.Windows.Forms;


// namespace Entity;
/* The Boss class is a subclass of the Enemy class and represents a boss enemy with a spawn point and
methods for attacking and handling collisions. */
public class Boss : GameObject
{
    public PointF SpawnPoint;

    public Boss(string name, int x, int y, string sprite)
        : base(name, x, y, sprite) { }

    // Strategy / State / Behaviour
    public void Attack(Player player)
    {
        throw new NotImplementedException();
    }

    public override void Render(Graphics g,PictureBox pb)
    {

        g.DrawImage(this.Sprite, this.X - this.Width / 2, this.Y - this.Height / 2);
        CreateHitbox(
            this.X, this.Y,
            this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }
}
