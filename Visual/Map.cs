using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public abstract class Map
{
    public List<Wall> Walls { get; set; } = new();
    public Image image = null;

    // public Map(string path, PictureBox pb)
    //     {
    //         this.image = Bitmap.FromFile(path);
    //         CreateWalls(pb);
    //     }

    // public Map(Image image, PictureBox pb)
    //     {
    //         this.image = image;
    //         CreateWalls(pb);
    //     }


    public abstract void CreateWalls(PictureBox pb);

    public abstract void Render(Graphics g, PictureBox pb);
    // public void setImage(string path)
    // {

    // }
    // public void setImage(Image img)
    // {

    // }
}
