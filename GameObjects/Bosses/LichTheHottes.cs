using System;

public class LichTheHottes : Boss
{
    public int Steps { get; set; } = 0;
    public int SlowFrameRate { get; set; } = 10;

    public LichTheHottes(float x, float y)
        : base("Lich, The Hottes", x, y, Resources.Litch[0])
    {
    }

    public LichTheHottes(float x, float y, params WallMoveable[] movableWalls)
        : base("Lich, The Hottes", x, y, Resources.Litch[0])
    {
        var w3 = new WaitState(4); // 5 segundos
        
        var v1 = new VulnerabilityState(5);

        var s1 = new TrackingProjectileStateLich(GameEngine.Current.Player, movableWalls);
        var s2 = new CircularlWaveState();
        var s3 = new XProjectileState(GameEngine.Current.Player);

        var c1_0 = new CircularlWaveState();
       
        // var c1_1 = new XProjectileState(GameEngine.Current.Player);
        // var c1_2 = new WaitState(2);
        // var c1_3 = new VulnerabilityState(3);

        this.Manager.AddContext(w3, s1, s2, s3);

        s1.SetNextState(s2);
        s2.SetNextState(w3);
        s3.SetNextState(w3);
        // c1_0.SetNextState(c1_1);
        // c1_1.SetNextState(c1_2);
        // c1_2.SetNextState(c1_3);

        this.Manager.AddList(s1);
        this.Manager.AddList(s2);
        this.Manager.AddList(s3);
        this.Manager.AddList(v1);
        this.Manager.AddList(c1_0);
   
    }

     public override void Update()
    {
        base.Update();
        if (Manager.Current is WaitState)
            AnimateBoss(0,6);
        else if (Manager.Current is VulnerabilityState)
            AnimateBoss(9,10);
        else if (Manager.Current is CircularlWaveState)
            AnimateBoss(7,8);
        else if (Manager.Current is DeadState)
            AnimateThisDeathBoss(11, 15);
        else
            AnimateBoss(0,6);
    }

    public void AnimateThisDeathBoss(int start, int end)
    {
        SlowFrameRate += 1;
        for (int i = 0; i < 5; i ++)
        {
            if (SlowFrameRate > 8)
            {
                Steps++;
                SlowFrameRate = 0;
            }
            if(Steps > 18)
                return;
            this.Sprite = Resources.Litch[Steps];
            
        }  
    }

    public void AnimateBoss(int start, int end)
    {
        SlowFrameRate += 1;

        if (SlowFrameRate > 8)
        {
            Steps++;
            SlowFrameRate = 0;
        }

        if (Steps > end || Steps < start)
            Steps = start;

        this.Sprite = Resources.Litch[Steps];
    }

    public override void ReceiveDamage()
    {
        this.Hp--;
        if (this.Hp == 2)
        {
            var w2 = new WaitState(2);
            foreach (var state in Manager.initialStateList)
            {
                if (!state.isChain)
                    state.SetNextState(w2);
            }
        }
        if (this.Hp == 1)
        {
            var w1 = new WaitState();
            foreach (var state in Manager.initialStateList)
            {
                if (!state.isChain)
                    state.SetNextState(w1);
            }
        }
        if (this.Hp <= 0)
        {
            this.Hp = 0;
            this.isAlive = false;
        }
    }
}