using System.Drawing;
using System.Windows.Forms;

// namespace Entity;

public abstract class GameObject
{
    public string Name { get; set; }
    public RectangleF Hitbox { get; set; }
    public bool isHittable { get; private set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Old_X { get; set; }
    public float Old_Y { get; set; }
    public Image Sprite { get; set; }

    public GameObject(string name, int x, int y, string sprite)
    {
        this.Name = name;
        this.X = x;
        this.Y = y;
        setImage(sprite);
        this.Width = this.Sprite.Width;
        this.Height = this.Sprite.Height;
    }

    public virtual void Update() { }

    public virtual void Render(Graphics g, PictureBox pb) { }

    public void setImage(string path)
    {
        this.Sprite = Bitmap.FromFile(path);
    }

    public virtual void CreateHitbox(float x, float y, float width, float height)
    {
        this.Hitbox = new RectangleF(x - (width / 2), y - (height / 2), width, height);
    }

    public virtual void EnableHitbox() => this.isHittable = true;

    public virtual void DisableHitbox() => this.isHittable = false;
}
