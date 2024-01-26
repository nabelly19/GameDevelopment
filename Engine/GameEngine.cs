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
    public bool transitioning = false;
    public  Map newMap;
    public int transitionClock;
    public int timer;
    public int transitionStep = 0;
    public void StartSound() => Sound.Play();
    public void StartBackground(Graphics g, PictureBox pb) => CurrentMap.Render(g, pb);

    public void StartUp(PictureBox pb)
    {
        AddMap(new Dungeon_01(pb));
        AddMap(new Dungeon_02(pb));

        CurrentMap = this.Maps[1];
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
        // DrawFadeMap(g, pb);

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

    
    public void TimerTick(Graphics g, PictureBox pb)
    {
         if(transitioning)
            StartTransitioning(CurrentMap, g);
    }


    public void StartTransitioning(Map map, Graphics g)
    {
        index++;
        newMap = Maps[index];

        transitionStep = 0;
        transitioning = true;

        DrawFadeMap(g);
    }
    //aqui KKKKKKKKKKKK

    public void DrawFadeMap(Graphics g)
    {   

        if(!transitioning)
            return;

        transitionClock = 5;

        g.DrawImage(CurrentMap.image, 500, 500);
        if (CurrentMap == Maps[0])
            timer += transitionClock;
        else
            timer -=  transitionClock;
        if (timer < 0)
            timer = 0;

          g.FillRectangle(
            new SolidBrush(Color.FromArgb( timer % 256, 0, 0, 0)),
            0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height
        );

        if (timer == 255)
        {
            CurrentMap = newMap;
            newMap = null;
            transitioning = false;
            // MessageBox.Show("ELP");
        }
    }
    

    public void Run(){

    }
    public void Stop(){
        
    }
}
