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
        if (player.Ax != 0)
        this.Ax = player.Ax;
        if (player.Ay != 0)
        this.Ay = player.Ay;
        
        this.X = player.Hitbox.X + player.Hitbox.Width / 2 + (player.Hitbox.Width / 2 + this.Width / 2) * this.Ax;
        this.Y = player.Hitbox.Y + player.Hitbox.Height / 2 + (player.Hitbox.Height / 2+ this.Height / 2) * this.Ay;
        
    }
}
