using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class GameEngine
{
    public Player player = null;
    public List<Map> Maps = new();
    public Map CurrentMap { get; set; } // => current map
    public SoundPlayer Sound { get; set; } = new();
    private int index = 0;
    public bool transitioning = false;
    public Map PrevMap;
    public Map newMap;
    public int transitionClock;
    public int timer;
    public int transitionStep = 0;
    public void StartSound() => Sound.Play();
    public void StartBackground(Graphics g, PictureBox pb) => CurrentMap.Render(g, pb);

    public void StartUp(PictureBox pb)
    {
        AddMap(new Dungeon_01(pb));
        AddMap(new Test_Dungeon(pb));
        AddMap(new Dungeon_01(pb));
        AddMap(new Test_Dungeon(pb));
        AddMap(new Dungeon_01(pb));
        AddMap(new Test_Dungeon(pb));

        CurrentMap = this.Maps[0];
        AddObject(new Player("Him", 700, 700, "../../../assets/Sprites/Player/SPRITE/k_0.png"));
        AddObject(new Weapon("Weapon", 0, 0, 10, 10, this.player));
        AddObject(new Boss("Frog", 50, 900, "../../../assets/Sprites/Bosses/pxArt.png"));

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
        // CurrentMap.Render(g, pb);
        // DrawFadeMap(g, pb);

        foreach (var gameObject in CollisionManager.Current.gameObjects)
        {
            gameObject.Render(g, pb);
        }
        g.FillRectangle(
          new SolidBrush(Color.FromArgb(timer % 256, 0, 0, 0)),
          0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height
      );
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
            transitionStep = 0;
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
            
            // MessageBox.Show("ELP");
        }
    }


    public void Run()
    {

    }
    public void Stop()
    {

    }
}
