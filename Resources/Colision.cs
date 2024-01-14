using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class Colision
{
    public void BossPlayer(Hittable boss, Hittable player)
    {
        var p = player.Hitbox;
        var b = boss.Hitbox;
        if (
            p.X < b.X + b.Width &&
            p.X + p.Width > b.X &&
            p.Y < b.Y + b.Height &&
            p.Y + p.Height > b.Y
        )
        {
            // Calculate the overlap on both the X and Y axes
            var overlapX = Math.Min(p.X + p.Width, b.X + b.Width) - Math.Max(p.X, b.X);
            var overlapY = Math.Min(p.Y + p.Height, b.Y + b.Height) - Math.Max(p.Y, b.Y);

            // Adjust player's position based on the axis with greater overlap
            if (overlapX < overlapY)
            {
                // Adjust X position
                var x = player.X - player.Old_X;
                if (x > 0)
                {
                    player.X = b.X - p.Width; // Move player to the left edge of the boss hitbox
                }
                else if (x < 0)
                {
                    player.X = b.X + b.Width; // Move player to the right edge of the boss hitbox
                }
            }
            else
            {
                // Adjust Y position
                var y = player.Y - player.Old_Y;
                if (y > 0)
                {
                    player.Y = b.Y - p.Height; // Move player to the top edge of the boss hitbox
                }
                else if (y < 0)
                {
                    player.Y = b.Y + b.Height; // Move player to the bottom edge of the boss hitbox
                }
            }
        }
    }
    public void PlayerMap(Hittable player, Map map)
    {

    }
}