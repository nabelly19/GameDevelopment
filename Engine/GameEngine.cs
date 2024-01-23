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
    public Background Background { get; set; } = new();
    public SoundPlayer Sound { get; set; } = new();

    private GameEngine() { }

    public void StartSound() => Sound.Play();

    public void StartBackground(Graphics g, PictureBox pb) => Background.Draw(g, pb);

    public void Update()
    {
        foreach (var gameObject in CollisionManager.Current.gameObjects.ToList())
        {
            if (gameObject is null)
                continue;
            gameObject.Update();
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

    public void Render(Graphics g, PictureBox pb)
    {
        foreach (var gameObject in CollisionManager.Current.gameObjects.ToList())
        {
            if (gameObject is null)
                continue;
            gameObject.Render(g, pb);
        }
    }

    public void Run() { }

    public void Stop() { }

    public static void New() => current = new GameEngine();
}
