using System.Data;
using System.Drawing;
using System.Windows.Forms;

public class Boss : GameObject, IAttackable
{
    public int Hp { get; set; } = 3;
    public StateManager Manager { get; private set; }
    public bool isVulnerable { get; set; }
    public bool isAlive { get; set; } = true;
    public float BlockChance { get; set; } = 0f;

    public Boss(string name, float x, float y, string sprite)
        : base(name, x, y, sprite)
    {
        this.Manager = new(this);
    }

    public Boss(string name, float x, float y, Image sprite)
        : base(name, x, y, sprite)
    {
        this.Manager = new(this);
    }

    public override void Update()
    {
        verifyDeath();

        CreateHitbox(this.X, this.Y, ClientScreen.ResponsiveX(this.Width), ClientScreen.ResponsiveY(this.Height));
        Attack();

        var collided = CollisionManager.GetCollisions(this);
        foreach (var other in collided)
        {
            if (other is IAttackable player)
                player.ReceiveDamage();
        }
    }

        Font font = new Font("Arial", ClientScreen.HeightFactor * 35);
    public override void Render(Graphics g, PictureBox pb)
    {
        g.DrawString($"{this.Hp}", font, Brushes.White, ClientScreen.ResponsiveX(80), ClientScreen.ResponsiveY(138));
        if (this.Manager.Current is PlatformState)
            g.DrawImage(
                this.Sprite,
                ClientScreen.ResponsiveX(this.X - this.Width / 2 * 1.45f),
                ClientScreen.ResponsiveY(this.Y - this.Height / 2),
                ClientScreen.ResponsiveX(this.Sprite.Width),
                ClientScreen.ResponsiveY(this.Sprite.Height)
            );
        else
            g.DrawImage(
                this.Sprite,
                this.X - ClientScreen.ResponsiveX(this.Width / 2),
                this.Y - ClientScreen.ResponsiveX(this.Height / 2),
                ClientScreen.ResponsiveX(this.Sprite.Width),
                ClientScreen.ResponsiveY(this.Sprite.Height)
            );
        // g.DrawRectangle(Pens.White, this.Hitbox);
        // RenderState(g, pb);
        // g.DrawString($"Boss Vulnerability: {isVulnerable}", SystemFonts.DefaultFont, Brushes.White, 10, 170);
    }

    public virtual void ReceiveDamage() => this.Hp--;

    private void RenderState(Graphics g, PictureBox pb)
    {
        var actualState = this.Manager.Current;
        if (actualState is null)
            g.DrawString($"Boss State: ", SystemFonts.DefaultFont, Brushes.White, 10, 155);
        else
            g.DrawString(
                $"Boss State: {actualState.ToString()}",
                SystemFonts.DefaultFont,
                Brushes.White,
                10,
                155
            );
    }

    private bool verifyDeath()
    {
        if (!this.isAlive)
        {
            if (this.Manager.Current is PlatformState)
                this.Manager.Current.GoToNext();
            this.Manager.Current = new DeadState();
            return true;
        }
        return false;
    }

    public void Attack() => this.Manager.Act();
}
