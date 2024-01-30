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
    // public List<Map> Maps = new();
    public Map CurrentMap { get; set; } // => current map

    public SoundPlayer Sound { get; set; } = new();

    private GameEngine() { }

    public void StartSound() => Sound.Play();

    public void StartBackground(Graphics g, PictureBox pb) => CurrentMap.Render(g, pb);

    public void StartUp(PictureBox pb)
    {
        Resources.New();
        CollisionManager.New();
        MapManager.New();

        MapManager.AddMap(new Dungeon_01(pb));
        MapManager.AddMap(new Dungeon_02(pb));
        MapManager.AddMap(new Dungeon_01(pb));
        MapManager.AddMap(new Dungeon_02(pb));
        MapManager.AddMap(new Dungeon_01(pb));
        MapManager.AddMap(new Test_Dungeon(pb));

        MapManager.Map = MapManager.Maps[0];

        MapManager.AddWalls();

        Player p = new Player("Him", 70, 700)
        {
            X = MapManager.Map.PlayerSpawn.X,
            Y = MapManager.Map.PlayerSpawn.Y
        }; // TODO: add image from resources
        AddObject(p);
        AddObject(new Weapon("Weapon", 0, 0, 10, 10, this.Player));
        
        Boss b = new FelixTheToad(960, 540);
        AddObject(b);
        AddObject(new Coin("Moeda", 900, 700));
    }

    public void Update()
    {
        foreach (var gameObject in CollisionManager.gameObjects.ToList())
        {
            if (gameObject is null)
                continue;
            gameObject.Update();
        }
    }

    public void Render(Graphics g, PictureBox pb)
    {
        foreach (var gameObject in CollisionManager.gameObjects.ToList())
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
        CollisionManager.AddGameObject(gameObject);
    }

    public void AddWalls()
    {
        foreach (var item in MapManager.Maps)
        {
            if (item == MapManager.Map)
            {
                foreach (var wall in item.Walls)
                {
                    CollisionManager.AddGameObject(wall);
                }
            }
        }
    }

    public void AddMap(Map map) => MapManager.Maps.Add(map);

    public void Run() { }

    public void Stop() { }

    public static void New() => current = new GameEngine();
}
