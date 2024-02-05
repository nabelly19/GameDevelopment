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
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
    }

    public override void Update()
    {
        var b = CollisionManager.CheckCollisionByPoint(this.Hitbox, Cursor.Position);
        if (b)
            BuyIt();
        // MessageBox.Show(b.ToString());
    }

    public virtual bool BuyIt()
    {
        var player = GameEngine.Current.Player;
        if (this.isbought || player.CoinWallet < Value)
            return false;
        player.CoinWallet -= Value;
        ItemManager.Bought.Add(this);
        return true;
    }
}
