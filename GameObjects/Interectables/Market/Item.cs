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

    public override void Render(Graphics g, PictureBox pb)
    {
        g.DrawRectangle(Pens.Orange, this.Hitbox);
        g.DrawString
        (
            this.Name, 
            new Font("Arial", 26, FontStyle.Bold), 
            Brushes.Gold, 
            new PointF(this.Hitbox.X, this.Hitbox.Y)
        );
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
    }

    public override void Update()
    {
        var b = CollisionManager.CheckCollisionByPoint(this.Hitbox, Cursor.Position);
        if (b)
            BuyIt();
        // MessageBox.Show(b.ToString());
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

    public virtual void ApplyBuff() {}
}
