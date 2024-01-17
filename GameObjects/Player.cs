using System;
using System.Drawing;
using System.Windows.Forms;

// namespace Entity;

public class Player : GameObject, IMoveable
{
    public int Hp { get; set; }
    public float Base_Speed { get; set; } = 5;
    public float Vx { get; set; }
    public float Vy { get; set; }
    public float CritChance { get; set; }
    public float BlockChance { get; set; }

    public Player(string name, int x, int y, string sprite)
        : base(name, x, y, sprite) { }

    public override void Render(Graphics g, PictureBox pb)
    {
        Move();
        g.FillRectangle(
            Brushes.Red,
            new RectangleF
            {
                X = this.X - this.Width / 2,
                Y = this.Y - this.Height / 2,
                Width = this.Width,
                Height = this.Height
            }
        );
        CreateHitbox(this.X, this.Y, this.Width + 1, this.Height + 1);
        g.DrawRectangle(Pens.White, this.Hitbox);

    }

    public void Move()
    {
        X = New_X;
        Y = New_Y;

        double magnitude = Math.Sqrt(Vx * Vx + Vy * Vy);

        if (magnitude == 0)
            return;

        New_X += (float)(Vx / magnitude) * Base_Speed;
        New_Y += (float)(Vy / magnitude) * Base_Speed;
    }

    public void MoveUp() => this.Vy = -1;

    public void MoveDown() => this.Vy = 1;

    public void MoveRight() => this.Vx = 1;

    public void MoveLeft() => this.Vx = -1;

    public void StopY_axis() => this.Vy = 0;

    public void StopX_axis() => this.Vx = 0;
}
