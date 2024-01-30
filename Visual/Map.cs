using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public abstract class Map
{
    public PointF PlayerSpawn { get; set; }
    public List<Wall> Walls { get; set; } = new ();
    public virtual Boss Boss { get; set;} = null;
    public abstract List<GameObject> GameObjects{ get; set; } 

    public Image image = null;
    public abstract void CreateWalls(PictureBox pb);

    public abstract void Render(Graphics g, PictureBox pb);
    public abstract Map New(PictureBox pb);
}
