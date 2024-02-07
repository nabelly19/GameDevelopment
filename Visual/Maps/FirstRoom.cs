using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class FirstRoom : Map
{
    public override List<GameObject> GameObjects { get; set; }
    public override CoinSystem CoinSystem { get; set; }
    public override System.Media.SoundPlayer song { get; set; }

    public FirstRoom()
    {
        GameObjects = new();
        this.image = Resources.Maps[0];
        this.PlayerSpawn = new PointF(
            (Screen.PrimaryScreen.Bounds.Width / 2) - 0.60f * this.image.Width * ClientScreenSize.WidthFactor / 2,
            Screen.PrimaryScreen.Bounds.Height / 2
            );
        this.song = new("../../../assets/songs/Haunt.wav");
        InitializeMapObjects();
    }

    public override void InitializeMapObjects()
    {
        float width = this.image.Width * ClientScreenSize.WidthFactor;
        float height = this.image.Height * ClientScreenSize.WidthFactor;
        float x = Screen.PrimaryScreen.Bounds.Width / 2;
        float y = Screen.PrimaryScreen.Bounds.Height / 2;

        var w1 = new Wall("Direita", x + width / 2, y, 100 * ClientScreenSize.WidthFactor, height); // parede da direita
        var w2 = new Wall("Baixo", x, y + 0.8f * height / 2, width, 100 * ClientScreenSize.WidthFactor); // parede de baixo
        var w3 = new Wall("Esquerda", x - 0.86f * width / 2, y, 100 * ClientScreenSize.WidthFactor, height); // parede da esquerda
        var w4 = new Wall("Cima", x, y - 0.65f * height / 2, width, 100 * ClientScreenSize.WidthFactor); // parede de cima
        var w5 = new Wall("Barril", x - 0.66f * width / 4, y - 0.52f * height / 2, 129 * ClientScreenSize.WidthFactor, 40 * ClientScreenSize.WidthFactor); // barril
        var v1 = new Wall("BarrilV", x + 0.20f * width / 2, y - 0.42f * height / 2, 129 * ClientScreenSize.WidthFactor, 90 * ClientScreenSize.WidthFactor); // barril V ( de pra cima tlgd )
        var w6 = new Wall("BarriSS", x + 0.623f * width / 2, y - 0.47f * height / 2, 220 * ClientScreenSize.WidthFactor, 90 * ClientScreenSize.WidthFactor); // barriSS
        var v2 = new Wall("BarriSS V1", x + 0.05f * width / 2, y - 0.36f * height / 2, 100 * ClientScreenSize.WidthFactor, 50 * ClientScreenSize.WidthFactor); // barriSS V1 ( de pra baixo tlgd )
        var v3 = new Wall("BarriSS V2", x + 0.75f * width / 2, y - 0.45f * height / 2, 70 * ClientScreenSize.WidthFactor, 90 * ClientScreenSize.WidthFactor); // barriSS

        var i1 = new NextMapInteractable("Indo Ali",
        PlayerSpawn.X, PlayerSpawn.Y,
            75, 75);

        var i2 = new Market("Coé",
        x + 0.32f * width / 2, y - 0.20f * height / 2, 200 * ClientScreenSize.WidthFactor, 90 * ClientScreenSize.WidthFactor);

        // var market = new Market("Maercadin kkk", 700, 700);

        // this.GameObjects.Add(i1);
        this.GameObjects.Add(i2);
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
            (pb.Width / 2) - this.image.Width * ClientScreenSize.WidthFactor / 2,
            (pb.Height / 2) - this.image.Height * ClientScreenSize.WidthFactor / 2,
            this.image.Width * ClientScreenSize.WidthFactor,
            this.image.Height * ClientScreenSize.WidthFactor
        );

        foreach (var item in this.GameObjects)
        {
            // if (item is Interactable pog)
            // g.DrawRectangle(Pens.Gold, item.Hitbox);
        }

        foreach (var wall in GameObjects)
        {
            g.DrawRectangle(Pens.White, wall.Hitbox);
        }
    }

    public override void UpdateBackground()
    {
    }
}