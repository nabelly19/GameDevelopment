using System;
using System.Drawing;
using System.Windows.Forms;

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