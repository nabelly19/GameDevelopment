using System.Drawing;
using System.Windows.Forms;

// namespace Entity;

public abstract class GameObject
{
    public string Name { get; set; }
    public RectangleF Hitbox { get; set; }
    public bool isHittable { get; private set; } = true;
    public PointF Location { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public Image Sprite { get; set; }

    public GameObject(string name, float x, float y, string sprite)
    {
        this.Name = name;
        this.X = x;
        this.Y = y;
        setImage(sprite);
        this.Width = this.Sprite.Width;
        this.Height = this.Sprite.Height;
    }

    public GameObject(string name, float x, float y, Image image)
    {
        this.Name = name;
        this.X = x;
        this.Y = y;
        this.Sprite = image;
        this.Width = this.Sprite.Width;
        this.Height = this.Sprite.Height;
    }

    public GameObject(string name, float x, float y, float width, float height)
    {
        this.Name = name;
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }

    public virtual void Update() { }

    public virtual void Render(Graphics g, PictureBox pb) { }

    public void setImage(string path)
        => this.Sprite = Bitmap.FromFile(path);

    public virtual void CreateHitbox(float x, float y, float width, float height)
        => this.Hitbox = new RectangleF(x - (width / 2), y - (height / 2), width, height);
    public virtual void EnableHitbox() => this.isHittable = true;

    public virtual void DisableHitbox() => this.isHittable = false;
}