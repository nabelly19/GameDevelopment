using System;
using System.Drawing;
using System.Windows.Forms;

// namespace Entity;

public class Weapon : GameObject, IMoveable
{
    private Player player { get; set; }
    public int AtkSpeed { get; set; }
    public bool WindBlade { get; set; } = false;
    public float BaseAcceleration { get; set; }
    public float Ax { get; set; }
    public float Ay { get; set; }

    public Weapon(string name, int x, int y, float width, float height, Player player)
        : base(name, x, y, height, width)
    {
        this.player = player;
        DisableHitbox();
    }

    public void Attack() { }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }

    public override void Update()
    {
        Move();
    }

    public void Move()
    {
        this.X = player.X + (player.Width / 2 + this.Width / 2) * player.Ax;
        this.Y = player.Y + (player.Height / 2 + this.Height / 2) * player.Ay;
    }
}
