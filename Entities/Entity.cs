using System.Drawing;

public abstract class Entity
{
    public Image Image  { get; private set; }
    public int Width    { get; private set; }
    public int Height   { get; private set; }
    
    public Entity(string path)
        => this.Image = Bitmap.FromFile(path);

    public abstract void Draw(Graphics g);
}