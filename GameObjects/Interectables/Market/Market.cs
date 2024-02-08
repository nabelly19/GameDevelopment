using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class Market : Interactable
{
    public List<Item> AllMarketItems = new();
    private RectangleF MarketBackground { get; set; }
    public bool isVisible = false;

    public Market(string name, float x, float y, float width, float height) : base(name, x, y, width, height)
    {
        this.isInteractable = true;
        CreateHitbox(x, y, width, height);
    }
    public override void Render(Graphics g, PictureBox pb)
    {
        g.FillRectangle(Brushes.Transparent, this.Hitbox);
        if (this.isVisible)
        {
            showCurrentMarket();
            var collided = CollisionManager.GetCollisions(this);
            if (!collided.Contains(GameEngine.Current.Player))
                Interact();

        }
    }

    public void showCurrentMarket()
    {
        foreach (var item in HUD.Objs.ToList())
        {
            if(item is MarketMenu)
                return;
        }
        HUD.AddObject(new MarketMenu("MENU", 100, 100, 100, 100));
    }

    public override void Interact()
    {
        this.isVisible = !isVisible;
        HUD.Reset();
    }
}
