// using System.Collections.Generic;
// using System.Drawing;
// using System.Windows.Forms;

// public abstract class Map : Entity
// {
//     protected Map(string path) : base(path) { 
//         this.Width = this.Image.Width;
//         this.Height = this.Image.Height;
//     }
//     public List<RectangleF> Hitboxes { get; set; } = new List<RectangleF>();

//     public abstract void CreateWalls(PictureBox pb);
//     public virtual void UpdateBackground(string path)
//         => ChangeImage(path);

//     public abstract void UpdateWalls();
//     public abstract void Draw(Graphics g, PictureBox pb);
// }
