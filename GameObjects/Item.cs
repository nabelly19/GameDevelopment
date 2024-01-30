using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class Item : GameObject, IItemMarket 
{
    public int itemValue { get; set; }
    public int itemOccurrence { get; set; }
    public ItemManager ItemManager { get; set; }
    Player player { get; set; } 
    public Item(string name, float x, float y, float width, float height) 
    : base(name, x, y, width, height)
    {
        this.ItemManager = new(this);
    }
    public override void Render(Graphics g, PictureBox pb)
    {
        base.Render(g, pb);
    }
    public override void Update()
    {
        base.Update();
    }
    public void BuyIt()
    {

       this.player.coinWallet -= itemValue;
    }
}
