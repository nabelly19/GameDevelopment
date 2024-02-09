using System;

public class CircularlWaveState : State
{
    private float angle = 0;
    DateTime? dt = null;
    int count = 0;

    public override void Act(Boss boss)
    {
        if (count >= 2)
        {
            dt = null;
            angle = 0;
            count = 0;
            GoToNext();
            return;
        }

        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(1.5))
            return;

        for (int i = 0; i < 180; i++)
        {
            GameEngine.Current.AddObjectToCollisionList(
                new Wave("Bullet", boss.X, boss.Y, Resources.LitchSpiral, angle, boss)
            );
            var a = CollisionManager.GameObjects;
            angle += 2;
        }
        count++;
    }
}
