using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public abstract class Map
{
    public PointF PlayerSpawn { get; set; }
    public virtual Boss Boss { get; set; } = null;
    public abstract List<GameObject> GameObjects { get; set; }
    public abstract System.Media.SoundPlayer song { get; set; }
    public abstract CoinSystem CoinSystem { get; set; }
    public virtual void SetBackground(Image img) { this.image = img; }
    public virtual void SetBackground(Bitmap img) { this.image = img; }
    protected Image image = null;
    public abstract void InitializeMapObjects();
    public abstract void UpdateBackground();
    public abstract void RenderBackground(Graphics g, PictureBox pb);
    protected virtual void AddRandomCoin()
    {
        var query =
            from obj in CollisionManager.GameObjects
            where obj is Coin
            select obj;
        if (query.FirstOrDefault() is null)
            CollisionManager.AddGameObject(
                new Coin(
                    "Moeda",
                    Random.Shared.Next(
                        (int)(Screen.PrimaryScreen.Bounds.Width * 0.13f),
                        (int)(
                            Screen.PrimaryScreen.Bounds.Width
                            - Screen.PrimaryScreen.Bounds.Width * 0.13f
                        )
                    ),
                    Random.Shared.Next(
                        (int)(Screen.PrimaryScreen.Bounds.Height * 0.297f),
                        (int)(
                            Screen.PrimaryScreen.Bounds.Height
                            - Screen.PrimaryScreen.Bounds.Height * 0.297f
                        )
                    )
                )
            );
    }
    protected virtual void AddObjects(params GameObject[] objs)
    {
        foreach (var obj in objs)
            this.GameObjects.Add(obj);
    }
    public virtual void ResetInteractables() {}
}
