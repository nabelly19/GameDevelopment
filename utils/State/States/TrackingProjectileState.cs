using System;

public class TrackingProjectileState : State
{
    DateTime? dt = null;
    int count = 0;
    Player player;

    public TrackingProjectileState(Player player)
    {
        this.player = player;
    }

    public override void Act(Boss boss)
    {
        if (count == 1)
        {
            dt = null;
            count = 0;
            GoToNext();
            return;
        }

        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(1))
            return;

        count++;
        dt = null;

        GameEngine.Current.AddObjectToCollisionList(
            new TrackingProjectile("Bullet", boss.X, boss.Y, 25, 25, this.player, 30, boss)
        );
    }
}