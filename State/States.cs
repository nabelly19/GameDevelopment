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

public class WaitState : State
{
    DateTime? dt = null;

    public override void Act(Boss boss)
    {
        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(4))
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
