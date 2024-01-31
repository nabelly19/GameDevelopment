using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public static class MapManager
{
    public static List<Map> Maps { get; private set; }
    private static int index = 0;
    private static Map previous;
    public static Map Current { get; set; }
    private static Map next;
    private static int timer;
    private static bool transitioning = false;
    private static int transitionClock;

    public static void AddMapObjects()
    {
        foreach (var item in Maps)
        {
            if (item == Current)
                CollisionManager.SetGameobjects(item.GameObjects);
        }
    }

    public static void AddMap(Map map) => Maps.Add(map);

    public static void NextMap()
    {
        index++;
        previous = Current;
        next = Maps[index];
        transitioning = true;
    }

    public static void PreviousMap()
    {
        index--;
        previous = Current;
        next = Maps[index];
        transitioning = true;
    }

    public static void RenderMapOrFade(Graphics g, PictureBox pb)
    {
        if (!transitioning)
            Current.RenderBackground(g, pb);
        else
            DrawFadeMap(g, pb);
    }

    public static void DrawFadeMap(Graphics g, PictureBox pb)
    {
        transitionClock = 5;

        Current.RenderBackground(g, pb);
        if (Current == previous)
            timer += transitionClock;
        else
            timer -= transitionClock;

        if (timer < 0)
        {
            timer = 0;
            transitioning = false;
        }

        if (timer == 255)
        {
            Current = next;
            AddMapObjects();
            setPlayerSpawn();
            next = null;
        }
    }

    private static void setPlayerSpawn()
    {
        GameEngine.Current.Player.X = Current.PlayerSpawn.X;
        GameEngine.Current.Player.Y = Current.PlayerSpawn.Y;
    }

    public static void DrawFadeRectangle(Graphics g)
    {
        g.FillRectangle(
            new SolidBrush(Color.FromArgb(timer % 256, 0, 0, 0)),
            0,
            0,
            Screen.PrimaryScreen.Bounds.Width,
            Screen.PrimaryScreen.Bounds.Height
        );
    }

    public static void InitializeMapList() => Maps = new List<Map>();

    public static void Start(PictureBox pb)
    {
        InitializeMapList();

        AddMap(new FirstRoom(pb));
        AddMap(new FelixRoom(pb));

        Current = Maps[0];

        GameEngine.Current.Player.X = Current.PlayerSpawn.X;
        GameEngine.Current.Player.Y = Current.PlayerSpawn.Y;
        AddMapObjects();
    }
}
