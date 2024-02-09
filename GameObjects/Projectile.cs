using System;
using System.Collections.Generic;
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
    public List<GameObject> Owners { get; set; } = new();
    public float Vx { get; set; }
    public float Vy { get; set; }
    public bool isMoving { get; set; }

    public Projectile(
        string name,
        float x,
        float y,
        string sprite,
        float direction,
        IAttackable owner
    )
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
        Image sprite,
        float direction,
        IAttackable owner
    )
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
        this.Owner = owner;
        this.Direction = direction;
        DisableHitbox();
    }

    public override void Render(Graphics g, PictureBox pb) =>
        g.DrawRectangle(Pens.White, this.Hitbox);

    public virtual void Move()
    {
        GoTo(Direction);
        Location += 5f * this.speed;
    }

    public override void Update()
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
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
                    var chance = Random.Shared.NextDouble();
                    if (chance < other.BlockChance)
                    {
                        CollisionManager.RemoveGameObject(this);
                        return;
                    }
                    other.ReceiveDamage();
                }
                else
                    return;
            }
            CollisionManager.RemoveGameObject(this);
        }
        if (CollisionManager.ScreenColision(this))
            CollisionManager.RemoveGameObject(this);
    }

    protected float ToRadians(float angleD) => MathF.PI / 180 * angleD;

    protected float ToDegree(float angleR) => 180 / MathF.PI * angleR;

    public virtual void GoTo(float angle)
    {
        var radians = ToRadians(angle);
        this.X += BaseAcceleration * MathF.Cos(radians);
        this.Y += BaseAcceleration * MathF.Sin(radians);
    }

    public virtual void AddOwners(params GameObject[] objs)
    {
        foreach (var item in objs)
            this.Owners.Add(item);
    }
}
