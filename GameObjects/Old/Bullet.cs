// // namespace Entity;

// using System;
// using System.Drawing;
// using System.Linq;
// using System.Security.Cryptography.X509Certificates;
// using System.Windows.Forms;

// public class Projectile : GameObject
// {
//     public PointF center = new(100, 100);
//     public float angle = 0;
//     public float radius = 35; 

//     public Projectile(string name, int x, int y, float width, float height)
//         : base(name, x, y, width, height)
//     {
//         DisableHitbox();
//     }

//     public override void Update()
//     {
//         Move();
//     }

//     public override void Render(Graphics g, PictureBox pb)
//     {
//         CreateHitbox(this.X, this.Y, this.Width, this.Height);
//         g.DrawRectangle(Pens.White, this.Hitbox);

//         // g.DrawRectangle(Pens.Red, this.center.X - 5, this.center.Y - 5, 10, 10);
//     }

//     public void Move()
//     {
//         RotatePoints();
//         // this.X = this.center.X;
//         // this.Y = this.center.Y;

//         var collided = CollisionManager.Current.GetCollisions(this).FirstOrDefault();
//         if (collided is not null)
//         {
//             if (collided is Player other)
//                 other.ReceiveDamage();
//             CollisionManager.Current.RemoveGameObject(this);
//             return;
//         }
//         if (CollisionManager.Current.ScreenColision(this))
//             CollisionManager.Current.RemoveGameObject(this);
//     }

//     public void RotatePoints()
//     {
//         float radians = MathF.PI / 180 * angle;
//         float cos = MathF.Cos(radians);
//         float sin = MathF.Sin(radians);

//         this.X = this.center.X + this.radius * cos;
//         this.Y = this.center.Y + this.radius * sin;

//         this.center.X+=1.5f;
//         this.center.Y+=1.5f;
//         this.angle += 10f;

//         return;
//     }
// }
