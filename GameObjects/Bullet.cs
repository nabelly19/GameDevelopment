// namespace Entity;

using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Bullet : GameObject
{
    public Bullet(string name, int x, int y, float width, float height)
        : base(name, x, y, width, height)
    {
        DisableHitbox();
    }

    public override void Update()
    {
        Move();
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }

    public void Move()
    {
        // Lógica de movimento da bala
        X += 1;
        Y += 1;
        var collided = CollisionManager.Current.GetCollisions(this).FirstOrDefault();
        if (CollisionManager.Current.CheckCollisions(this))
        {
            if (collided is IAttackable other)
                other.ReceiveDamage();
            CollisionManager.Current.RemoveGameObject(this);
        }
    }
}
