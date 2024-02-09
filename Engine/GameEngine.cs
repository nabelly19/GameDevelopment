using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class GameEngine
{
    private static GameEngine current;
    public static GameEngine Current => current;
    public Player Player { get; set; } = new Player("Him", 0, 0);

    public SoundPlayer Sound { get; set; } = new();

    private GameEngine() { }

    public void StartSound() => Sound.Play();

    public void StartUp()
    {
        Resources.New();
        CollisionManager.ResetList();
        MapManager.Start();
        ItemManager.Startup();
        HUD.Start();
    }

    public void Update()
    {
        if (!this.Player.isAlive)
            RestartGame();

        MapManager.UpdateMap();

        foreach (var gameObject in CollisionManager.GameObjects.ToList())
        {
            if (gameObject is null)
                continue;
            gameObject.Update();
        }
    }

    public void Render(Graphics g, PictureBox pb)
    {
        MapManager.RenderMapOrFade(g, pb);

        foreach (var gameObject in CollisionManager.GameObjects.ToList())
        {
            if (gameObject is null)
                continue;
            gameObject.Render(g, pb);
        }

        HUD.Render(g, pb);

        MapManager.DrawFadeRectangle(g);
    }

    public void AddObjectToCollisionList(GameObject gameObject) =>
        CollisionManager.AddGameObject(gameObject);

    public void AddPlayer(List<GameObject> list)
    {
        if (list.Contains(this.Player))
            return;
        this.Player.Weapon ??= new Weapon("Weapon", 0, 0, 50, 50, this.Player);
        list.Add(this.Player);
    }

    public void AddMap(Map map) => MapManager.Maps.Add(map);

    public void RestartGame()
    {
        MapManager.Restart();
    }

    public static void New() => current = new GameEngine();
}
