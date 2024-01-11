using System;
using System.Windows.Forms;
using System.Drawing;

public class Chefe1
{
    public int X { get; set; }
    public int Y { get; set; }
    private Image img;
    private DateTime danoRecebido = DateTime.MinValue;
    public Chefe1()
    {

    }

    public void DanoRecebido()
    {
        danoRecebido = DateTime.Now;
    }
}