using System;

public class WaitState : State
{
    DateTime? dt = null;
    int AddTime = 0;
    public WaitState() { }
    public WaitState(int addTime)
        => this.AddTime = addTime;

    public override void Act(Boss boss)
    {
        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(1 + this.AddTime))
            return;

        dt = null;
        GoToNext();
    }
}