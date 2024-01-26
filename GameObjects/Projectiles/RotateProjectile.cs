using System;
using System.Drawing;

public abstract class RotateProjectile : Projectile
{
    protected RotateProjectile(
        string name,
        int x,
        int y,
        string sprite,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, sprite, direction, owner) { }

    protected RotateProjectile(
        string name,
        float x,
        float y,
        float width,
        float height,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, width, height, direction, owner) { }

    public PointF center;
    public float radius;

    public abstract void RotatePoints();
}
