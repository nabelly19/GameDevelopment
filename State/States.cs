using System;

public class MovingState : State
{
    public override void Act(Boss boss)
    {
        throw new System.NotImplementedException();
    }
}

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

        for (int i = 0; i < 8; i++)
        {
            GameEngine.Current.AddObject(
                new SpiralProjectile("Bullet", 50, 50, 25, 25, Random.Shared.Next(0, 89), boss)
            );
            GameEngine.Current.AddObject(
                new SpiralProjectile("Bullet", 50, 50, 25, 25, Random.Shared.Next(90, 179), boss)
            );
            GameEngine.Current.AddObject(
                new SpiralProjectile("Bullet", 50, 50, 25, 25, Random.Shared.Next(180, 269), boss)
            );
            GameEngine.Current.AddObject(
                new SpiralProjectile("Bullet", 50, 50, 25, 25, Random.Shared.Next(270, 359), boss)
            );
        }
    }
}

public class SpiralWaveState : State
{
    private float angle = 0;
    DateTime? dt = null;
    int count = 0;
    int index = 0;

    public override void Act(Boss boss)
    {
        if (angle >= 180)
        {
            GameEngine.Current.AddObject(new Wave("Bullet", boss.X, boss.Y, 25, 25, angle, boss));
            angle = 0;
            index = 0;
            count = 0;
            GoToNext();
            return;
        }

        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(1.5))
            return;

        GameEngine.Current.AddObject(new Wave("Bullet", boss.X, boss.Y, 25, 25, angle, boss));

        // 4 = 360 mais espacado
        // > 1 = 360
        // 1 = 180
        angle += 5;
        count++;
    }
}

public class CircularlWaveState : State
{
    private float angle = 0;
    DateTime? dt = null;
    int count = 0;
    int index = 0;

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
            GameEngine.Current.AddObject(new Wave("Bullet", boss.X, boss.Y, 25, 25, angle, boss));
            // 4 = 360 mais espacado
            // > 1 = 360
            // 1 = 180
            angle += 2;
        }
        count++;
    }
}

// public class WaveState : State
// {
//     private float angle = 0;
//     DateTime? dt = null;
//     int count = 0;

//     public override void Act(Boss boss)
//     {
//         if (count == 3)
//         {
//             dt = null;
//             count = 0;
//             GoToNext();
//             return;
//         }

//         dt ??= DateTime.Now;

//         if (DateTime.Now < dt?.AddSeconds(2))
//             return;

//         count++;
//         dt = null;
//         for (int i = 0; i < 200; i++)
//         {
//             GameEngine.Current.AddObject(
//                 new Wave("Bullet", 50, 50, 25, 25, angle, boss)
//             );
//             angle++;
//         }
//         angle = 0;
//     }
// }

public class WaitState : State
{
    DateTime? dt = null;

    public override void Act(Boss boss)
    {
        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(2))
            return;

        dt = null;
        GoToNext();
    }
}

public class RayState : State
{
    public override void Act(Boss boss)
    {
        throw new System.NotImplementedException();
    }
}

public class PlataformState : State
{
    public override void Act(Boss boss)
    {
        throw new System.NotImplementedException();
    }
}

public class VulnerabilityState : State
{
    public override void Act(Boss boss)
    {
        throw new System.NotImplementedException();
    }
}
