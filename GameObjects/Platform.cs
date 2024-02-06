using System;
using System.Drawing;
using System.Windows.Forms;

public class Platform : GameObject
{
    public DateTime creationTime = DateTime.Now;
    public bool isAttaking { get; private set; } = false;
    public float damageInitiationDelay = 3_750;

    public Platform(string name, float x, float y, float width, float height)
        : base(name, x, y, width, height)
    {
        DisableHitbox();
    }

    public override void Update()
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        var player = GameEngine.Current.Player;
        var bottomR = new PointF(
            player.X + player.Hitbox.Width / 2,
            player.Y + player.Hitbox.Height / 2
        );
        var bottomL = new PointF(
            player.X - player.Hitbox.Width / 2,
            player.Y + player.Hitbox.Height / 2
        );
        if (
            !(
                CollisionManager.CheckCollisionByPoint(this.Hitbox, bottomR)
                || CollisionManager.CheckCollisionByPoint(this.Hitbox, bottomL)
            )
        )
        {
            if (isAttaking)
                player.ReceiveDamage();
        }
        var diff = DateTime.Now - creationTime;
        var millis = (float)diff.TotalMilliseconds;

        if (millis > damageInitiationDelay)
            isAttaking = true;
        else
            isAttaking = false;
    }

    public override void Render(Graphics g, PictureBox pb) =>
        g.DrawRectangle(Pens.White, this.Hitbox);
}
