// using System;
// using System.Drawing;
// using System.Windows.Forms;

// namespace OldEntity;

// /// <summary>
// /// The Player class represents a player character in a game, with properties such as HP, weapon, crit
// /// chance, block chance, velocity, and angle, as well as methods for attacking, receiving damage, and
// /// movement.
// /// </summary>
// public class Player : Hittable
// {
//     public int HP { get; set; }
//     public float CritChance { get; set; }
//     public float BlockChance { get; set; }
//     public float Base_Velocity { get; set; } = 5;
//     public float Velocity_X { get; set; }
//     public float Velocity_Y { get; set; }

//     public Player(string path, int width, int height)
//         : base(path, width, height) { }

//     public void Attack() { }

//     public void ReceiveDamage() => this.HP--;

//     public void Move()
//     {
//         this.Old_X = this.X;
//         this.Old_Y = this.Y;

//         double magnitude = Math.Sqrt(Velocity_X * Velocity_X + Velocity_Y * Velocity_Y);

//         if (magnitude == 0)
//             return;

//         this.X += (float)(this.Velocity_X / magnitude) * Base_Velocity;
//         this.Y += (float)(this.Velocity_Y / magnitude) * Base_Velocity;
//     }

//     public void MoveUp() => this.Velocity_Y = -1;

//     public void MoveDown() => this.Velocity_Y = 1;

//     public void MoveRight() => this.Velocity_X = 1;

//     public void MoveLeft() => this.Velocity_X = -1;

//     public void StopY_axis() => this.Velocity_Y = 0;

//     public void StopX_axis() => this.Velocity_X = 0;

//     public override void Draw(Graphics g)
//     {
//         g.FillRectangle(
//             Brushes.Red,
//             new RectangleF
//             {
//                 X = this.X - this.Width / 2,
//                 Y = this.Y - this.Height / 2,
//                 Width = this.Width,
//                 Height = this.Height
//             }
//         );
//         CreateHitbox(this.X, this.Y, this.Width + 1, this.Height + 1);

//         g.DrawRectangle(Pens.White, this.Hitbox);
//     }
// }
