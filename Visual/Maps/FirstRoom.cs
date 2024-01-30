using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class FirstRoom : Map
{
    public override List<GameObject> GameObjects { get; set; }
    public FirstRoom(PictureBox pb)
    {
        GameObjects = new();
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
        var v1 = new Wall("BarrilV", x - 0.66f * width / 4, y - 0.45f * height / 2, 90, 90); // barril V ( de pra cima tlgd )
        var w6 = new Wall("BarriSS", x + 0.623f * width / 2, y - 0.47f * height / 2, 220, 90); // barriSS
        var v2 = new Wall("BarriSS V1", x + 0.51f * width / 2, y - 0.45f * height / 2, 90, 90); // barriSS V1 ( de pra baixo tlgd )
        var v3 = new Wall("BarriSS V2", x + 0.75f * width / 2, y - 0.45f * height / 2, 70, 90); // barriSS

        this.GameObjects.Add(w1);
        this.GameObjects.Add(w2);
        this.GameObjects.Add(w3);
        this.GameObjects.Add(w4);
        this.GameObjects.Add(w5);
        this.GameObjects.Add(v1);
        this.GameObjects.Add(w6);
        this.GameObjects.Add(v2);
        this.GameObjects.Add(v3);
    }

    public override void RenderBackground(Graphics g, PictureBox pb)
    {
        g.DrawImage(
            this.image,
            (pb.Width / 2) - this.image.Width / 2,
            (pb.Height / 2) - this.image.Height / 2
        );
    }
}
