using System;
using System.Drawing;
using System.Windows.Forms;

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

        for (int i = 0; i < 3; i++)
        {
            GameEngine.Current.AddObjectToCollisionList(
                new SpiralProjectile(
                    "Bullet",
                    boss.X - 14,
                    boss.Y - 50,
                    Random.Shared.Next(0, 89),
                    boss
                )
            );
            GameEngine.Current.AddObjectToCollisionList(
                new SpiralProjectile(
                    "Bullet",
                    boss.X - 14,
                    boss.Y - 50,
                    Random.Shared.Next(90, 179),
                    boss
                )
            );
            GameEngine.Current.AddObjectToCollisionList(
                new SpiralProjectile(
                    "Bullet",
                    boss.X - 14,
                    boss.Y - 50,
                    Random.Shared.Next(180, 269),
                    boss
                )
            );
            GameEngine.Current.AddObjectToCollisionList(
                new SpiralProjectile(
                    "Bullet",
                    boss.X - 14,
                    boss.Y - 50,
                    Random.Shared.Next(270, 359),
                    boss
                )
            );
        }
    }
}

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

public class SpiralWaveState : State
{
    private float angle = 0;
    DateTime? dt = null;

    public override void Act(Boss boss)
    {
        if (angle >= 180)
        {
            GameEngine.Current.AddObjectToCollisionList(
                new Wave("Bullet", boss.X - 14, boss.Y - 50, 25, 25, angle, boss)
            );
            angle = 0;
            GoToNext();
            return;
        }

        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(2.5))
            return;

        GameEngine.Current.AddObjectToCollisionList(
            new Wave("Bullet", boss.X - 14, boss.Y - 50, 25, 25, angle, boss)
        );

        // 4 = 360 mais espacado
        // > 1 = 360
        // 1 = 180
        angle += 1;
    }
}

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
                new Wave("Bullet", boss.X, boss.Y, 25, 25, angle, boss)
            );
            angle += 2;
        }
        count++;
    }
}

public class EnchantState : State
{
    public override void Act(Boss boss)
    {
        throw new NotImplementedException();
    }
}

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

public class PlatformState : State
{
    float durationTime;
    DateTime creationTime = DateTime.Now;
    Platform platform;
    bool isActivated = false;
    PointF[] spawns =
    {
        new PointF(465, 390),
        new PointF(465, 910),
        new PointF(1450, 390),
        new PointF(1450, 910),
    };

    public PlatformState(float lifetime)
    {
        var random = Random.Shared.Next(0, 2);
        platform = new("Platform", spawns[random].X, spawns[random].Y, 290, 260);
        if (GameEngine.Current.Player.X > Screen.PrimaryScreen.Bounds.Width / 2)
            platform = new("Platform", spawns[random + 2].X, spawns[random + 2].Y, 290, 260);
        this.durationTime = lifetime + platform.damageInitiationDelay;
    }

    public override void Act(Boss boss)
    {
        int contador = 2;

        if (!isActivated)
        {
            creationTime = DateTime.Now;
            platform.creationTime = DateTime.Now;
            GameEngine.Current.AddObjectToCollisionList(platform);
            isActivated = true;
        }
        var diff = DateTime.Now - creationTime;
        var millis = (float)diff.TotalMilliseconds;

        if (millis > durationTime)
        {
            isActivated = false;
            CollisionManager.RemoveGameObject(platform);
            var random = Random.Shared.Next(0, 2);
            platform = new("Platform", spawns[random].X, spawns[random].Y, 290, 260);
            if (GameEngine.Current.Player.X > Screen.PrimaryScreen.Bounds.Width / 2)
                platform = new("Platform", spawns[random + 2].X, spawns[random + 2].Y, 290, 260);

            if (random == 0)
                MapManager.Current.SetBackground(Resources.Maps[2]);


            if (random == 1 + contador)
                MapManager.Current.SetBackground(Resources.Maps[3]);

            if (random == 0 + contador)
                MapManager.Current.SetBackground(Resources.Maps[4]);


            if (random == 1)
                MapManager.Current.SetBackground(Resources.Maps[5]);


            GoToNext();
            return;
        }
    }
}

public class VulnerabilityState : State
{
    public override void Act(Boss boss)
    {
        throw new System.NotImplementedException();
    }
}
