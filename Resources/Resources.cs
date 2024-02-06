using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;
using System.Windows.Forms;

public static class Resources
{
    public static void New()
    { 
        PlayerSprites = Directory
            // .GetFiles("./assets/Sprites/Player/NewSprite/", "*.png")
            .GetFiles("../../../assets/Sprites/Player/NewSprite/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();
        Maps = Directory
            // .GetFiles("./assets/Maps/", "*.png")
            .GetFiles("../../../assets/Maps/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();
        Coins = Directory
            // .GetFiles("./assets/Sprites/Coin/", "*.png")
            .GetFiles("../../../assets/Sprites/Coin/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();
        Market = Directory 
            .GetFiles("../../../assets/Sprites/Market/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();
        Cards = Directory  
            .GetFiles("../../../assets/Sprites/Card/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();
        Felix = Directory
            .GetFiles("../../../assets/Sprites/Bosses/Felix", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();

    }

    public static List<Bitmap> PlayerSprites = new();
    public static List<Bitmap> Maps = new();
    public static List<Bitmap> Cards = new();
    public static List<Bitmap> Coins = new();
    public static List <Bitmap> Felix = new();
    public static List<Bitmap> Market = new();
}
