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
    }

    public void Move()
    {
        var collided = CollisionManager.GetCollisions(this).FirstOrDefault();

        if (collided is Player)
            CollisionManager.RemoveGameObject(this);

        AnimateItem(1, 5);
    }

    private void AnimateItem(int start, int end)
    {
        SlowFrameRate += 1;

        if (SlowFrameRate > 5)
        {
            Steps++;
            SlowFrameRate = 0;
        }

        if (Steps > end || Steps < start)
        {
            Steps = start;
        }

        this.Sprite = Resources.Coins[Steps];
    }
}
