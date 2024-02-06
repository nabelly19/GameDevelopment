using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic;

public class Test_Dungeon01 : Map
{
    public override List<GameObject> GameObjects { get; set; } = new();
    public override CoinSystem CoinSystem { get; set; }

    public Test_Dungeon01(PictureBox pb)
    {
        this.image = Resources.Maps[0];
        this.PlayerSpawn = new PointF(
          (Screen.PrimaryScreen.Bounds.Width / 2) - 0.60f * this.image.Width / 2,
          Screen.PrimaryScreen.Bounds.Height / 2
          );
        InitializeMapObjects(pb);
    }


    public override void InitializeMapObjects(PictureBox pb)
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

        var i1 = new PrevMapInteractable("Indo Ali", 0, 0, 0, 0);

        // var market = new Market("Maercadin kkk", 700, 700);

        this.GameObjects.Add(i1);

        this.GameObjects.Add(w1);
        this.GameObjects.Add(w2);
        this.GameObjects.Add(w3);
        this.GameObjects.Add(w4);
        this.GameObjects.Add(w5);

    }

    public override void RenderBackground(Graphics g, PictureBox pb)
    {
        g.DrawImage(
            this.image,
            (pb.Width / 2) - this.image.Width / 2, (pb.Height / 2) - this.image.Height / 2
        );
        foreach (var item in this.GameObjects)
        {
            if (item is Interactable pog)
                g.FillRectangle(Brushes.Gold, item.Hitbox);

        }
    }

    public override void UpdateBackground()
    {
        throw new System.NotImplementedException();
    }
}
