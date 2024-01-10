using System.Drawing;
using System.Windows.Forms;

public class Background
{
    public PointF SpawnPoint;
    public float Size;
    private Image img;

    public Background ()
        => this.img = Bitmap.FromFile("../../../Media./imgs./bggif.gif"); //TODO ImageAnimator.Image

    public void Draw (Graphics g, PictureBox pb)
    {
        SpawnPoint.X = (pb.Width / 2) - img.Width/2;
        SpawnPoint.Y = (pb.Height / 2) - img.Height/2;
        Size = 626;
        
        g.DrawImage(img,
            SpawnPoint);

    }

}