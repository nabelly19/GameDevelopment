using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Projectile : GameObject, IMoveable
{
    protected SizeF speed;
    public float Angle = Random.Shared.Next(360);
    public float Direction { get; set; }
    public virtual float BaseAcceleration { get; set; } = 5;
    public float Ax { get; set; }
    public float Ay { get; set; }
    public IAttackable Owner { get; set; } = null;

    public Projectile(string name, float x, float y, string sprite, float direction, IAttackable owner)
        : base(name, x, y, sprite)
    {
        this.Direction = direction;
        this.Owner = owner;
        DisableHitbox();
    }

    public Projectile(
        string name,
        float x,
        float y,
        float width,
        float height,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, width, height)
    {
        DisableHitbox();
        this.Owner = owner;
        this.Direction = direction;
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }

    public virtual void Move()
    {
        GoTo(Direction);
        Location += 5f * this.speed;
    }

    public override void Update()
    {
        Move();
        var collided = CollisionManager.Current.GetCollisions(this).FirstOrDefault();
        if (collided is not null)
        {
            if (collided == Owner)
                return;
            if (collided is IAttackable other)
            {
               if ( other.isVulnerable) 
                    other.ReceiveDamage(); 
                else
                    return;
            
            }
            CollisionManager.Current.RemoveGameObject(this);
        }
        if (CollisionManager.Current.ScreenColision(this))
            CollisionManager.Current.RemoveGameObject(this);
    }

    protected float ToRadians(float angleD) => MathF.PI / 180 * angleD;
    protected float ToDegree(float angleR) => 180 / MathF.PI * angleR;
    public virtual void GoTo(float angle)
    {
        var radians = ToRadians(angle);
        this.X += BaseAcceleration * MathF.Cos(radians);
        this.Y += BaseAcceleration * MathF.Sin(radians);
    }
}
