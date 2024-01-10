using System;
using System.Windows.Forms;
using System.Drawing;

public abstract class Jogador
{
    public int Vida         { get; private set; }
    public int Dano         { get; private set; }
    public EstadoJogador estado    { get; set; }
    public int X            { get; set; }
    public int Y            { get; set; }
    public float Largura    { get; private set; }
    public float Altura     { get; private set; }

    private Image img;
    public Jogador()
        => this.img = Bitmap.FromFile("PATH"); //TODO MUDAR IMAGEM AQ PO
    
    public abstract void Atacar ();
    public abstract void ReceberDano();
}

// Necessario?? 
public abstract class EstadoJogador
{
    public Jogador jogador; // contexto
    public EstadoJogador proxEstado { get; set; }
    public abstract void Agir();
}