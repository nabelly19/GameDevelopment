using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class WallMoveable : RotateProjectile
{
    public float AngularSpeed { get; set; } = 0.5f;

    public WallMoveable(
        string name,
        float x,
        float y,
        string sprite,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner) { }

    public WallMoveable(string name, float x, float y, float direction, IAttackable owner)
        : base(name, x, y, 200, 200, direction, owner)
    {
        this.Angle = direction;
        this.center = new(x, y);
        this.radius = 300;
        EnableHitbox();
    }

    public WallMoveable(
        string name,
        float x,
        float y,
        float width,
        float height,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, width, height, direction, owner)
    {
        this.Angle = direction;
        this.center = new(x, y);
        this.radius = 300;
        EnableHitbox();
    }

    public WallMoveable(
        string name,
        float x,
        float y,
        Image sprite,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner)
    {
        this.Angle = direction;
        this.center = new(x, y);
        this.radius = 300;
        EnableHitbox();
    }

    public override void Render(Graphics g, PictureBox pb) =>
        g.DrawImage(this.Sprite, this.X - this.Width / 2, this.Y - this.Height / 2);

    public override void Move()
    {
        RotatePoints();
        var collisions = CollisionManager.GetCollisions(this);
        var player = collisions.FirstOrDefault(obj => obj is Player) as Player;
        if (player is null)
            return;

        var data = CollisionManager.CheckCollisionsData(player);

        var rA = Random.Shared.Next(10_000);
        var rB = Random.Shared.Next(10_000) - 5_000;

        if ((data & CollisionType.Bottom) > 0)
            player.ApplyForce(rB, 30_000 + rA);

        if ((data & CollisionType.Top) > 0)
            player.ApplyForce(rB, -30_000 - rA);

        if ((data & CollisionType.Left) > 0)
            player.ApplyForce(30_000 + rA, rB);

        if ((data & CollisionType.Right) > 0)
            player.ApplyForce(-30_000 - rA, rB);

        float radians = ToRadians(Angle);
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);
        player.ApplyForce(-5_000 * sin, 5_000 * cos);
    }

    public override void Update()
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        Move();
    }

    public override void RotatePoints()
    {
        float radians = ToRadians(Angle);
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);

        this.X = this.center.X + this.radius * cos;
        this.Y = this.center.Y + this.radius * sin;
        // GoTo(Direction);
        this.Angle += this.AngularSpeed;
    }
}
