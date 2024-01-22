using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;

public class Resources
{
    private static Resources crr;
    public static Resources Current => crr;

    private Resources()
    {
        this.PlayerSprites = Directory
            .GetFiles("./assets/Sprites/Player/NewSprite/", "*.png")
            .Select(file => Bitmap.FromFile(file) as Bitmap)
            .ToList();
    }

    public List<Bitmap> PlayerSprites = new();

    public static void New() => crr = new Resources();
}
