using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

public class Icons : GameObject
{
    // public Icons(string name, float x, float y) 
    // : base(name, x, y, Resources.IconHud[0])
    // {
    //     DisableHitbox();
    //     CreateHitbox(this.X, this.Y, this.Width, this.Height);
    // }

    public Icons(string name, float x, float y) 
    : base(name, x, y, Resources.IconHud[0])
    {
         DisableHitbox();
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
    }

    public Icons(string name, float x, float y, Image image) 
    : base(name, x, y, image)
    {
        DisableHitbox();
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
    }

    public Icons(string name, float x, float y, float width, float height)
        : base(name, x, y, width, height)
    {
        DisableHitbox();
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
    }

    

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawImage(this.Sprite, this.X, this.Y, this.Width, this.Height);
    }

}