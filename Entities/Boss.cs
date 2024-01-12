using System;
using System.Drawing;
using System.Windows.Forms;

/* The Boss class is a subclass of the Enemy class and represents a boss enemy with a spawn point and
methods for attacking and handling collisions. */
public class Boss : Enemy
{
    public PointF SpawnPoint;

    public Boss(string path, int width, int height)
        : base(path, width, height) 
        {
            this.Width = this.Image.Width;
            this.Height = this.Image.Height;

            this.X = 500;
            this.Y = 500;
        }

    // Strategy / State / Behaviour
    public override void Attack(Player player)
    {
        throw new NotImplementedException();
    }

    public override void Draw(Graphics g)
    {

        g.DrawImage(this.Image, this.X - this.Width / 2, this.Y - this.Height / 2);
        CreateHitbox(
            this.X, this.Y, 
            this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }
}
