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
            this.player = gameObject as Player;
        CollisionManager.Current.AddGameObject(gameObject);
    }

    public void Render(Graphics g, PictureBox pb)
    {
        foreach (var gameObject in CollisionManager.Current.gameObjects)
        {
            gameObject.Render(g, pb);
        }
    }
}
