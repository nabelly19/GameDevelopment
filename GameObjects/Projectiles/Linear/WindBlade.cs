using System.Linq;
using System.Windows.Forms;

public class WindBlade : Projectile
{
    public WindBlade(string name, int x, int y, string sprite, float direction, IAttackable owner)
        : base(name, x, y, sprite, direction, owner) { }

    public WindBlade(
        string name,
        float x,
        float y,
        float width,
        float height,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, width, height, direction, owner) { }

    public override void Update()
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        Move();
        var collided = CollisionManager.GetCollisions(this).FirstOrDefault();
        if (collided is not null)
        {
            if (collided == Owner)
                return;
            if (collided is IAttackable other)
            {
                if (other.isVulnerable)
                    other.ReceiveDamage();
            }
            CollisionManager.RemoveGameObject(this);
        }
        if (CollisionManager.ScreenColision(this))
            CollisionManager.RemoveGameObject(this);
    }
}
