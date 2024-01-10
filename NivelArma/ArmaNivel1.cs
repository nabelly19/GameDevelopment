using System;
using System.Windows.Forms;
using System.Drawing;

public class ArmaNivel1 : Arma
{

    public float X { get; set; }
    public float Y { get; set; }

    private Image img;
      public ArmaNivel1()
    {
        this.Nivel = 1;
        this.Ataque = 2;
        this.Nome = "Cepo de Madeira";
        this.img = Bitmap.FromFile("Wooden_sword.png");
         X = 100;
         Y = 100;
        
    }

    public override void Melhorar()
    {
        base.Melhorar();
    }

}