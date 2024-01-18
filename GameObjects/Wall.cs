// namespace Entity;

using System.Drawing;
using System.Windows.Forms;

public class Wall : GameObject
{
    public Wall(string name, int x, int y, float width, float height)
        : base(name, x, y, width, height) { }

    // test Render
    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, 100, 100);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }
}
