using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class GameEngine
{
    public Player player = null;
    public List<Map> Maps = new();
    public Map CurrentMap { get; set; } // => current map
    public SoundPlayer Sound { get; set; } = new();
    private int index = 0;

    public void StartSound() => Sound.Play();
    public void StartBackground(Graphics g, PictureBox pb) => CurrentMap.Render(g, pb);

    public void StartUp(PictureBox pb)
    {
        AddMap(new Dungeon_01(pb));
        CurrentMap = this.Maps[0];
        AddObject(new Player("Him", 700, 700, "./assets/Sprites/Player/SPRITE/k_0.png"));
        AddObject(new Weapon("Weapon", 0, 0, 10, 10, this.player));
        AddObject(new Boss("Frog", 50, 900, "./assets/Sprites/Bosses/pxArt.png"));
        
        AddWalls();
    }

    public void Update()
    {
        foreach (var gameObject in CollisionManager.Current.gameObjects)
        {
            gameObject.Update();
        }
    }

    public void Render(Graphics g, PictureBox pb)
    {
        CurrentMap.Render(g, pb);
        foreach (var gameObject in CollisionManager.Current.gameObjects)
        {
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
            this.player = newPlayer;
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

    public void AddMap(Map map)
        => Maps.Add(map);

    public void nextMap()
    {
        index++;
        CurrentMap = Maps[index];
    }

    public void prevMap()
    {
        index--;
        CurrentMap = Maps[index];
    }

    public void Run(){

    }
    public void Stop(){
        
    }
}
