// using System.Drawing;
// using System.Windows.Forms;

// public class Dungeon_01 : Map
// {
//     public Dungeon_01(string path) : base(path) { }

//     public override void CreateWalls(PictureBox pb)
//     {
//         float x = pb.Width / 2;
//         float y = pb.Height / 2;

//         float width = this.Image.Width / 2;
//         float height = this.Image.Height / 2;

//         var hit1 = new RectangleF(
//                 (x) + width, 
//                 y - height,
//                 100,  this.Image.Height
//             );

//         var hit2 = new RectangleF(
//                 (x) - width - 100, 
//                 y + height,
//                 this.Image.Width + 200, 100
//             );

//         var hit3 = new RectangleF(
//                 (x) - width - 100, 
//                 y - height,
//                 100, this.Image.Height
//             );

//         var hit4 = new RectangleF(
//                 x - width - 100, 
//                 y - height - 100,
//                 this.Image.Width + 200, 100
//             );

//         this.Hitboxes.Add(hit1);
//         this.Hitboxes.Add(hit2);
//         this.Hitboxes.Add(hit3);
//         this.Hitboxes.Add(hit4);

//     }
    
//     public override void UpdateWalls()
//     {
//         throw new System.NotImplementedException();
//     }

//     public override void Draw(Graphics g)
//     {
//         throw new System.NotImplementedException();
//     }

//     public override void Draw(Graphics g, PictureBox pb)
//     {
//         float x = (pb.Width / 2) - this.Image.Width / 2;
//         float y = (pb.Height / 2) - this.Image.Height / 2;

//         g.DrawImage(this.Image, x, y);

//         foreach (var wall in Hitboxes)
//         {
//             g.DrawRectangle(Pens.White, wall);
//         }

//     }
// }
