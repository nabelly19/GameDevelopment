using System;
using System.Drawing;

public class SpiralWaveState : State
{
    // private Image image = Bitmap.FromFile("...assets/Beam/beam.png") as Bitmap;
    private float angle = -45;
    DateTime? dt = null;

    public override void Act(Boss boss)
    {
        if (angle >= 225)
        {
            GameEngine.Current.AddObjectToCollisionList(
                new Wave("Bullet", boss.X - 14, boss.Y - 50, Resources.Beam, angle, boss)
            );
            angle = 0;
            GoToNext();
            return;
        }

        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(2.5))
            return;

        GameEngine.Current.AddObjectToCollisionList(
            new Wave("Bullet", boss.X - 14, boss.Y - 50, Resources.Beam, angle, boss)
        );

        // 4 = 360 mais espacado
        // > 1 = 360
        // 1 = 180
        angle += 1;
    }
}
