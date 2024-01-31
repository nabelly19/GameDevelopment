using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public abstract class Interactable : GameObject
{
    public bool isInteractable { get; set; }

    protected Interactable
    (
        string name,
        float x,
        float y,
        string sprite
    ) : base(name, x, y, sprite) { DisableHitbox(); }

    protected Interactable
    (
        string name, 
        float x, 
        float y, 
        float width, 
        float height
    ) : base(name, x, y, width, height) { DisableHitbox(); }

    public override void Render(Graphics g, PictureBox pb)
    {
        g.FillRectangle(Brushes.Gold, this.Hitbox);
    }

    public abstract void Interact();

    public bool VerifyCollisions()
    {
        var collided = CollisionManager.GetCollisions(this).FirstOrDefault();
        if (collided is not null)
        {
            if (collided is Player p)
            {
                return true;
            }
        }
        return false;
    }

}
