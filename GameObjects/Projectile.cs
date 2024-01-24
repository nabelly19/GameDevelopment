using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Projectile : GameObject
{
    public Projectile(string name, int x, int y, string sprite)
        : base(name, x, y, sprite)
    {
        DisableHitbox();
    }

    public Projectile(string name, float x, float y, float width, float height)
        : base(name, x, y, width, height)
    {
        DisableHitbox();
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }

    public virtual void Move()
    {
        this.X++;
        this.Y++;
    }

    public override void Update()
    {
        Move();
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
