using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public abstract class Map
{
    public PointF PlayerSpawn { get; set; }

    public List<Wall> Walls { get; set; } = new ();

    public Image image = null;

    public abstract void CreateWalls(PictureBox pb);

    public abstract void Render(Graphics g, PictureBox pb);
  
}
