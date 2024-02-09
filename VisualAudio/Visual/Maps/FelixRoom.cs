using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class FelixRoom : Map
{
    public override Boss Boss { get; set; } =
        new FelixTheToad(ClientScreen.ResponsiveX(960), ClientScreen.ResponsiveY(540));
    public override List<GameObject> GameObjects { get; set; } = new();
    public override CoinSystem CoinSystem { get; set; }
    public override System.Media.SoundPlayer song { get; set; }
    private Wall removable;
    private Interactable addable;
    public FelixRoom()
    {
        this.image = Resources.Maps[1];
        this.PlayerSpawn = new PointF(
            ClientScreen.Width / 2,
            0.9f * ClientScreen.Height
        );
        this.song = new("assets/songs/FelixTheme.wav");
        InitializeMapObjects();
    }

    public override void InitializeMapObjects()
    {
        float width = ClientScreen.ResponsiveX(this.image.Width);
        float height = ClientScreen.ResponsiveY(this.image.Height);
        float x = ClientScreen.Width / 2;
        float y = ClientScreen.Height / 2;

        this.CoinSystem = new CoinSystem(
            x - 0.87f * width / 2,
            x + 0.87f * width / 2,
            y - 0.375f * height / 2,
            2 * y,
            5,
            2
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

        var wV = new Wall("Removivel", x, y - 0.45f * height / 2, width, 50);
        this.removable = wV;

        var i1 = new NextMapInteractable("Indo Ali",
                x, y - 0.45f * height / 2,
                100, 100);
        i1.Auto = true;
        this.addable = i1;

        this.GameObjects.Add(w1);
        this.GameObjects.Add(w2);
        this.GameObjects.Add(w3);
        this.GameObjects.Add(w4);
        this.GameObjects.Add(w5);
        this.GameObjects.Add(w6);
        this.GameObjects.Add(w7);
        this.GameObjects.Add(w8);
        this.GameObjects.Add(w9);
        this.GameObjects.Add(wV);
        this.GameObjects.Add(Boss);
    }

    public override void RenderBackground(Graphics g, PictureBox pb)
    {
        // g.DrawString(
        //     $"Map Coins: {CoinSystem.Count}",
        //     SystemFonts.DefaultFont,
        //     Brushes.White,
        //     10,
        //     150
        // );

        g.DrawImage(
            this.image,
            (ClientScreen.Width / 2) - ClientScreen.ResponsiveX(this.image.Width) / 2,
            (ClientScreen.Height / 2) - ClientScreen.ResponsiveY(this.image.Height) / 2.65f,
            ClientScreen.ResponsiveX(this.image.Width),
            ClientScreen.ResponsiveY(this.image.Height)
        );
        // foreach (var wall in GameObjects)
        // {
        //     g.DrawRectangle(Pens.White, wall.Hitbox);
        // }
    }

    public override void UpdateBackground()
    {
        this.CoinSystem.Act();
        if (!this.Boss.isAlive)
        {
           CollisionManager.RemoveGameObject(this.removable);
            CollisionManager.AddGameObject(this.addable);
        }
            
    }
}
