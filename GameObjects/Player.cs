using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

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
    public bool isVulnerable { get; set; }
    public DateTime lastDamage { get; set; }

    public Player(string name, int x, int y)
        // : base(name, x, y, "./assets/Sprites/Player/NewSprite/k_0.png")
        : base(name, x, y, "../../../assets/Sprites/Player/SPRITE/k_0.png")
    {
        this.Height = 340;
        this.Width = 0.894118f * this.Height;
        this.Width /= 3f;
        this.Height /= 3f;
    }

    public override void Update()
    {
        VerifyVulnerability();
        Move();
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        // this.Sprite.
        g.DrawImage(
            this.Sprite,
            new RectangleF(
                this.X - this.Width / 2,
                this.Y - this.Height / 2,
                this.Width,
                this.Height
            )
        );
        g.DrawString($"Player HP: {this.Hp}", SystemFonts.DefaultFont, Brushes.White, 10, 30);
        CreateHitbox(this.X, this.Y + 10, this.Width * 0.75f, this.Height - 20);
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

        this.Weapon.Move();

        if (vx > 8)
            StopRight();
        else if (vx < -8)
            StopLeft();
        else if (vy < -8)
            StopUp();
        else if (vy > 8)
            StopDown();

        if (vx > 20)
            AnimatePLayer(9, 12);
        else if (vx < -20)
            AnimatePLayer(5, 8);
        else if (vy < -20)
            AnimatePLayer(13, 16);
        else if (vy > 20)
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

        if (
            !(
                CollisionManager.Current.CheckCollisions(this)
                || CollisionManager.Current.ScreenColision(this)
            )
        )
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
        MessageBox.Show(Weapon.Ax.ToString());
        MessageBox.Show(Weapon.Ay.ToString());
        // MessageBox.Show($"X: {this.X}  Y:{this.Y} Xw:{this.Weapon.X} Yw:{this.Weapon.Y} HitBoxX:{this.Weapon.Hitbox.X} HitboxY:{this.Weapon.Hitbox.Y}");
        // MessageBox.Show($"Colision:{this.Y + this.Hitbox.Height / 2 > 1080} HitboxY:{this.Y + this.Hitbox.Height / 2}");
    }

    public void Attack()
    {
        var now = DateTime.Now;
        var dt = now - this.lastAttack;
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

        if (this.Weapon.WindBlade)
        {
            var ax = Weapon.Ax;
            var ay = Weapon.Ay;
            switch (ax)
            {
                case -1:
                    GameEngine.Current.AddObject(
                        new WindBlade(
                            "Bullet",
                            this.X - this.Width / 2 - 25,
                            this.Y,
                            50,
                            50,
                            180,
                            this
                        )
                    );
                    break;
                case 1:
                    GameEngine.Current.AddObject(
                        new WindBlade(
                            "Bullet",
                            this.X + this.Width / 2 + 25,
                            this.Y,
                            50,
                            50,
                            0,
                            this
                        )
                    );
                    break;
            }
            switch (ay)
            {
                case -1:
                    GameEngine.Current.AddObject(
                        new WindBlade(
                            "Bullet",
                            this.X,
                            this.Y - this.Height / 2 - 25,
                            50,
                            50,
                            270,
                            this
                        )
                    );
                    break;
                case 1:
                    GameEngine.Current.AddObject(
                        new WindBlade(
                            "Bullet",
                            this.X,
                            this.Y + this.Height / 2 + 25,
                            50,
                            50,
                            90,
                            this
                        )
                    );
                    break;
            }
            this.lastAttack = now;
        }
    }

    public void ReceiveDamage()
    {
        if (isVulnerable)
        {
            this.Hp--;
            lastDamage = DateTime.Now;
        }

        isVulnerable = false;
    }


    public void VerifyVulnerability(){
        var now = DateTime.Now;
        var diff = now - lastDamage;
        var seconds = diff.TotalSeconds;
        if(seconds > 3)
            isVulnerable = true; 
    }
}
