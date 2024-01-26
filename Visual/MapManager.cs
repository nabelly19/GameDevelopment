using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class MapManager
{
    private static MapManager crr;
    public static MapManager Current => crr;
    public List<Map> Maps { get; private set; }
    private int index = 0;
    private Map prevMap;
    public Map Map { get; set; }
    private Map newMap;
    private static int timer;
    private bool transitioning = false;
    private int transitionClock;
    private MapManager()
    {
        Maps = new List<Map>();
        crr = this;
    }

    public void AddWalls()
    {
        foreach (var item in Maps)
        {
            if (item == Map)
            {
                foreach (var wall in item.Walls)
                {
                    CollisionManager.Current.AddGameObject(wall);
                }
            }
        }
    }

    public void AddMap(Map map) => Maps.Add(map);

    public void nextMap()
    {
        index++;
        prevMap = Map;
        newMap = Maps[index];
        transitioning = true;
    }

    public void PrevMap()
    {
        index--;
        prevMap = Map;
        newMap = Maps[index];
        transitioning = true;
    }

    public void RenderMapOrFade(Graphics g, PictureBox pb)
    {
        if (!transitioning)
            Map.Render(g, pb);
        else
            DrawFadeMap(g, pb);
    }

    public void DrawFadeMap(Graphics g, PictureBox pb)
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
            foreach (var item in CollisionManager.Current.gameObjects.ToList())
            {
                if (item is Wall)
                    CollisionManager.Current.gameObjects.Remove(item);
            }
            AddWalls();
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

    public static void New() => crr = new MapManager();
}