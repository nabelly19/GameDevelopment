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
            (ClientScreen.Width / 2) - 0.60f * ClientScreen.ResponsiveX(this.image.Width) / 2,
            ClientScreen.Height / 2
            );
        this.song = new ("assets/songs/Haunt.wav");
        InitializeMapObjects();
    }

    public override void InitializeMapObjects()
    {
        float width = ClientScreen.ResponsiveY(this.image.Width);
        float height = ClientScreen.ResponsiveY(this.image.Height);
        float x = ClientScreen.Width / 2;
        float y = ClientScreen.Height / 2;

        var w1 = new Wall("Direita", x + width / 2, y, 100, height); // parede da direita
        var w2 = new Wall("Baixo", x, y + 0.8f * height / 2, width, 100); // parede de baixo
        var w3 = new Wall("Esquerda", x - 0.86f * width / 2, y, 100, height); // parede da esquerda
        var w4 = new Wall("Cima", x, y - 0.65f * height / 2, width, 100); // parede de cima
        var w5 = new Wall("Barril", x - 0.66f * width / 4, y - 0.52f * height / 2, 129, 40); // barril
        var v1 = new Wall("BarrilV", x + 0.20f * width / 2, y - 0.42f * height / 2, 129, 90); // barril V ( de pra cima tlgd )
        var w6 = new Wall("BarriSS", x + 0.623f * width / 2, y - 0.47f * height / 2, 220, 90); // barriSS
        var v2 = new Wall("BarriSS V1", x + 0.05f * width / 2, y - 0.36f * height / 2, 100, 50); // barriSS V1 ( de pra baixo tlgd )
        var v3 = new Wall("BarriSS V2", x + 0.75f * width / 2, y - 0.45f * height / 2, 70, 90); // barriSS

        var i1 = new NextMapInteractable("Indo Ali", 
        x + 0.45f * width, y,
            100, 0.45f * height);
            i1.Auto = true;

        var i2 = new Market("Coé", 
        x + 0.32f * width / 2, y - 0.20f * height / 2, 200, 90);

        this.GameObjects.Add(i1);
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
            (ClientScreen.Width / 2) - ClientScreen.ResponsiveY(this.image.Width) / 2,
            (ClientScreen.Height / 2) - ClientScreen.ResponsiveY(this.image.Height) / 2,
            ClientScreen.ResponsiveY(this.image.Width),
            ClientScreen.ResponsiveY(this.image.Height)
        );

        // foreach (var wall in GameObjects)
        // {
        //     g.DrawRectangle(Pens.White, wall.Hitbox);

        // // }
        // foreach (var item in this.GameObjects)
        // {
        //     if (item is Interactable pog)
        //         g.DrawRectangle(Pens.Gold, item.Hitbox);
        // }

    }

    public override void UpdateBackground()
    {
    }

    public override void ResetInteractables()
    {
       foreach (var item in this.GameObjects)
        {
            if (item is Interactable pog)
                pog.Interacted = false;
        } 
    }
}