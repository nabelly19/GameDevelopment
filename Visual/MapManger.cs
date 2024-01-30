using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public static class MapManager
{
    public static List<Map> Maps { get; private set; }
    private static int index = 0;
    private static Map prevMap;
    public static Map Map { get; set; }
    private static Map newMap;
    private static int timer;
    private static bool transitioning = false;
    private static int transitionClock;

    public static void AddWalls()
    {
        foreach (var item in Maps)
        {
            if (item == Map)
            {
                foreach (var wall in item.GameObjects)
                    CollisionManager.AddGameObject(wall);

                if (item.Boss is not null)
                    CollisionManager.AddGameObject(item.Boss);
            
            }
        }
    }

    public static void AddMapObjects()
    {
        foreach (var item in Maps)
        {
            if (item == Map)
            {   
                
                GameEngine.Current.AddPlayer(item.GameObjects);
                CollisionManager.gameObjects = item.GameObjects;
            }
        }
    }

    public static void AddMap(Map map) => Maps.Add(map);

    public static void NextMap()
    {
        index++;
        prevMap = Map;
        newMap = Maps[index];
        transitioning = true;
    }

    public static void PrevMap()
    {
        index--;
        prevMap = Map;
        newMap = Maps[index];
        transitioning = true;
    }

    public static void RenderMapOrFade(Graphics g, PictureBox pb)
    {
        if (!transitioning)
            Map.Render(g, pb);
        else
            DrawFadeMap(g, pb);
    }

    public static void DrawFadeMap(Graphics g, PictureBox pb)
    {
        transitionClock = 5;

        Map.Render(g, pb);
        if (Map == prevMap)
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
            Map = newMap;
            AddMapObjects();
            GameEngine.Current.Player.X = Map.PlayerSpawn.X;
            GameEngine.Current.Player.Y = Map.PlayerSpawn.Y;
            newMap = null;
        }
    }

    public static void DrawFadeRectangle(Graphics g)
    {
            g.FillRectangle(
              new SolidBrush(Color.FromArgb(timer % 256, 0, 0, 0)),
              0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height
          );
    }

    public static void New() 
        => Maps = new List<Map>();

}
