using System;
using System.Drawing;
using System.Windows.Forms;

public class SpiralProjectileState : State
{
    DateTime? dt = null;
    int count = 0;

    public override void Act(Boss boss)
    {
        if (count == 3)
        {
            dt = null;
            count = 0;
            GoToNext();
            return;
        }

        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(2))
            return;

        count++;
        dt = null;

        for (int i = 0; i < 3; i++)
        {
            GameEngine.Current.AddObjectToCollisionList(
                new SpiralProjectile(
                    "Bullet",
                    boss.X - 14,
                    boss.Y - 50,
                    Random.Shared.Next(0, 89),
                    boss
                )
            );
            GameEngine.Current.AddObjectToCollisionList(
                new SpiralProjectile(
                    "Bullet",
                    boss.X - 14,
                    boss.Y - 50,
                    Random.Shared.Next(90, 179),
                    boss
                )
            );
            GameEngine.Current.AddObjectToCollisionList(
                new SpiralProjectile(
                    "Bullet",
                    boss.X - 14,
                    boss.Y - 50,
                    Random.Shared.Next(180, 269),
                    boss
                )
            );
            GameEngine.Current.AddObjectToCollisionList(
                new SpiralProjectile(
                    "Bullet",
                    boss.X - 14,
                    boss.Y - 50,
                    Random.Shared.Next(270, 359),
                    boss
                )
            );
        }
    }
}
