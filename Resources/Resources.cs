using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;
using System.Windows.Forms;

public static class Resources
{
    public static List<Bitmap> PlayerSprites = new();
    public static List<Bitmap> Maps = new();
    public static List<Bitmap> Coins = new();
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
        this.Market = Directory 
            .GetFiles("../../../assets/Sprites/Market/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();

    }

    public List<Bitmap> PlayerSprites = new();
    public List<Bitmap> Maps = new();
    public List<Bitmap> Coins = new();
    public List<Bitmap> Market = new();
    public static void New() => crr = new Resources();
}
