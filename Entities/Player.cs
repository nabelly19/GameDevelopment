using System;
using System.Windows.Forms;
using System.Drawing;

public class Player : Hittable
{
    public int HP { get; set; }
    public Weapon weapon { get; set; }
    public float CritChance { get; set; }
    public float BlockChance { get; set; }
    public float Velocity_X { get; set; }
    public float Velocity_Y { get; set; }
    public double Angle { get; set; }
    public Player(string path) : base(path) { }


    public void Attack( ) { }
    public void ReceiveDamage( )
        => this.HP--;

    public void Move ()
    {
        // this.Velocity_X = 5; this.Velocity_Y = 5;
        this.X += (float)(this.Velocity_X * Math.Cos(this.Angle));
        this.Y += (float)(this.Velocity_Y * Math.Sin(this.Angle));
    }
    
    public void MoveY_axis ()
        => this.Velocity_Y = 5;
    public void StopY_axis ()
        => this.Velocity_Y = 0;
    public void MoveX_axis ()
        => this.Velocity_X = 5;
    public void StopX_axis ()
        => this.Velocity_X = 0;

    public override bool Colision(Hittable hittable)
    {
        throw new NotImplementedException();
    }

    public override void Draw(Graphics g)
    {
        g.FillRectangle(
        Brushes.Red,
        new RectangleF
            {
                X = this.X - 5,
                Y = this.Y - 5,
                Width = 10,
                Height = 10
            }
        );
    }
}
