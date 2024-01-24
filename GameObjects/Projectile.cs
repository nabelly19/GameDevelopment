using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Projectile : GameObject, IMoveable
{
    protected SizeF speed;
    public float angle = 0;
    public float BaseAcceleration { get; set; } = 5;
    public float Ax { get; set; }
    public float Ay { get; set; }

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
        // this.X++;
        // this.Y++;
        GoTo(90);
        Location += 5f * this.speed;
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

    protected float ToRadians(float angleD) => MathF.PI / 180 * angleD;

    public virtual void GoTo(float angle)
    {
        var radians = ToRadians(angle);
        this.X += BaseAcceleration * MathF.Cos(radians);
        this.Y += BaseAcceleration * MathF.Sin(radians);
    }
}
