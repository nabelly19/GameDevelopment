using System;

public class DeadState : State {
    public override void Act(Boss boss)
    {
        if (!boss.isAlive)
            return;
    }
}