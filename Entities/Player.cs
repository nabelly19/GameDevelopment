using System;
using System.Windows.Forms;
using System.Drawing;

public abstract class Player
{
    public int Vida         { get; private set; }
    public int Dano         { get; private set; }
    public State state    { get; set; }
    public int X            { get; set; }
    public int Y            { get; set; }
    public float Largura    { get; private set; }
    public float Altura     { get; private set; }

    private Image img;
    public Player(string path)
        => this.img = Bitmap.FromFile(path); //TODO MUDAR IMAGEM AQ PO
    
    public abstract void Atacar ();
    public abstract void ReceberDano();
}
