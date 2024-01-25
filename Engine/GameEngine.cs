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

    private GameEngine() { }

    public void StartSound() => Sound.Play();

    public void StartBackground(Graphics g, PictureBox pb) => CurrentMap.Render(g, pb);

    public void StartUp(PictureBox pb)
    {
        // AddMap(new Dungeon_01(pb));
        // CurrentMap = this.Maps[0];

        Player p = new Player("Him", 700, 700);
        Boss b = new FelixTheToad(960, 540);

        AddObject(p);
        AddObject(new Weapon("Weapon", 0, 0, 10, 10, this.Player));
        AddObject(b);

        // AddWalls();
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
        // CurrentMap.Render(g, pb);
        foreach (var gameObject in CollisionManager.Current.gameObjects.ToList())
        {
            if (gameObject is null)
                continue;
            gameObject.Render(g, pb);
        }
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

    public void Run() { }

    public void AddMap(Map map) => Maps.Add(map);

    public void nextMap()
    {
        index++;
        CurrentMap = Maps[index];
    }

    public void prevMap()
    {
        this.index--;
        this.CurrentMap = Maps[index];
    }

    public void Stop() { }

    public static void New() => current = new GameEngine();
}
