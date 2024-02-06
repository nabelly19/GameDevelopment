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
    float lifetime;
    Platform platform;
    int random;
    bool isActivated = false;
    PointF[] spawns =
    {
        new PointF(
            Screen.PrimaryScreen.Bounds.Width * 0.2421875f,
            Screen.PrimaryScreen.Bounds.Height * 0.361111111f
        ),
        new PointF(
            Screen.PrimaryScreen.Bounds.Width * 0.2421875f,
            Screen.PrimaryScreen.Bounds.Height * 0.8425925826f
        ),
        new PointF(
            Screen.PrimaryScreen.Bounds.Width * 0.7552083333f,
            Screen.PrimaryScreen.Bounds.Height * 0.361111111f
        ),
        new PointF(
            Screen.PrimaryScreen.Bounds.Width * 0.7552083333f,
            Screen.PrimaryScreen.Bounds.Height * 0.8425925826f
        ),
    };

    public PlatformState(float lifetime)
    {
        this.lifetime = lifetime;
    }

    public override void Act(Boss boss)
    {
        if (!isActivated)
        {
            random = Random.Shared.Next(0, 2);
            if (GameEngine.Current.Player.X > Screen.PrimaryScreen.Bounds.Width / 2)
                random += 2;

            this.platform = new(
                "Platform",
                spawns[random].X,
                spawns[random].Y,
                Screen.PrimaryScreen.Bounds.Width * 0.1510416667f,
                Screen.PrimaryScreen.Bounds.Height * 0.2407407407f
            );

            creationTime = DateTime.Now;
            platform.creationTime = creationTime;
            GameEngine.Current.AddObjectToCollisionList(platform);
            isActivated = true;
        }

        if (random == 0 && platform.isAttaking)
            MapManager.Current.SetBackground(Resources.Maps[2]);

        else if (random == 1 && platform.isAttaking)
            MapManager.Current.SetBackground(Resources.Maps[4]);

        else if (random == 2 && platform.isAttaking)
            MapManager.Current.SetBackground(Resources.Maps[3]);

        else if (random == 3 && platform.isAttaking)
            MapManager.Current.SetBackground(Resources.Maps[5]);

        this.durationTime = this.lifetime + platform.damageInitiationDelay;
        var diff = DateTime.Now - creationTime;
        var millis = (float)diff.TotalMilliseconds;
        if (millis > durationTime)
        {
            random = 0;
            isActivated = false;
            MapManager.Current.SetBackground(Resources.Maps[1]);
            CollisionManager.RemoveGameObject(platform);
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
