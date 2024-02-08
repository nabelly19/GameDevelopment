using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class MarketMenu : GameObject
{
    private PointF[] itemsPosition =
    {
        new(ClientScreen.Width / 2 * 0.65f, Screen.PrimaryScreen.Bounds.Height / 2 * 0.75f),
        new(ClientScreen.Width / 2 * 0.90f, Screen.PrimaryScreen.Bounds.Height / 2 * 0.75f),
        new(ClientScreen.Width / 2 * 1.15f, Screen.PrimaryScreen.Bounds.Height / 2 * 0.75f)
    };

      private PointF[] iconsPosition =
    {
        new(ClientScreen.Width / 2 * 0.65f, Screen.PrimaryScreen.Bounds.Height / 2),
        new(ClientScreen.Width / 2 * 0.90f, ClientScreen.Height / 2)
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
        float wid = ClientScreen.Width / 2;
        float hei = ClientScreen.Height / 2;

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
