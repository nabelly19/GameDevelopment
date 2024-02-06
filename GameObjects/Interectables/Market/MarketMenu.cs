using System;
using System.Drawing;
using System.Windows.Forms;

public class MarketMenu : GameObject
{
    private PointF[] itemsPosition =
    {
        new(Screen.PrimaryScreen.Bounds.Width / 2 * 0.75f, Screen.PrimaryScreen.Bounds.Height / 2),
        new(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2),
        new(Screen.PrimaryScreen.Bounds.Width / 2 * 1.25f, Screen.PrimaryScreen.Bounds.Height / 2)
    };
    private RectangleF marketBackground { get; set; }

    public Item[] Items = new Item[3]; // tem 3 cartas neh

    public MarketMenu(string name, float x, float y, float width, float height)
        : base(name, x, y, width, height)
    {
        DisableHitbox();
        CreateHitbox(0, 0, width, height);
        ItemManager.getRandomItems(this.Items);
        replaceItems();
        float wid = Screen.PrimaryScreen.Bounds.Width / 2;
        float hei = Screen.PrimaryScreen.Bounds.Height / 2;

        this.marketBackground = new RectangleF(wid - wid / 2, hei - hei / 2, wid, hei);
    }

    public void replaceItems()
    {
        for (int i = 0; i < itemsPosition.Length; i++)
        {
            var current = this.Items[i];
            current.X = itemsPosition[i].X;
            current.Y = itemsPosition[i].Y;
            current.CreateHitbox(current.X, current.Y, current.Width, current.Height);
        }
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        replaceItems();
        g.FillRectangle(Brushes.Gray, this.marketBackground);
        foreach (var item in Items)
            item.Render(g, pb);
    }

    public override void Update()
    {
        foreach (var item in Items)
        {
            item.Update();
        }
    }
}