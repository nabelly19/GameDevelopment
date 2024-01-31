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
    public bool isInteractable { get; set; }
    
    public Market(string name, float x, float y, float width, float height) : base(name, x, y, width, height)
    {
        this.isInteractable = true;
        CreateHitbox(x, Y, width, height);
    }
    public override void Render(Graphics g, PictureBox pb)
    {
        g.DrawRectangle(Pens.PaleGoldenrod, Hitbox);
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

    public override void Interact()
    {
        throw new NotImplementedException();
    }
}
