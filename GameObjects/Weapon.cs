using System;
using System.Drawing;
using System.Windows.Forms;

// namespace Entity;

public class Weapon : GameObject, IMoveable
{
    private Player player { get; set; }
    public int AtkSpeed { get; set; } = 800;
    public bool WindBlade { get; set; } = false;
    public float BaseAcceleration { get; set; }
    public float Ax { get; set; }
    public float Ay { get; set; }
    public float Vx { get; set; }
    public float Vy { get; set; }
    public bool isMoving { get; set; }
    public bool isAttaking { get; set; }

    public Weapon(string name, int x, int y, float width, float height, Player player)
        : base(name, x, y, height, width)
    {
        this.player = player;
        this.Sprite = Resources.Weapon[0];
        DisableHitbox();
    }

    public Weapon(string name, int x, int y, string sprite, Player player)
        : base(name, x, y, sprite)
    {
        this.player = player;
        DisableHitbox();
    }

    public override void Update() => CreateHitbox(this.X, this.Y, this.Width, this.Height);

    public override void Render(Graphics g, PictureBox pb) =>
        g.DrawImage(
            this.Sprite,
            this.X - this.Hitbox.Width / 2 ,
            this.Y - this.Hitbox.Height / 2,
            this.Hitbox.Width,
            this.Hitbox.Height
        );

    public void Move()
    {
        if (player.Ax != 0)
        {
            this.Ax = player.Ax;
            this.Ay = 0;
        }
        else if (player.Ay != 0)
        {
            this.Ax = 0;
            this.Ay = player.Ay;
        }

        if (Ax > 0)
            this.Sprite = Resources.Weapon[0];
        else if (Ax < 0)
            this.Sprite = Resources.Weapon[3];

        if (Ay > 0)
            this.Sprite = Resources.Weapon[2];
        else if (Ay < 0)
            this.Sprite = Resources.Weapon[1];

        this.X =
            player.Hitbox.X
            + player.Hitbox.Width / 2
            + (player.Hitbox.Width / 2 + this.Width / 2) * this.Ax;
        this.Y =
            player.Hitbox.Y
            + player.Hitbox.Height / 2
            + (player.Hitbox.Height / 2 + this.Height / 2) * this.Ay;
    }
}
