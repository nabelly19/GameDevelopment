using System;
using System.Collections.Generic;

public class TrackingProjectileStateLich : State
{
    DateTime? dt = null;
    int count = 0;
    Player player;
    List<GameObject> Owners = new();
    List<WallMoveable> Spawns = new();
    bool gotOwners = false;

    public TrackingProjectileStateLich(Player player)
    {
        this.player = player;
    }

    public TrackingProjectileStateLich(Player player, params WallMoveable[] spawnpoints)
    {
        this.player = player;
        foreach (var wall in spawnpoints)
        {
            this.Spawns.Add(wall);
        }
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
        if (count == 3)
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


        foreach (var spawn in Spawns)
        {
            GameEngine.Current.AddObjectToCollisionList(
                new TrackingProjectile("Bullet", spawn.X, spawn.Y, Resources.Tracking[0], this.Owners, this.player, 30, boss)
            );
            
        }
    }
}