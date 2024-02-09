using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class TrackingProjectile : Projectile
{
    private int Steps { get; set; } = 0;
    private int SlowFrameRate { get; set; } = 0;
    private GameObject player;
    private float angle;
    private bool track = true;
    private DateTime? dt = null;

    public TrackingProjectile(
        string name,
        int x,
        int y,
        string sprite,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner)
    {
        this.BaseAcceleration = 3;
    }

    public TrackingProjectile(
        string name,
        int x,
        int y,
        Image sprite,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner)
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
    )
        : base(name, x, y, width, height, direction, owner)
    {
        this.player = player;
        this.BaseAcceleration = 3;
    }

    public TrackingProjectile(
        string name,
        int x,
        int y,
        string sprite,
        GameObject player,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner)
    {
        this.BaseAcceleration = 3;
        this.player = player;
    }

    public TrackingProjectile(
        string name,
        int x,
        int y,
        Image sprite,
        GameObject player,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner)
    {
        this.BaseAcceleration = 3;
        this.player = player;
    }

    public TrackingProjectile(
        string name,
        int x,
        int y,
        string sprite,
        GameObject player,
        List<GameObject> owners,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner)
    {
        this.BaseAcceleration = 3;
        this.player = player;
        this.Owners = owners;
    }

    public TrackingProjectile(
        string name,
        int x,
        int y,
        Image sprite,
        GameObject player,
        List<GameObject> owners,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner)
    {
        this.BaseAcceleration = 3;
        this.player = player;
        this.Owners = owners;
    }

    public TrackingProjectile(
        string name,
        float x,
        float y,
        float width,
        float height,
        List<GameObject> owners,
        GameObject player,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, width, height, direction, owner)
    {
        this.player = player;
        this.BaseAcceleration = 3;
        this.Owners = owners;
    }

    public TrackingProjectile(
        string name,
        float x,
        float y,
        Image sprite,
        List<GameObject> owners,
        GameObject player,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner)
    {
        this.player = player;
        this.BaseAcceleration = 3;
        this.Owners = owners;
    }

    public override void Update()
    {
        AnimateProjectile(0,1);
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
                    var chance = Random.Shared.NextDouble();
                    // MessageBox.Show(chance.ToString());
                    if (chance < other.BlockChance)
                    {
                        CollisionManager.RemoveGameObject(this);
                        return;
                    }
                    other.ReceiveDamage();
                }
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
        g.DrawImage(this.Sprite, this.X - this.Width / 2, this.Y - this.Height / 2);
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

     private void AnimateProjectile(int start, int end)
    {
        SlowFrameRate += 1;

        if (SlowFrameRate > 3)
        {
            Steps++;
            SlowFrameRate = 0;
        }

        if (Steps > end || Steps < start)
            Steps = start;

        this.Sprite = Resources.Tracking[Steps];
    }

}
