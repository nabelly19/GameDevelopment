using System;
using System.Windows.Forms;
using System.Drawing;


public class Boss : Enemy
{
    public PointF SpawnPoint;
    public Boss(string path) : base(path) { }

    // Strategy / State / Behaviour
    public override void Attack(Player player)
    {
        throw new NotImplementedException();
    }

    public override bool Colision(Hittable hittable)
    {
        throw new NotImplementedException();
    }
    public override void Draw(Graphics g)
    {
        this.SpawnPoint.X = 500;
        this.SpawnPoint.Y = 500;
        
        g.DrawImage(this.Image,
            SpawnPoint);
    }
}
