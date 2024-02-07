using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Coin : GameObject, IMoveable
{
    public float BaseAcceleration { get; set; }
    public float Ax { get; set; }
    public float Ay { get; set; }
    public int StateManager { get; set; }
    public int Steps { get; set; } = 0;
    public int SlowFrameRate { get; set; } = 0;
    public float Vx { get; set; }
    public float Vy { get; set; }
    public bool isMoving { get; set; }

    public Coin(string name, int x, int y)
        : base(name, x, y, Resources.Coins[0])
    {
        DisableHitbox();
        this.Height = 110;
        this.Width = 1.25555f * this.Height;
        this.Width /= 4f;
        this.Height /= 4f;
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawImage(this.Sprite, this.X, this.Y, this.Width, this.Height);
    }

    public override void Update()
    {
        Move();
        VerifyCollisions();
      
    }

    public bool VerifyCollisions()
    {
        var collided = CollisionManager.GetCollisions(this);
        foreach (var other in collided)
        {
            if (other is Player player)
            {
                player.ColectCoin();
                CollisionManager.RemoveGameObject(this);
                MapManager.Current.CoinSystem.Count++;
                return true;
            }
        }
        return false;
    }

    public void Move()
    => AnimateItem(1, 5);

    private void AnimateItem(int start, int end)
    {
        SlowFrameRate += 1;

        if (SlowFrameRate > 5)
        {
            Steps++;
            SlowFrameRate = 0;
        }

        if (Steps > end || Steps < start)
            Steps = start;


        this.Sprite = Resources.Coins[Steps];
    }
}