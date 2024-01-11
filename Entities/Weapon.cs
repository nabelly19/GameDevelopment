using System;
using System.Windows.Forms;
using System.Drawing;

public abstract class Weapon : Hittable
{
    protected Weapon(string path) : base(path) { }

    public string Name      { get; set; }
    public int AtkSpeed     { get; set; }
    public bool WindBlade   { get; set; } = false;

}