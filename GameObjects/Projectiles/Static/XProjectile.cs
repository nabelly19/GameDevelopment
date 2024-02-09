using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class XProjectile : Projectile
{
    private DateTime? dt = null;
    public XProjectile
    (
        string name, 
        float x, float y, 
        string sprite, 
        float direction, 
        IAttackable owner
    ) : base(name, x, y, sprite, direction, owner)
    {
    }

    public XProjectile
    (
        string name, 
        float x, float y, 
        float width, float height, 
        float direction, 
        IAttackable owner
    ) : base(name, x, y, width, height, direction, owner)
    {
    }

    public XProjectile
    (
        string name, 
        float x, float y, 
        Image sprite, 
        float direction, 
        IAttackable owner) 
        : base(name, x, y, sprite, direction, owner)
    {
    }

    public override void Update()
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        VerifyLifespan();
        Move();
        var collided = CollisionManager.GetCollisions(this).FirstOrDefault();
        if (collided is not null)
        {
            if (collided == Owner)
                return;
            if (this.Owners.Contains(collided))
                return;

            if (collided is IAttackable other)
            {
                if (other.isVulnerable)
                {
                    other.ReceiveDamage();
                }
            }

            CollisionManager.RemoveGameObject(this);
        }
        if (CollisionManager.ScreenColision(this))
            CollisionManager.RemoveGameObject(this);

    }
    public override void Move()
    { }
    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, 30, 30);
       if (this.Sprite is not null)
            g.DrawImage(this.Sprite, this.X - this.Width / 2, this.Y - this.Height / 2);
      else
            g.DrawRectangle(Pens.White, this.Hitbox);
    }

    private void VerifyLifespan()
    {
        dt ??= DateTime.Now;

        if (DateTime.Now > dt?.AddSeconds(3))
        {
            CollisionManager.RemoveGameObject(this);
            dt = null;
        }
    }
}