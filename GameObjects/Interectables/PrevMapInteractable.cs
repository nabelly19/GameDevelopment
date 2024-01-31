using System.Drawing;
using System.Windows.Forms;

public class PrevMapInteractable : Interactable
{
    public PrevMapInteractable(string name, float x, float y, float width, float height) : base(name, x, y, width, height)
    {
        CreateHitbox(
            x, y,
            width, height
        );
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        g.DrawRectangle(Pens.Gold, this.Hitbox);
    }

    public override void Interact()
        => MapManager.PreviousMap();
    
}