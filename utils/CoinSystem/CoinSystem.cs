using System;
using System.Linq;
using System.Windows.Forms;

public class CoinSystem
{
    private DateTime last = DateTime.Now;
    private float minWidth;
    private float maxWidth;
    private float minHeight;
    private float maxHeight;
    public float Cooldown { get; set; }
    public int Count { get; set; }
    public int MaxCoins { get; set; }
    private bool spawnable = true;

    public CoinSystem
    (
        float minWid,
        float maxWid,
        float minHei,
        float maxHei,
        float cd,
        int maxCollectedCoins
    )
    {
        this.minWidth = minWid;
        this.maxWidth = maxWid;
        this.minHeight = minHei;
        this.maxHeight = maxHei;

        this.Cooldown = cd;
        this.MaxCoins = maxCollectedCoins;
    }

    public void Act()
    {
        AddRandomCoin();
    }

    private void AddRandomCoin()
    {
        if (!spawnable)
            return;

        var now = DateTime.Now;
        var dt = now - last;
        var seconds = dt.TotalSeconds;

        if (seconds < Cooldown)
            return;

        var query =
            from obj in CollisionManager.GameObjects.ToList()
            where obj is Coin
            select obj;

        var coin = query.FirstOrDefault();

        if (this.Count > this.MaxCoins)
        {
            CollisionManager.RemoveGameObject(coin);
            spawnable = false;
            return;
        }

        if (coin is not null)
            CollisionManager.RemoveGameObject(coin);

        if (seconds < Cooldown + 3)
            return;

        CollisionManager.AddGameObject(
            new Coin(
                "Moeda",
                Random.Shared.Next(
                    (int)(this.minWidth),
                    (int)(
                        this.maxWidth
                    )
                ),
                Random.Shared.Next(
                    (int)(this.minHeight),
                    (int)(
                        this.maxHeight
                    )
                )
            )
        );

        last = DateTime.Now;
    }
}