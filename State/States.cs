using System;

public class MovingState : State
{
    public override void Act()
    {
        throw new System.NotImplementedException();
    }
}

public class BulletState : State
{
    DateTime? dt = null;
    int count = 0;
    public override void Act()
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
        GameEngine.Current.AddObject(new Bullet("Bullet", 50, 50, 50, 50));
    }
}

public class WaitState : State
{
    DateTime? dt = null;
    public override void Act()
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
    public override void Act()
    {
        throw new System.NotImplementedException();
    }
}

public class PlataformState : State
{
    public override void Act()
    {
        throw new System.NotImplementedException();
    }
}

public class VulnerabilityState : State
{
    public override void Act()
    {
        throw new System.NotImplementedException();
    }
}