using System.Drawing;
using System.Windows.Forms;

public abstract class Item : GameObject, IItemMarket
{
    public int Value { get; set; } = 1;
    public int Occurrence { get; set; }
    public bool Temporary { get; set; }
    private bool isbought = false;

    public Item(string name, float x, float y, float width, float height)
        : base(name, x, y, width, height)
    {
        DisableHitbox();
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
    }

    protected Item(string name, float x, float y)
        : base(name, x, y, Resources.Cards[0])
    {
        DisableHitbox();
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
    }

    protected Item(string name, float x, float y, string sprite)
        : base(name, x, y, sprite)
    {
        DisableHitbox();
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
    }
    protected Item(string name, float x, float y, Image sprite)
        : base(name, x, y, sprite)
    {
        DisableHitbox();
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(
            this.X + ClientScreen.ResponsiveX(105),
            this.Y + ClientScreen.ResponsiveX(150),
            ClientScreen.ResponsiveX(210),
            ClientScreen.ResponsiveX(300)
        );
        g.DrawImage(
            this.Sprite,
            this.X,
            this.Y,
            ClientScreen.ResponsiveX(210),
            ClientScreen.ResponsiveX(300)
        );
        g.DrawString(
            this.Name,
            new Font("Pixelify Sans", ClientScreen.ResponsiveX(26), FontStyle.Bold),
            Brushes.White,
            new PointF(this.Hitbox.X +  ClientScreen.ResponsiveX(20), this.Hitbox.Y + 1.95f *  ClientScreen.ResponsiveX(120))
        );
        g.DrawString(
            $"Price: {this.Value.ToString()}",
            new Font("Pixelify Sans", ClientScreen.ResponsiveX(26), FontStyle.Bold),
            Brushes.White,
            new PointF(this.Hitbox.X +  ClientScreen.ResponsiveX(20), this.Hitbox.Y + 1.95f *  ClientScreen.ResponsiveX(160))
        );
    }

    public override void Update()
    {
        var b = CollisionManager.CheckCollisionByPoint(this.Hitbox, Cursor.Position);
        if (b)
            BuyIt();
    }

    public virtual void BuyIt()
    {
        var player = GameEngine.Current.Player;

        if (ItemManager.Bought.Contains(this))
            return;
        if (player.CoinWallet < Value)
            return;
        player.CoinWallet -= Value;
        ItemManager.Bought.Add(this);
        ApplyBuff();
    }

    public virtual void ApplyBuff() { }
}
