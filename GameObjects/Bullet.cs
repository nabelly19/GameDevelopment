// namespace Entity;

using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

public class Bullet : GameObject
{
    public Bullet(string name, int x, int y, float width, float height)
        : base(name, x, y, width, height)
    {
        DisableHitbox();
    }

    public override void Update()
    {
        Move();
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }

    public void Move()
    {
        // var radius = 10;
        // var center = new PointF(80,80);
        // var point = new PointF(center.X + radius,center.Y);
        // var incrementX = -0.1f;
        // var incrementY = +0.1f;
        // for (int i = 0; i < 100; i++)
        // {
        //     if(point.X > center.X + radius)
        //         incrementX = -0.01f;
        //     else if(point.X < center.X - radius)
        //         incrementX = 0.1f;

        //     if(point.Y > center.Y + radius)
        //         incrementY = -0.1f;
        //     else if(point.Y < center.Y - radius)
        //         incrementY= 0.1f;

        //     point.X += incrementX;
        //     point.Y += incrementY;

        //     this.X = point.X;
        //     this.Y = point.Y;
        // }

        this.X+=5;
        this.Y+=5;

        var collided = CollisionManager.Current.GetCollisions(this).FirstOrDefault();
        if (collided is not null)
        {
            if (collided is Player other)
                other.ReceiveDamage();
            CollisionManager.Current.RemoveGameObject(this);
            return;
        }
        if (CollisionManager.Current.ScreenColision(this))
            CollisionManager.Current.RemoveGameObject(this);
    }
}
