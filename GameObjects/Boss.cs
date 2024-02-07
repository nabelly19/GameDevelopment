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
    public Boss(string name, int x, int y, string sprite)
        : base(name, x, y, sprite)
    {
        this.Manager = new(this);
    }

    public override void Update()
    {
        verifyDeath();

        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        Attack();

        var collided = CollisionManager.GetCollisions(this);
        foreach (var other in collided)
        {
            if (other is IAttackable player)
                player.ReceiveDamage();
        }
    }

    public override void Render(Graphics g, PictureBox pb)
    {

        g.DrawString($"HP Boss: {this.Hp}", SystemFonts.DefaultFont, Brushes.White, 10, 20);
        g.DrawImage(this.Sprite, this.X - this.Width / 2, this.Y - this.Height / 2);
        g.DrawRectangle(Pens.White, this.Hitbox);
        RenderState(g, pb);
        g.DrawString($"Boss Vulnerability: {isVulnerable.ToString()}", SystemFonts.DefaultFont, Brushes.White, 10, 170);
    }

    public virtual void ReceiveDamage()
        => this.Hp--;
    

    private void RenderState(Graphics g, PictureBox pb)
    {   
        var actualState = this.Manager.Current;
        if (actualState is null)
            g.DrawString($"Boss State: ", SystemFonts.DefaultFont, Brushes.White, 10, 155);
        else
            g.DrawString($"Boss State: {actualState.ToString()}", SystemFonts.DefaultFont, Brushes.White, 10, 155);
    }
    private bool verifyDeath()
    {
        if (!this.isAlive)
        {
            this.Manager.Current = new DeadState();
            return true;
        }
        return false;
    }

    public void Attack()
        => this.Manager.Act();
         
        
    
}
