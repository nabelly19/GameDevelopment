using System;

public class XProjectileState : State
{
    DateTime? dt = null;
    Player Player;
    int count = 0;
    bool updatedPosition = false;
    bool inverted = false;
    float x1 = 0;
    float x2 = 0;
    float y = 0;
    public XProjectileState(Player player)
    {
        this.Player = player;
    }

    public override void Act(Boss boss)
    {
        if (!updatedPosition)
        {
            changeLocation();
            return;
        }

        if (count == 10)
        {
            dt = null;
            count = 0;
            updatedPosition = false;
            GoToNext();
            return;
        }

        dt ??= DateTime.Now;

        if (DateTime.Now < dt?.AddSeconds(0.5))
            return;

        count++;
        dt = null;

        x1 += 30;
        x2 -= 30;
        if (inverted)
            y -= 30;
        else
            y += 30;

        GameEngine.Current.AddObjectToCollisionList(
            new XProjectile("Bullet", x1, y, Resources.X[0], 30, boss)
        );
        GameEngine.Current.AddObjectToCollisionList(
            new XProjectile("Bullet", x2, y, Resources.X[0], 30, boss)
        );
    }

    private void changeLocation()
    {   
        inverted = false;
        var num = 150;
        this.x1 = this.Player.X - num;
        this.x2 = this.Player.X + num;
        var rand = Random.Shared.Next(0, 2);
        if (rand == 1)
        {
            num *= -1;
            inverted = true;
            this.y = this.Player.Y - num;
        }
        else
            this.y = this.Player.Y - num;

        updatedPosition = true;
    }
}