using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;
using System.Windows.Forms;

public class Resources
{
    private static Resources crr;
    public static Resources Current => crr;

    private Resources()
    {

        this.PlayerSprites = Directory
            // .GetFiles("./assets/Sprites/Player/NewSprite/", "*.png")
            .GetFiles("../../../assets/Sprites/Player/NewSprite/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();
        this.Maps = Directory
            // .GetFiles("./assets/Maps/", "*.png")
            .GetFiles("../../../assets/Maps/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();
        this.Coins = Directory
            // .GetFiles("./assets/Sprites/Coin/", "*.png")
            .GetFiles("../../../assets/Sprites/Coin/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();
        // this.Maps.Add(Bitmap.FromFile("../../../assets/Maps/PRIMEIROCENARIO.png"));
    }

    public List<Bitmap> PlayerSprites = new();
    public List<Bitmap> Maps = new();
    public List<Bitmap> Coins = new();
    public static void New() => crr = new Resources();
}
