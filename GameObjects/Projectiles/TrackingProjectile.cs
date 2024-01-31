using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
public class TrackingProjectile : Projectile
{
    private GameObject player;
    private float angle;
    private bool track = true;
    private DateTime? dt = null;
    public TrackingProjectile
    (
        string name,
        int x,
        int y,
        string sprite,
        float direction,
        IAttackable owner
    ) : base(name, x, y, sprite, direction, owner)
    {
        this.BaseAcceleration = 3;
    }
    public TrackingProjectile(
        string name,
        float x,
        float y,
        float width,
        float height,
        GameObject player,
        float direction,
        IAttackable owner
    ) : base(name, x, y, width, height, direction, owner)
    {
        this.player = player;
        this.BaseAcceleration = 3;

    }

    public TrackingProjectile
    (
        string name,
        int x,
        int y,
        string sprite,
        GameObject player,
        float direction,
        IAttackable owner
    ) : base(name, x, y, sprite, direction, owner)
    {
        this.BaseAcceleration = 3;
        this.player = player;
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

            if (collided is IAttackable other)
            {
                if (other.isVulnerable)
                    other.ReceiveDamage();

            }

            CollisionManager.RemoveGameObject(this);
        }
        if (CollisionManager.ScreenColision(this))
            CollisionManager.RemoveGameObject(this);
    }

    private void VerifyLifespan()
    {
        dt ??= DateTime.Now;

        if (DateTime.Now > dt?.AddSeconds(8))
        {
            CollisionManager.RemoveGameObject(this);
            dt = null;
        }

    }

    public override void Move()
    {
        Track();
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        base.Render(g, pb);
    }

    public void Track()
    {
        GoTo(this.angle);
        var dist = getDist();
        if (dist < 100)
        {
            track = false;
            return;
        }
        GetPlayerAngle();
    }

    private void GetPlayerAngle()
    {   
        if (!track)
            return;

        var dx = this.player.X - this.X;
        var dy = this.player.Y - this.Y;
        this.angle = MathF.Atan2(dy, dx);

        this.angle = ToDegree(this.angle);
    }

    private float getDist()
    {
        var dx = this.player.X - this.X;
        var dy = this.player.Y - this.Y;

        var dist = MathF.Sqrt(dx * dx + dy * dy);

        return dist;

    }
}