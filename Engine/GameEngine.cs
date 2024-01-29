using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class GameEngine
{
    private static GameEngine current;
    public static GameEngine Current => current;
    public Player Player { get; set; }
    public List<Map> Maps = new();
    public Map CurrentMap { get; set; } // => current map

    public SoundPlayer Sound { get; set; } = new();
    private int index = 0;
    public bool transitioning = false;
    public Map PrevMap;
    public Map newMap;
    public int transitionClock;
    public int timer;

    private GameEngine() { }

    public void StartSound() => Sound.Play();

    public void StartBackground(Graphics g, PictureBox pb) => CurrentMap.Render(g, pb);

    public void StartUp(PictureBox pb)
    {
        Resources.New();
        CollisionManager.New();
        MapManager.New();

        MapManager.Current.AddMap(new Dungeon_01(pb));
        MapManager.Current.AddMap(new Dungeon_02(pb));
        MapManager.Current.AddMap(new Dungeon_01(pb));
        MapManager.Current.AddMap(new Dungeon_02(pb));
        MapManager.Current.AddMap(new Dungeon_01(pb));
        MapManager.Current.AddMap(new Test_Dungeon(pb));

        MapManager.Current.Map = MapManager.Current.Maps[0];

        // MapManager.Current.AddWalls();

        Player p = new Player("Him", 700, 700); // TODO: add image from resources
        Boss b = new FelixTheToad(960, 540);

        AddObject(p);
        AddObject(b);
        AddObject(new Coin("Moeda", 900, 700));
    }

    public void Update()
    {
        foreach (var gameObject in CollisionManager.Current.gameObjects.ToList())
        {
            if (gameObject is null)
                continue;
            gameObject.Update();
        }
    }

    public void Render(Graphics g, PictureBox pb)
    {
        foreach (var gameObject in CollisionManager.Current.gameObjects.ToList())
        {
            if (gameObject is null)
                continue;
            gameObject.Render(g, pb);
        }

        // FadeEffect Rectangle
        MapManager.DrawFadeRectangle(g);
    }

    public void AddObject(GameObject gameObject)
    {
        if (gameObject is Player)
        {
            var newPlayer = gameObject as Player;
            var weapon = new Weapon("Weapon", 0, 0, 50, 50, newPlayer);
            newPlayer.Weapon = weapon;
            this.Player = newPlayer;
            AddObject(weapon);
        }
        CollisionManager.Current.AddGameObject(gameObject);
    }

    public void AddWalls()
    {
        foreach (var item in Maps)
        {
            if (item == CurrentMap)
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
        PrevMap = CurrentMap;
        newMap = Maps[index];
        transitioning = true;
    }

    public void prevMap()
    {
        index--;
        PrevMap = CurrentMap;
        newMap = Maps[index];
        transitioning = true;
    }

    public void TimerTick(Graphics g, PictureBox pb)
    {
        if (!transitioning)
            CurrentMap.Render(g, pb);
        else
            DrawFadeMap(g, pb);
    }

    public void DrawFadeMap(Graphics g, PictureBox pb)
    {
        transitionClock = 5;

        CurrentMap.Render(g, pb);
        if (CurrentMap == PrevMap)
            timer += transitionClock;
        else
            timer -= transitionClock;

        if (timer < 0)
        {
            timer = 0;
            transitioning = false;
        }

        //     g.FillRectangle(
        //       new SolidBrush(Color.FromArgb(timer % 256, 0, 0, 0)),
        //       0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height
        //   );

        if (timer == 255)
        {
            CurrentMap = newMap;
            foreach (var item in CollisionManager.Current.gameObjects.ToList())
            {
                if (item is Wall)
                    CollisionManager.Current.gameObjects.Remove(item);
            }
            AddWalls();
            newMap = null;
        }
    }

    public void Run() { }

    public void Stop() { }

    public static void New() => current = new GameEngine();
}
