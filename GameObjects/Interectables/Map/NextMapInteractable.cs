using System.Drawing;
using System.Windows.Forms;

public class NextMapInteractable : Interactable
{
    public bool Auto { get; set; }
    public NextMapInteractable(string name, float x, float y, float width, float height) : base(name, x, y, width, height)
    {
        CreateHitbox(
            x, y,
            width, height
        );
        this.isInteractable = true;
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        VerifyPlayer();
        // g.DrawRectangle(Pens.Gold, this.Hitbox);
    }

    public override void Interact()
        => MapManager.NextMap();

    private void VerifyPlayer()
    {
        if (this.Interacted)
            return;

        if (Auto)
        {
            if (VerifyCollisions())
            {
                this.Interacted = true;
                Interact();
            }


        }
    }

}