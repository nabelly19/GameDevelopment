using System;
using System.Drawing;
using System.Windows.Forms;

public abstract class Weapon : Hittable
{
    protected Weapon(string path, int width, int height)
        : base(path, width, height) { }

    public string Name { get; set; }
    public int AtkSpeed { get; set; }
    public bool WindBlade { get; set; } = false;
}
