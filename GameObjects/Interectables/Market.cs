using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class Market : GameObject, IInteractable
{
    public List<Item> AllMarketItems = new();
    public bool isInteractable { get; set; } = true;
    public RectangleF rectangle;
    public Market(string name, float x, float y, float width, float height) : base(name, x, y, width, height)
    {
        CreateHitbox(x, Y, width, height);
    }
    public override void Render(Graphics g, PictureBox pb)
    {
        g.DrawRectangle(Pens.PaleGoldenrod, rectangle);
    }
    public override void Update()
    {
        var collided = CollisionManager.GetCollisions(this).FirstOrDefault();
            if (collided is Player)
            {
                CollisionManager.RemoveGameObject(this);
            }
    }

    public void showCurrentMarket()
    {
        
    }

}
