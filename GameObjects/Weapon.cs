using System;
using System.Drawing;
using System.Windows.Forms;

// namespace Entity;

public abstract class Weapon : GameObject
{
    protected Weapon(string name, int x, int y, string sprite)
        : base(name, x, y, sprite) { }

    public void Attack() { }

    public int AtkSpeed { get; set; }
    public bool WindBlade { get; set; } = false;
}
