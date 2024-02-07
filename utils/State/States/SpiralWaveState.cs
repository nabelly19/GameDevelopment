using System;

public class SpiralWaveState : State
{
    private float angle = 0;
    DateTime? dt = null;

    public override void Act(Boss boss)
    {
        if (angle >= 180)
        {
            GameEngine.Current.AddObjectToCollisionList(
                new Wave("Bullet", boss.X - 14, boss.Y - 50, 25, 25, angle, boss)
            );
            angle = 0;
            GoToNext();
            return;
        }

        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(2.5))
            return;

        GameEngine.Current.AddObjectToCollisionList(
            new Wave("Bullet", boss.X - 14, boss.Y - 50, 25, 25, angle, boss)
        );

        // 4 = 360 mais espacado
        // > 1 = 360
        // 1 = 180
        angle += 1;
    }
}
