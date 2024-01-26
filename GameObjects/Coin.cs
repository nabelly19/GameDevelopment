using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class Coin : GameObject, IMoveable
{
    private float vy = 0;
    private float vx = 0;
    public int StateManager {get; set;}

    public Coin(string name, int x, int y)
        : base(name, x, y, Resources.Current.Coins[0])
    {
        
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawImage(this.Sprite, this.X, this.Y);
    }

    public override void Update()
    {
        Move();
    }

    public void Move()
    {
        var collided = CollisionManager.Current.GetCollisions(this).FirstOrDefault();
        
        if (collided is Player)
            CollisionManager.Current.RemoveGameObject(this);

    }


    public float BaseAcceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Ax { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Ay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}