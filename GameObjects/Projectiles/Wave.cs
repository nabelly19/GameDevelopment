using System;
using System.Drawing;
using System.Windows.Forms;

public class Wave : Projectile
{
    public Wave(string name, int x, int y, string sprite, float direction, IAttackable owner)
        : base(name, x, y, sprite, direction, owner)
    {
    }

    public Wave(
        string name,
        float x,
        float y,
        float width,
        float height,
        float direction,
        IAttackable owner
    )
        : base(name, x, y, width, height, direction, owner)
    {;
    }
}
