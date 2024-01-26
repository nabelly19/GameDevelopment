using System.Drawing;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic;

public class Dungeon_02 : Map
{

    public Dungeon_02(PictureBox pb)
    {
        this.image = Resources.Current.Maps[1];
        CreateWalls(pb);  
    }
    public override void CreateWalls(PictureBox pb)
    {   
        float width = this.image.Width;
        float height = this.image.Height ;
        float x = Screen.PrimaryScreen.Bounds.Width / 2;
        float y = Screen.PrimaryScreen.Bounds.Height / 2;

        var w1 = new Wall("Direita",x + 7.45f * 100, y, 50,height); 
        var w2 = new Wall("Esquerda",x - 7.45f * 100, y, 50,height); 
        var w3 = new Wall("CimaEsquerda",x - 9 * 100, y - 325, width, 50); 
        var w4 = new Wall("CimaDireita", x + 9 * 100, y - 325, width, 50);
        var w5 = new Wall("Portao", x, y - 447.5f, width, 50);
        var w6 = new Wall("ParedeEsquedaPortao", x - 131.5f, y - 395, 50, 190);
        var w7 = new Wall("ParedeDireitaPortao", x + 131.5f, y - 395, 50, 190);
        var w8 = new Wall("LavaEsquerda", x - 6.85f * 100, y - 270, 110, 90);
        var w9 = new Wall("LavaDireita", x + 6.79f * 100, y - 270, 110, 90);


        this.Walls.Add(w1);
        this.Walls.Add(w2);
        this.Walls.Add(w3);
        this.Walls.Add(w4);    
        this.Walls.Add(w5);
        this.Walls.Add(w6);
        this.Walls.Add(w7);
        this.Walls.Add(w8);
        this.Walls.Add(w9);

        }

    public override void Render(Graphics g, PictureBox pb)
    {
        g.DrawImage(
            this.image,
            (pb.Width / 2 ) - this.image.Width / 2, (pb.Height / 2) - this.image.Height / 2.65f
        );

        // foreach (var wall in Walls)
        // {
        //     g.DrawRectangle(Pens.White, wall.CreateHitbox());

        // }
    }
}
