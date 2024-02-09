using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class LitchRoom : Map
{
    public override Boss Boss { get; set; } = new LichTheHottes(ClientScreen.ResponsiveX(960), ClientScreen.ResponsiveY(540));
    public override List<GameObject> GameObjects { get; set; } = new();
    public override CoinSystem CoinSystem { get; set; }
    public override System.Media.SoundPlayer song { get; set; }

    public LitchRoom()
    {
        this.image = Resources.Maps[6];
        this.PlayerSpawn = new PointF(
            Screen.PrimaryScreen.Bounds.Width / 2,
            0.9f * Screen.PrimaryScreen.Bounds.Height
        );
        this.song = new("assets/songs/LichTheme.wav");
        InitializeMapObjects();
    }

    public override void InitializeMapObjects()
    {
        float width = ClientScreen.ResponsiveX(this.image.Width);
        float height = ClientScreen.ResponsiveY(this.image.Height);
        float x = ClientScreen.Width / 2;
        float y = ClientScreen.Height / 2;

        this.CoinSystem = new CoinSystem
        (
            2 * x * 0.13f,
            2 * x - 2 * x * 0.13f,
            2 * y * 0.297f,
            2 * y - 2 * y * 0.297f,
            5, 2
        );

        var w1 = new Wall("Direita", x + 0.960f * width / 2, y, 50, height);
        var w2 = new Wall("Esquerda", x - 0.960f * width / 2, y, 50, height);
        var w3 = new Wall("Cimaw", x - 0.568f * width, y - 0.55f * height / 2, width * width / 2, 50);
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
        var w17 = new Wall("Arvore Direita", x + 0.7400f * width / 2, y + 0.550f * height / 2, 605, 90);

        var b1_0 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            0,
            Boss
        );

        var b1_1 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            355,
            Boss
        );
        var b1_2 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            5,
            Boss
        );
        var b1_3 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            350,
            Boss
        );
        var b1_4 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            10,
            Boss
        );
        var b1_5 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            345,
            Boss
        );


        var b2_0 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            120,
            Boss
        );
        var b2_1 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            115,
            Boss
        );var b2_2 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            125,
            Boss
        );var b2_3 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            110,
            Boss
        );var b2_4 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            130,
            Boss
        );var b2_5 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            105,
            Boss
        );

        var b3_0 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            240,
            Boss
        );
        var b3_1 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            235,
            Boss
        );
        var b3_2 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            245,
            Boss
        );var b3_3 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            230,
            Boss
        );var b3_4 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            250,
            Boss
        );var b3_5 = new WallMoveable(
            "Bullet",
            Boss.X - 14,
            Boss.Y - 50,
            Resources.RotateWall,
            225,
            Boss
        );

        AddObjects(
            w1, w2, w3, w4, w5, w6, w7, w8, w9, w10,
            w11, w12, w13, w14, w15, w16, w17,

            b1_0, b1_1, b1_2, b1_3, b1_4, b1_5,
            b2_0, b2_1, b2_2, b2_3, b2_4, b2_5,
            b3_0, b3_1, b3_2, b3_3, b3_4, b3_5
        );
        this.Boss = new LichTheHottes(ClientScreen.ResponsiveX(960), ClientScreen.ResponsiveY(540), b1_0, b2_0, b3_0);
        this.GameObjects.Add(this.Boss);

    }

    public override void RenderBackground(Graphics g, PictureBox pb)
    {
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
    }
}