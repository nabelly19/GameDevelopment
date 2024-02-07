using System.Drawing;
using System.Windows.Forms;

public static class ClientScreenSize
{
    public static int Width { get; private set; } = Screen.PrimaryScreen.Bounds.Width;
    public static int Height { get; private set; } = Screen.PrimaryScreen.Bounds.Height;
    public static float WidthFactor { get; private set; } = Screen.PrimaryScreen.Bounds.Width / 1920f;
    public static float HeightFactor { get; private set; } = Screen.PrimaryScreen.Bounds.Height / 1080f;
    public static Rectangle GetClientScreenSize()
        => Screen.PrimaryScreen.Bounds;
}