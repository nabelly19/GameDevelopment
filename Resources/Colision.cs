using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class Colision
{
    public void BossPlayer(Boss boss, Player player)
    {
        var p = player.Hitbox;
        var b = boss.Hitbox;

        var pBottom = p.Y + p.Height;
        var pTop = p.Y;
        var pLeft = p.X;
        var pRight = p.X + p.Width;

        var bBottom = b.Y + b.Height;
        var bTop = b.Y;
        var bLeft = b.X;
        var bRight = b.X + b.Width;

        var opBottom = player.Old_Y + p.Height;
        var opTop = player.Old_Y;
        var opLeft = player.Old_X;
        var opRight = player.Old_X + p.Width;

        var obBottom = boss.Old_Y + b.Height;
        var obTop = boss.Old_Y;
        var obLeft = boss.Old_X;
        var obRight = boss.Old_X + b.Width;

        if (pBottom < bTop || pTop > bBottom || pLeft > bRight || pRight < bLeft)
            return;

        if (pBottom >= bTop)
        {
            player.Y = bTop - p.Height / 2 - 0.1f;
            player.Velocity_Y *= -1;
            // MessageBox.Show("Foi no 1");
        }
        else if (pTop <= bBottom)
        {
            player.Y = bBottom + p.Height / 2 + 0.1f;
            player.Velocity_Y *= -1;
            // MessageBox.Show("Foi no 2");
        }
        else if (pRight >= bLeft)
        {
            player.X = bLeft - p.Width / 2 - 0.1f;
            player.Velocity_X *= -1;
            // MessageBox.Show("Foi no 3");
        }
        else if (pLeft <= bRight)
        {
            player.X = bRight + p.Width / 2 + 0.1f;
            player.Velocity_X *= -1;
            // MessageBox.Show("Foi no 4");
        }
    }

    public void PlayerMap(Hittable player, Map map) { }
}
