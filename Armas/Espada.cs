using System;
using System.Windows.Forms;
using System.Drawing;

public class Espada : Arma
{

    public float X { get; set; }
    public float Y { get; set; }
    public float Tamanho { get; set; }
    private Image img;
      public Espada()
        {
            this.Nivel = 1;
            this.Dano = 2;
            this.Nome = "Espada do Herói";
            this.img = Bitmap.FromFile("Sprites/Espada/Wooden_sword.png");
            X = 100;
            Y = 100;
        }

    public override void Melhorar()
    {
        base.Melhorar();
    }

}