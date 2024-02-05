using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public abstract class Map
{
    public PointF PlayerSpawn { get; set; }
    public virtual Boss Boss { get; set; } = null;
    public abstract List<GameObject> GameObjects { get; set; }
    public virtual void SetBackground(Image img) {this.image = img;}
    public virtual void SetBackground(Bitmap img) {this.image = img;}
    protected Image image = null;
    public abstract void InitializeMapObjects(PictureBox pb);
    public abstract void RenderBackground(Graphics g, PictureBox pb);
}
