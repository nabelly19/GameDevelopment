using System;
using System.Linq;
using System.Windows.Forms;

public class CoinSystem
{
    private DateTime last = DateTime.Now;
    private float maxWidth;
    private float maxHeight;
    public float Cooldown { get; set; }
    public int Count { get; set; }
    public int MaxCoins { get; set; }
    private bool spawnable = true;

    public CoinSystem
    (
        float maxWid,
        float maxHei,
        float cd,
        int maxCollectedCoins
    )
    {
        this.maxWidth = maxWid;
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
                    (int)(this.maxWidth * 0.13f),
                    (int)(
                        this.maxWidth
                        - this.maxWidth * 0.13f
                    )
                ),
                Random.Shared.Next(
                    (int)(this.maxHeight * 0.297f),
                    (int)(
                        this.maxHeight
                        - this.maxHeight * 0.297f
                    )
                )
            )
        );

        last = DateTime.Now;
    }
}