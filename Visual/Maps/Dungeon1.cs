using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic;

public class Dungeon_01 : Map
{
    public Dungeon_01(PictureBox pb)
    {
        this.image = Resources.Current.Maps[0];
        CreateWalls(pb);
    }

    public override void CreateWalls(PictureBox pb)
    {
        float width = this.image.Width;
        float height = this.image.Height;
        float x = Screen.PrimaryScreen.Bounds.Width / 2;
        float y = Screen.PrimaryScreen.Bounds.Height / 2;

        var w1 = new Wall("Direita", x + width / 2, y, 100, height); // parede da direita
        var w2 = new Wall("Baixo", x, y + 0.8f * height / 2, width, 100); // parede de baixo
        var w3 = new Wall("Esquerda", x - 0.86f * width / 2, y, 100, height); // parede da esquerda
        var w4 = new Wall("Cima", x, y - 0.65f * height / 2, width, 100); // parede de cima
        var w5 = new Wall("Barril", x - 0.66f * width / 4, y - 0.49f * height / 2, 129, 90); // barril
        var v1 = new Wall("BarrilV", x - 0.66f * width / 4, y - 0.45f * height / 2, 90, 90); // barril V ( de pra cima tlgd )
        var w6 = new Wall("BarriSS", x + 0.623f * width / 2, y - 0.47f * height / 2, 220, 90); // barriSS
        var v2 = new Wall("BarriSS V1", x + 0.51f * width / 2, y - 0.45f * height / 2, 90, 90); // barriSS V1 ( de pra baixo tlgd )
        var v3 = new Wall("BarriSS V2", x + 0.75f * width / 2, y - 0.45f * height / 2, 70, 90); // barriSS

        // var market = new Market("Maercadin kkk", 700, 700);

        this.Walls.Add(w1);
        this.Walls.Add(w2);
        this.Walls.Add(w3);
        this.Walls.Add(w4);
        this.Walls.Add(w5);
        this.Walls.Add(v1);
        this.Walls.Add(w6);
        this.Walls.Add(v2);
        this.Walls.Add(v3);

        // this.Interactables.Add(market);

    }

    public override void Render(Graphics g, PictureBox pb)
    {
        g.DrawImage(
            this.image,
            (pb.Width / 2) - this.image.Width / 2,
            (pb.Height / 2) - this.image.Height / 2
        );

        foreach (var wall in Walls)
        {
            g.DrawRectangle(Pens.White, wall.Hitbox);
        }

        foreach (var item in Interactables)
        {
            // g.DrawRectangle(Pens.Gold, item.)
        }

    }
}
