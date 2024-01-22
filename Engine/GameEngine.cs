using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class GameEngine
{
    public Player player = null;
    public Background Background { get; set; } = new();
    public SoundPlayer Sound { get; set; } = new();

    public void StartSound() => Sound.Play();

    public void StartBackground(Graphics g, PictureBox pb) => Background.Draw(g, pb);

    public void Update()
    {
        foreach (var gameObject in CollisionManager.Current.gameObjects)
        {
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
            this.player = newPlayer;
            AddObject(weapon);
        }
        CollisionManager.Current.AddGameObject(gameObject);
    }

    public void Render(Graphics g, PictureBox pb)
    {
        foreach (var gameObject in CollisionManager.Current.gameObjects)
        {
            gameObject.Render(g, pb);
        }
    }

    public void Run(){

    }
    public void Stop(){
        
    }
}
