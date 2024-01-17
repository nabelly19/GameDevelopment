using System;
using System.Drawing;
using System.Windows.Forms;

// namespace Entity;

public class Player : GameObject, IMoveable
{
    private float vx = 0f;
    private float vy = 0f;
    private DateTime last = DateTime.Now;

    public int Hp { get; set; }
    public float BaseAcceleration { get; set; } = 1_500;
    public float Ax { get; set; }
    public float Ay { get; set; }
    public float CritChance { get; set; }
    public float BlockChance { get; set; }

    public Player(string name, int x, int y, string sprite)
        : base(name, x, y, sprite) { }

    public override void Update (){
        Move();
    }

    public override void Render(Graphics g, PictureBox pb)
    { 
        // g.FillRectangle(
        //     Brushes.Red,
        //     new RectangleF
        //     {
        //         X = this.X - this.Width / 2,
        //         Y = this.Y - this.Height / 2,
        //         Width = this.Width,
        //         Height = this.Height
        //     }
        // );
        CreateHitbox(this.X, this.Y, 100, 200);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }

    public void Move()
    {
        var OldX = X;
        var OldY = Y;

        var now = DateTime.Now;
        var time = now - last;
        var secs = (float)time.TotalSeconds;
        last = now;

        double magnitude = Math.Sqrt(Ax * Ax + Ay * Ay);

        if (magnitude != 0)
        {
            vx += (float)(Ax / magnitude) * BaseAcceleration * secs;
            vy += (float)(Ay / magnitude) * BaseAcceleration * secs;
        }

        X += vx * secs;
        Y += vy * secs;

        vx *= MathF.Pow(0.001f, secs);
        vy *= MathF.Pow(0.001f, secs);

        const int max = 600;
        if (vx > max)
            vx = max;
        else if (vx < -max)
            vx = -max;
        
        if (vy > max)
            vy = max;
        else if (vy < -max)
            vy = -max;
        
        if (!CollisionManager.Current.CheckCollisions(this))
            return;

        const float energyLoss = 0.2f;
        vx = -vx * energyLoss;
        vy = -vy * energyLoss;
        X = OldX;
        Y = OldY;
    }

    public void MoveUp() => this.Ay = -1;

    public void MoveDown() => this.Ay = 1;

    public void MoveRight() => this.Ax = 1;

    public void MoveLeft() => this.Ax = -1;

    public void StopY_axis() => this.Ay = 0;

    public void StopX_axis() => this.Ax = 0;

    public void Info(){
        MessageBox.Show( $"X: {this.X}  Y:{this.Y}");
    }
}
