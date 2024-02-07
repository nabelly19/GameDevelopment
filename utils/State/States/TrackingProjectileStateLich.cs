using System;
using System.Collections.Generic;

public class TrackingProjectileStateLich : State
{
    DateTime? dt = null;
    int count = 0;
    Player player;
    List<GameObject> Owners = new();
    bool gotOwners = false;

    public TrackingProjectileStateLich(Player player)
    {
        this.player = player;

    }

    public override void Act(Boss boss)
    {
        if (!gotOwners)
        {
            foreach (var item in CollisionManager.GameObjects)
            {
                if (item is WallMoveable)
                    this.Owners.Add(item);
            }
        }
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
            new TrackingProjectile("Bullet", boss.X, boss.Y, 25, 25, this.Owners, this.player, 30, boss)
        );
    }
}