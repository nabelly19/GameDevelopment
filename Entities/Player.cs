using System;
using System.Windows.Forms;
using System.Drawing;

public abstract class Player : Hittable
{
    public int HP           { get; set; }
    public Weapon weapon    { get; set; }
    public float CritChance { get; set; }
    public float BlockChance{ get; set; }
    public int Velocity     { get; set; }
    protected Player(string path) : base(path) { }


    public abstract void Attack ();
    public virtual void ReceiveDamage()
        => this.HP--;
}
