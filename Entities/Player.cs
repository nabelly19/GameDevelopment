using System;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// The Player class represents a player character in a game, with properties such as HP, weapon, crit
/// chance, block chance, velocity, and angle, as well as methods for attacking, receiving damage, and
/// movement.
/// </summary>
public class Player : Hittable
{
    public int HP { get; set; }
    public Weapon weapon { get; set; }
    public float CritChance { get; set; }
    public float BlockChance { get; set; }
    public float Base_Velocity { get; set; } = 5;
    public float Velocity_X { get; set; }
    public float Velocity_Y { get; set; }

    public Player(string path)
        : base(path) { }

    public void Attack() { }

    public void ReceiveDamage() => this.HP--;

    public void Move()
    {
        double magnitude = Math.Sqrt(Velocity_X * Velocity_X + Velocity_Y * Velocity_Y);

        if (magnitude == 0)
            return;

        this.X += (float)(this.Velocity_X / magnitude) * Base_Velocity;
        this.Y += (float)(this.Velocity_Y / magnitude) * Base_Velocity;
    }

    public void MoveUp() => this.Velocity_Y = -1;

    public void MoveDown() => this.Velocity_Y = 1;

    public void MoveRight() => this.Velocity_X = 1;

    public void MoveLeft() => this.Velocity_X = -1;

    public void StopY_axis() => this.Velocity_Y = 0;

    public void StopX_axis() => this.Velocity_X = 0;

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
