using System.Drawing;
using System.Windows.Forms;

public static class ClientScreen
{
    public static int Width { get; private set; } = Screen.PrimaryScreen.Bounds.Width;
    public static int Height { get; private set; } = Screen.PrimaryScreen.Bounds.Height;
    public static float WidthFactor { get; private set; } =
        Screen.PrimaryScreen.Bounds.Width / 1920f;
    public static float HeightFactor { get; private set; } =
        Screen.PrimaryScreen.Bounds.Height / 1080f;

    public static Rectangle GetClientScreen() => Screen.PrimaryScreen.Bounds;

    public static float ResponsiveX(float X) => X * Width / 1920f;

    public static float ResponsiveX(int X) => X * Width / 1920;

    public static float ResponsiveY(float Y) => Y * Height / 1080f;

    public static float ResponsiveY(int Y) => Y * Height / 1080;
}
