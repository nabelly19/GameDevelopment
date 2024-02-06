using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class LitchRoom : Map {
    //  public override Boss Boss { get; set; } = new FelixTheToad(960, 540);
    public override List<GameObject> GameObjects { get; set; } = new();
    public override CoinSystem CoinSystem { get; set; }
    public override System.Media.SoundPlayer song { get; set; }

    public LitchRoom(PictureBox pb)
    {
        this.image = Resources.Maps[6];
        this.PlayerSpawn = new PointF(
            Screen.PrimaryScreen.Bounds.Width / 2,
            0.9f * Screen.PrimaryScreen.Bounds.Height
        );
        InitializeMapObjects(pb);
    }

     public override void InitializeMapObjects(PictureBox pb)
    {
        float width = this.image.Width;
        float height = this.image.Height;
        float x = Screen.PrimaryScreen.Bounds.Width / 2;
        float y = Screen.PrimaryScreen.Bounds.Height / 2;

        var w1 = new Wall("Direita", x + 0.960f * width / 2, y, 50, height);
        var w2 = new Wall("Esquerda", x - 0.960f * width / 2, y, 50, height);
        var w3 = new Wall("Cimaw", x - 0.568f * width, y - 0.55f * height / 2, width * width/2, 50);
        var w4 = new Wall("Arvore Esquerda", x - 0.712f * width / 2, y + 0.407f * height / 2, 75, 90);
        var w5 = new Wall("Arvore Esquerda", x - 0.822f * width / 2, y + 0.450f * height / 2, 439, 90);
        var w6 = new Wall("Arvore Esquerda", x - 0.777f * width / 2, y + 0.480f * height / 2, 580, 90);
        var w7 = new Wall("Arvore Esquerda", x - 0.777f * width / 2, y + 0.525f * height / 2, 605, 90);
        var w8 = new Wall("Arvore Esquerda", x - 0.87f * width / 2, y + 0.428f * height / 2, 439, 90);
        var w9 = new Wall("Arvore Esquerda", x - 0.777f * width / 2, y + 0.525f * height / 2, 605, 90);
        var w10 = new Wall("Arvore Esquerda", x - 0.7400f * width / 2, y + 0.550f * height / 2, 605, 90);

        var w11 = new Wall("Arvore Direita", x + 0.822f * width / 2, y + 0.450f * height / 2, 439, 90);
        var w12 = new Wall("Arvore Direita", x + 0.777f * width / 2, y + 0.480f * height / 2, 580, 90);
        var w13 = new Wall("Arvore Direita", x + 0.777f * width / 2, y + 0.525f * height / 2, 605, 90);
        var w14 = new Wall("Arvore Direita", x + 0.87f * width / 2, y + 0.428f * height / 2, 439, 90);
        var w15 = new Wall("Arvore Direita", x + 0.712f * width / 2, y + 0.407f * height / 2, 75, 90);
        var w16 = new Wall("Arvore Direita", x + 0.777f * width / 2, y + 0.525f * height / 2, 605, 90);
        var w17  = new Wall("Arvore Direita", x + 0.7400f * width / 2, y + 0.550f * height / 2, 605, 90);

        this.GameObjects.Add(w1);
        this.GameObjects.Add(w2);
        this.GameObjects.Add(w3);
        this.GameObjects.Add(w4);
        this.GameObjects.Add(w5);
        this.GameObjects.Add(w6);
        this.GameObjects.Add(w7);
        this.GameObjects.Add(w8);
        this.GameObjects.Add(w9);
        this.GameObjects.Add(w10);

        this.GameObjects.Add(w11);
        this.GameObjects.Add(w12);
        this.GameObjects.Add(w13);
        this.GameObjects.Add(w14);
        this.GameObjects.Add(w15);
        this.GameObjects.Add(w16);
        this.GameObjects.Add(w17);
 
        this.GameObjects.Add(new Coin("Moeda", 900, 700));
       
    }

    public override void RenderBackground(Graphics g, PictureBox pb)
    {
        g.DrawImage(
            this.image,
            (pb.Width / 2) - this.image.Width / 2,
            (pb.Height / 2) - this.image.Height / 2.65f
        );

        foreach (var wall in GameObjects)
        {
            g.DrawRectangle(Pens.White, wall.Hitbox);

        }
    }

       public override void UpdateBackground()
    {
    }
}