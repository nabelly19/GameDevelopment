using System.Drawing;
using System.Windows.Forms;

public class MarketMenu : GameObject
{
    private RectangleF MarketBackground { get; set; }
    public MarketMenu(string name, float x, float y, float width, float height) : base(name, x, y, width, height)
    {
        DisableHitbox();
        CreateHitbox(X, Y, width, height);

        float wid = Screen.PrimaryScreen.Bounds.Width / 2;
        float hei = Screen.PrimaryScreen.Bounds.Height / 2;

        this.MarketBackground = new RectangleF(wid - wid / 2, hei - hei / 2, wid, hei);
    }



    public override void Render(Graphics g, PictureBox pb)
    {
        g.FillRectangle(Brushes.Gray, this.MarketBackground);
    }
}