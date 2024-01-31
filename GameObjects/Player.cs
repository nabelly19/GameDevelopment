using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class Player : GameObject, IMoveable, IAttackable
{
    private float vx = 0f;
    private float vy = 0f;
    private DateTime last = DateTime.Now;
    private DateTime lastAttack = DateTime.Now;
    public int Steps { get; set; } = 0;
    public int SlowFrameRate { get; set; } = 0;

    public Weapon Weapon { get; set; }

    public int Hp { get; set; } = 3;
    public bool isVulnerable { get; set; }
    public bool isAlive { get; set; }
    public float BaseAcceleration { get; set; } = 1_300;
    public float Ax { get; set; }
    public float Ay { get; set; }
    public float CritChance { get; set; }
    public float BlockChance { get; set; }
    public DateTime LastDamage { get; set; }
    public int CoinWallet { get; set; } = 0;
    public bool isInteractable { get; set; }

    public Player(string name, int x, int y)
        // : base(name, x, y, "./assets/Sprites/Player/NewSprite/k_0.png")
        : base(name, x, y, "../../../assets/Sprites/Player/NewSprite/k_0.png")
    {
        this.Height = 340;
        this.Width = 0.894118f * this.Height;
        this.Width /= 2.8f;
        this.Height /= 2.8f;
    }

    public override void Update()
    {
        VerifyVulnerability();
        updateHitbox();
        this.Weapon.Update();
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
        g.DrawRectangle(Pens.White, this.Hitbox);
        g.DrawString($"Player HP: {this.Hp}", SystemFonts.DefaultFont, Brushes.White, 10, 30);
        g.DrawString($"Player Wallet: {this.CoinWallet}", SystemFonts.DefaultFont, Brushes.White, 10, 40);
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
        // else if (vy > 8)
        //     StopDown();

        if (vx > 20)
            AnimatePLayer(9, 12);
        else if (vx < -20)
            AnimatePLayer(5, 8);
        else if (vy < -20)
            AnimatePLayer(13, 16);
        else if (vy > 20)
            AnimatePLayer(1, 4);
        else if ((int)vy == 0)
            AnimatePLayer(17, 21);

        double magnitude = Math.Sqrt(Ax * Ax + Ay * Ay);

        if (magnitude != 0)
        {
            vx += (float)(Ax / magnitude) * BaseAcceleration * secs;
            vy += (float)(Ay / magnitude) * BaseAcceleration * secs;
        }

        X += vx * secs;
        Y += vy * secs;

        updateHitbox();

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

        if (!(CollisionManager.CheckCollisions(this) || CollisionManager.ScreenColision(this)))
            return;

        const float energyLoss = 0.2f;
        vx = -vx * energyLoss;
        vy = -vy * energyLoss;

        X = OldX;
        Y = OldY;
    }

    public void MoveUp()
    => this.Ay = -1;


    public void MoveDown()
    => this.Ay = 1;


    public void MoveRight()
    => this.Ax = 1;


    public void MoveLeft()
    => this.Ax = -1;


    public void StopY_axis() => this.Ay = 0;

    public void StopX_axis() => this.Ax = 0;

    public void StopUp() => this.Sprite = Resources.PlayerSprites[15];

    public void StopDown() => this.Sprite = Resources.PlayerSprites[0];

    public void StopLeft() => this.Sprite = Resources.PlayerSprites[5];

    public void StopRight() => this.Sprite = Resources.PlayerSprites[9];

    private void AnimatePLayer(int start, int end)
    {
        SlowFrameRate += 1;

        if (SlowFrameRate > 3)
        {
            Steps++;
            SlowFrameRate = 0;
        }

        if (Steps > end || Steps < start)
            Steps = start;

        this.Sprite = Resources.PlayerSprites[Steps];
    }

    public void Info() { }

    public void Attack()
    {
        var now = DateTime.Now;
        var dt = now - this.lastAttack;
        var secs = (float)dt.TotalMilliseconds;

        if (secs < this.Weapon.AtkSpeed)
            return;

        var collisions = CollisionManager.GetCollisions(this.Weapon);

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
                    GameEngine.Current.AddObjectToCollisionList(
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
                    GameEngine.Current.AddObjectToCollisionList(
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
                    GameEngine.Current.AddObjectToCollisionList(
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
                    GameEngine.Current.AddObjectToCollisionList(
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
            verifyLifeStatus();
            LastDamage = DateTime.Now;
        }

        isVulnerable = false;
    }

    private void verifyLifeStatus()
    {
        if (this.Hp < 0)
            this.Hp = 0;
    }

    public void VerifyVulnerability()
    {
        var now = DateTime.Now;
        var diff = now - LastDamage;
        var seconds = diff.TotalSeconds;
        if (seconds > 3)
            isVulnerable = true;
    }
    private void updateHitbox() =>
        CreateHitbox(this.X + 5, this.Y + 13, this.Width * 0.5f, this.Height - 35);
    public void Interact()
    {
        foreach (var item in CollisionManager.GameObjects)
        {
            if (item is Interactable iter)
            {
                if (iter.VerifyCollisions())
                iter.Interact();
                return;
            }
        }

    }

    public void ColectItem()
    {
        if (isInteractable)
            this.CoinWallet++;
    }
}
