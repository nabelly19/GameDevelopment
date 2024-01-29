using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Platform : GameObject
{
    public DateTime creationTime = DateTime.Now;
    public float damageInitiationDelay = 3_750;

    public Platform(string name, float x, float y, float width, float height)
        : base(name, x, y, width, height)
    {
        DisableHitbox();
    }

    public override void Update()
    {
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
                CollisionManager.Current.CheckCollisionbyPoint(this.Hitbox, bottomR)
                || CollisionManager.Current.CheckCollisionbyPoint(this.Hitbox, bottomL)
            )
        )
        {
            var diff = DateTime.Now - creationTime;
            var millis = (float)diff.TotalMilliseconds;

            if (millis > damageInitiationDelay)
                player.ReceiveDamage();
        }
    }

    public override void Render(Graphics g, PictureBox pb)
    {
        CreateHitbox(this.X, this.Y, this.Width, this.Height);
        g.DrawRectangle(Pens.White, this.Hitbox);
    }
}