using System.Drawing;
using System.Windows.Forms;

public class NextMapInteractable : Interactable
{
    public NextMapInteractable(string name, float x, float y, float width, float height) : base(name, x, y, width, height)
    {
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(
            MapManager.Map.PlayerSpawn.X,
            MapManager.Map.PlayerSpawn.Y,
            75, 75
        );
        g.DrawRectangle(Pens.Gold, this.Hitbox);
    }

    public override void Interact()
        => MapManager.NextMap();
    
}