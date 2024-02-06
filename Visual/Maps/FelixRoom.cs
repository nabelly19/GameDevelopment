using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class FelixRoom : Map
{
    public override Boss Boss { get; set; } = new FelixTheToad(960, 540);
    public override List<GameObject> GameObjects { get; set; } = new();
    public override CoinSystem CoinSystem { get; set; }

    public FelixRoom(PictureBox pb)
    {
        this.image = Resources.Maps[2];
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

        this.CoinSystem = new CoinSystem
        (
            2 * x, 
            2 * y,
            5, 2
        );


        var w1 = new Wall("Direita", x + 0.965f * width / 2, y, 50, height);
        var w2 = new Wall("Esquerda", x - 0.965f * width / 2, y, 50, height);
        var w3 = new Wall("CimaEsquerda", x - 0.568f * width, y - 0.45f * height / 2, width, 50);
        var w4 = new Wall("CimaDireita", x + 0.568f * width, y - 0.45f * height / 2, width, 50);
        var w5 = new Wall("Portao", x, y - 447.5f, width, 50);
        var w6 = new Wall("ParedeEsquedaPortao", 0.865f * x, y - 0.55f * height / 2, 50, 190);
        var w7 = new Wall("ParedeDireitaPortao", 1.135f * x, y - 0.55f * height / 2, 50, 190);
        var w8 = new Wall("LavaEsquerda", x - 0.87f * width / 2, y - 0.375f * height / 2, 110, 90);
        var w9 = new Wall("LavaDireita", x + 0.87f * width / 2, y - 0.375f * height / 2, 110, 90);
        var b = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Random.Shared.Next(90, 179),
            Boss
        );

        this.GameObjects.Add(w1);
        this.GameObjects.Add(w2);
        this.GameObjects.Add(w3);
        this.GameObjects.Add(w4);
        this.GameObjects.Add(w5);
        this.GameObjects.Add(w6);
        this.GameObjects.Add(w7);
        this.GameObjects.Add(w8);
        this.GameObjects.Add(w9);
        this.GameObjects.Add(Boss);
        this.GameObjects.Add(b);
    }

    public override void RenderBackground(Graphics g, PictureBox pb)
    {
        g.DrawString($"Map Coins: {CoinSystem.Count}", SystemFonts.DefaultFont, Brushes.White, 10, 150);

        g.DrawImage(
            this.image,
            (pb.Width / 2) - this.image.Width / 2,
            (pb.Height / 2) - this.image.Height / 2.65f
        );
        // AddRandomCoin();
    }

    public override void UpdateBackground()
    {
        this.CoinSystem.Act();
    }
    protected override void AddRandomCoin()
    {
        var query = from obj in CollisionManager.GameObjects where obj is Coin select obj;
        if (query.FirstOrDefault() is null)
            CollisionManager.AddGameObject(
                new Coin(
                    "Moeda",
                    Random.Shared.Next(
                        (int)(Screen.PrimaryScreen.Bounds.Width * 0.13f),
                        (int)(
                            Screen.PrimaryScreen.Bounds.Width
                            - Screen.PrimaryScreen.Bounds.Width * 0.13f
                        )
                    ),
                    Random.Shared.Next(
                        (int)(Screen.PrimaryScreen.Bounds.Height * 0.297f),
                        (int)(
                            Screen.PrimaryScreen.Bounds.Height
                            - Screen.PrimaryScreen.Bounds.Height * 0.297f
                        )
                    )
                )
            );
    }

}
