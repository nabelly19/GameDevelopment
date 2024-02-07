using System.CodeDom;
using System.Drawing;
using System.Windows.Forms;

public static class StartScreen
{
    private static Image image = Resources.Wallpaper[0];
    public static bool goStart { get; private set; } = false;

    public static void Render(Graphics g, PictureBox pb)
    {
        g.DrawImage(image, 0, 0, pb.Width, pb.Height);
    }

    public static void SetgoStart()
    {
        if (!goStart)
        {
            GameEngine.New();
            GameEngine.Current.StartUp();
            GameEngine.Current.StartSound();
        }
        goStart = true;
    }
}
