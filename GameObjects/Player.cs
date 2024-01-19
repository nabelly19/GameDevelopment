using System;
using System.Drawing;
using System.Windows.Forms;

// namespace Entity;

public class Player : GameObject, IMoveable, IAttackable
{
    private float vx = 0f;
    private float vy = 0f;
    private DateTime last = DateTime.Now;
    private DateTime lastAttack = DateTime.Now;
    public int steps { get; set; } = 0;
    public int slowFrameRate { get; set; } = 0;

    public Weapon Weapon { get; set; }

    public int Hp { get; set; } = 3;
    public float BaseAcceleration { get; set; } = 1_000;
    public float Ax { get; set; }
    public float Ay { get; set; }
    public float CritChance { get; set; }
    public float BlockChance { get; set; }

    public Player(string name, int x, int y, string sprite)
        : base(name, x, y, sprite)
    {
        this.Height = 340;
        this.Width = 0.894118f * this.Height;
        this.Width /= 4;
        this.Height /= 4;
    }

    public override void Update()
    {
        Move();
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        g.DrawImage(
            this.Sprite,
            new RectangleF(
                this.X - this.Width / 2,
                this.Y - this.Height / 2,
                this.Width,
                this.Height
            )
        );
        CreateHitbox(this.X, this.Y + 10, this.Width * 0.75f, this.Height - 20);
        // CreateHitbox(this.X, this.Y, 250, 300);

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

        this.Weapon.Update();
        this.Weapon.Move();

        if ((int)vx > 8)
            StopRight();
        else if ((int)vx < -8)
            StopLeft();
        else if ((int)vy < -8)
            StopUp();
        else if ((int)vy > 8)
            StopDown();

        if ((int)vx > 20)
            AnimatePLayer(9, 12);
        else if ((int)vx < -20)
            AnimatePLayer(5, 8);
        else if ((int)vy < -20)
            AnimatePLayer(13, 16);
        else if ((int)vy > 20)
            AnimatePLayer(1, 4);

        double magnitude = Math.Sqrt(Ax * Ax + Ay * Ay);

        if (magnitude != 0)
        {
            vx += (float)(Ax / magnitude) * BaseAcceleration * secs;
            vy += (float)(Ay / magnitude) * BaseAcceleration * secs;
        }

        X += vx * secs;
        Y += vy * secs;

        CreateHitbox(this.X, this.Y + 10, this.Width * 0.75f, this.Height - 20);

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

    public void MoveUp()
    {
        this.Ay = -1;
    }

    public void MoveDown()
    {
        this.Ay = 1;
    }

    public void MoveRight()
    {
        this.Ax = 1;
    }

    public void MoveLeft()
    {
        this.Ax = -1;
    }

    public void StopY_axis() => this.Ay = 0;

    public void StopX_axis() => this.Ax = 0;

    public void StopUp() => this.Sprite = Resources.Current.PlayerSprites[15];

    public void StopDown() => this.Sprite = Resources.Current.PlayerSprites[0];

    public void StopLeft() => this.Sprite = Resources.Current.PlayerSprites[5];

    public void StopRight() => this.Sprite = Resources.Current.PlayerSprites[9];

    private void AnimatePLayer(int start, int end)
    {
        slowFrameRate += 1;

        if (slowFrameRate > 3)
        {
            steps++;
            slowFrameRate = 0;
        }

        if (steps > end || steps < start)
        {
            steps = start;
        }

        this.Sprite = Resources.Current.PlayerSprites[steps];
    }

    public void Info()
    {
        MessageBox.Show($"X: {this.X}  Y:{this.Y} Xw:{this.Weapon.X} Yw:{this.Weapon.Y} HitBoxX:{this.Weapon.Hitbox.X} HitboxY:{this.Weapon.Hitbox.Y}");
    }

    public void Attack() {
        
        var now = DateTime.Now;
        var dt = now - this.lastAttack ;
        var secs = (float)dt.TotalMilliseconds;

        if (secs < this.Weapon.AtkSpeed)
            return;

        var collisions = CollisionManager.Current.GetCollisions(this.Weapon);

        foreach (var obj in collisions)
        {
            if (obj == this)
                continue;

            if (obj is IAttackable other)
            {
                this.lastAttack = now;
                other.ReceiveDamage();
                return;
            }
        }

    }
}
