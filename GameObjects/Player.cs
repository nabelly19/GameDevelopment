using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class Player : GameObject, IMoveable, IAttackable
{
    public float Vx { get; set; } = 0f;
    public float Vy { get; set; } = 0f;
    private DateTime last = DateTime.Now;
    private DateTime lastAttack = DateTime.Now;
    public int Steps { get; set; } = 0;
    public int SlowFrameRate { get; set; } = 0;

    public Weapon Weapon { get; set; }

    public int baseHp = 3;
    public int Hp { get; set; } = 3;
    public bool isVulnerable { get; set; }
    public bool isAlive { get; set; } = true;
    public float BaseAcceleration { get; set; } = 1_300;
    public float Ax { get; set; }
    public float Ay { get; set; }
    public bool isMoving { get; set; } = true;
    public float Angle { get; set; }
    public float CritChance { get; set; }
    public float BlockChance { get; set; }
    public DateTime LastDamage { get; set; }
    public int CoinWallet { get; set; } = 0;

    private float Fx = 0f;
    private float Fy = 0;

    public Player(string name, int x, int y)
        // : base(name, x, y, "./assets/Sprites/Player/NewSprite/k_0.png")
        : base(name, x, y, "assets/Sprites/Player/NewSprite/k_0.png")
    {
        this.Height = 340;
        this.Width = 0.894118f * this.Height;
        this.Width /= 2.8f;
        this.Height /= 2.8f;
    }

    int fpsCount = 0;

    public override void Update()
    {
        if(!this.isAlive)
        {

            AnimateThisDeathPlayer(0,2);
        }

        if (!isMoving)
            return;

        if (Weapon.isAttaking)
        {
            fpsCount++;
            if (fpsCount > 10)
            {
                fpsCount = 0;
                Weapon.isAttaking = false;
                this.lastAttack = DateTime.Now;
                CollisionManager.RemoveGameObject(Weapon);
            }
        }
        VerifyVulnerability();
        updateHitbox();
        this.Weapon.Update();
        Move();
    }

    public void ApplyForce(float fx, float fy)
    {
        this.Fx += fx;
        this.Fy += fy;
    }

    public void Revive()
    {
        this.Vx = this.Vy = this.Fx = this.Fy = 0;
        this.isAlive = true;
        this.isMoving = true;
        this.Hp = this.baseHp;
    }

        Font font = new Font("Arial", ClientScreen.HeightFactor * 35);
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
        g.DrawString(
            $"{this.Hp}",
            font,
            Brushes.White,
            ClientScreen.ResponsiveX(80),
            ClientScreen.ResponsiveY(35)
        );
        g.DrawString(
            $"{this.CoinWallet}x",
            font,
            Brushes.White,
            ClientScreen.ResponsiveX(80),
            ClientScreen.ResponsiveY(85)
        );
        g.DrawString(
            $"Player Block: {BlockChance}",
            SystemFonts.DefaultFont,
            Brushes.White,
            10,
            ClientScreen.ResponsiveY(185)
        );
        g.DrawString(
            $"Player CC: {CritChance}",
            SystemFonts.DefaultFont,
            Brushes.White,
            10,
            ClientScreen.ResponsiveY(200)
        );
    }

    public void Move()
    {
        var now = DateTime.Now;
        var time = now - last;
        var secs = (float)time.TotalSeconds;
        last = now;

        this.Weapon.Move();

        if (Vx > 8)
            StopRight();
        else if (Vx < -8)
            StopLeft();
        else if (Vy < -8)
            StopUp();

        if (Vx > 20)
            AnimatePLayer(9, 12);
        else if (Vx < -20)
            AnimatePLayer(5, 8);
        else if (Vy < -20)
            AnimatePLayer(13, 16);
        else if (Vy > 20)
            AnimatePLayer(1, 4);
        else if ((int)Vy == 0)
            AnimatePLayer(17, 21);

        var OldX = X;
        var OldY = Y;

        double magnitude = Math.Sqrt(Ax * Ax + Ay * Ay);
        if (magnitude != 0)
        {
            Vx += (float)(Ax / magnitude) * BaseAcceleration * secs;
            Vy += (float)(Ay / magnitude) * BaseAcceleration * secs;
        }

        Vx += Fx * secs;
        Vy += Fy * secs;

        Fx /= 4;
        Fy /= 4;

        X += Vx * secs;
        Y += Vy * secs;

        Vx *= MathF.Pow(0.001f, secs);
        Vy *= MathF.Pow(0.001f, secs);

        float max = BaseAcceleration * 0.4615f;
        if (Vx > max)
            Vx = max;
        else if (Vx < -max)
            Vx = -max;

        if (Vy > max)
            Vy = max;
        else if (Vy < -max)
            Vy = -max;

        updateHitbox();
        var collInfo = CollisionManager.CheckCollisionsData(this);
        if (collInfo == CollisionType.None && !CollisionManager.ScreenColision(this))
            return;

        if ((collInfo & CollisionType.Bottom) > 0)
            Vy = -0.8f * Vy;
        else
            Vy = Vy * -1;

        if ((collInfo & CollisionType.Left) > 0)
            Vx = -0.8f * Vx;
        else
            Vx = Vx * -1;

        X = OldX;
        Y = OldY;
        updateHitbox();
    }

    public void MoveUp() => this.Ay = -1;

    public void MoveDown() => this.Ay = 1;

    public void MoveRight() => this.Ax = 1;

    public void MoveLeft() => this.Ax = -1;

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

      public void AnimateThisDeathPlayer(int start, int end)
    {
        SlowFrameRate += 1;
        for (int i = 0; i < 2; i ++)
        {
            if (SlowFrameRate > 3)
            {
                Steps++;
                SlowFrameRate = 0;
            }
            if(Steps > 2)
                return;
            this.Sprite = Resources.Death[Steps];
            
        }  
    }


    public void Info() { }

    public void Attack()
    {
        var now = DateTime.Now;
        var dt = now - this.lastAttack;
        var secs = (float)dt.TotalMilliseconds;

        if (secs < this.Weapon.AtkSpeed)
            return;

        CollisionManager.RemoveGameObject(Weapon);
        if (!Weapon.WindBlade)
            GameEngine.Current.AddObjectToCollisionList(Weapon);
        this.Weapon.isAttaking = true;

        var collisions = CollisionManager.GetCollisions(this.Weapon);

        foreach (var obj in collisions)
        {
            if (obj == this)
                continue;

            if (obj is IAttackable other)
            {
                if (other.isVulnerable)
                {
                    if (CritDamage())
                        other.ReceiveDamage();
                    other.ReceiveDamage();
                }
                this.lastAttack = now;
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
                            Resources.Weapon[3],
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
                            Resources.Weapon[0],
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
                            Resources.Weapon[1],
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
                            Resources.Weapon[2],
                            90,
                            this
                        )
                    );
                    break;
            }
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
        if (this.Hp <= 0)
        {
            this.Hp = 0;
            this.Fx = this.Fy = 0;
            this.Steps = 0;
            this.isAlive = false;
            this.isMoving = false;
           
        }
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
                if (!iter.isInteractable)
                    return;
                if (iter.VerifyCollisions())
                    iter.Interact();
            }
        }
    }

    public void ColectCoin() => this.CoinWallet++;

    private bool CritDamage()
    {
        float chance = (float)Random.Shared.NextDouble();
        if (chance < this.CritChance)
            return true;
        return false;
    }
}
