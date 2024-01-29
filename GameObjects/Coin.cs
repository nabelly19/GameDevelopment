using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public class Coin : GameObject, IMoveable
{
    public int StateManager { get; set; }
    public int steps { get; set; } = 0;
    public int slowFrameRate { get; set; } = 0;


    public Coin(string name, int x, int y)
        : base(name, x, y, Resources.Current.Coins[0])
    {
        DisableHitbox();
        this.Height = 110;
        this.Width = 1.25555f * this.Height;
        this.Width /= 4f;
        this.Height /= 4f;
    
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawImage(this.Sprite, this.X, this.Y, this.Width, this.Height);
        
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

        AnimateItem(1,5);

    }

     private void AnimateItem(int start, int end)
    {
        slowFrameRate += 1;

        if (slowFrameRate > 5)
        {
            steps++;
            slowFrameRate = 0;
        }

        if (steps > end || steps < start)
        {
            steps = start;
        }

        this.Sprite = Resources.Current.Coins[steps];
    }


    public float BaseAcceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Ax { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Ay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}