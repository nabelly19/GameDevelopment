using System;
using System.Windows.Forms;
using System.Drawing;

public abstract class Chefe
{
    public int Vida         { get; private set; }
    public int Dano         { get; private set; }
    public EstadoChefe estado    { get; set; }
    public int X            { get; set; }
    public int Y            { get; set; }
    public float Largura    { get; private set; }
    public float Altura     { get; private set; }

    private Image img;
    public Chefe()
        => this.img = Bitmap.FromFile("PATH"); //TODO MUDAR IMAGEM AQ PO
    
    public abstract void Atacar ();
    public abstract void ReceberDano();
}

public abstract class EstadoChefe
{
    public Chefe chefe; // contexto
    public EstadoChefe proxEstado { get; set; }
    public abstract void Agir();
}