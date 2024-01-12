using System.Drawing;

/* The abstract class Entity represents an entity with an image that can be drawn on a graphics object. */
public abstract class Entity
{
    public Image Image  { get; private set; }
    public int Width    { get; private set; }
    public int Height   { get; private set; }
    
    public Entity(string path)
        => this.Image = Bitmap.FromFile(path);

    public abstract void Draw(Graphics g);
}