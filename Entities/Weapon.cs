using System;
using System.Windows.Forms;
using System.Drawing;

public abstract class Arma 
{
    public string Nome { get; set; }
    public int Nivel { get; set; }
    public int Dano { get; set; }

    public virtual void Atirar() {}
    public virtual void Recarregar() {}
    public virtual void Melhorar() {}

}