using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public abstract class Map
{
    public int currentImgIndex {get;set;}

    public Image currentImage = null;

    public Image nextImage = null;

    public bool transitioning {get; set;}

    public int transitionStep {get; set;}
    public int timer {get; set;}
    public int transitionClock {get; set;}

    public int X {get; set;}

    public int Y {get; set;}
    public List<Wall> Walls { get; set; } = new ();

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
