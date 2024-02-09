public class FelixTheToad : Boss
{
    public int Steps { get; set; } = 0;
    public int SlowFrameRate { get; set; } = 0;

    public FelixTheToad(float x, float y)
        : base("Felix, the Toad", x, y, Resources.Felix[0])
    {
        // var s5 = new TrackingProjectileState(GameEngine.Current.Player);
        var s1 = new SpiralProjectileState();
        var s4 = new SpiralWaveState();
        var s6 = new PlatformState(3_500);
        // var s5 = new CircularlWaveState();
        var v1 = new VulnerabilityState();
        var w3 = new WaitState(4); // 5 segundos

        var c2 = new SpiralWaveState();
        var c1_0 = new SpiralProjectileState { isChain = true };
        var c1_1 = new SpiralWaveState();
        var c1_2 = new SpiralWaveState();
        var c1_3 = new VulnerabilityState(3);
        var c1_4 = new WaitState(2);

        this.Manager.AddContext(s1, s4, s6, v1, w3, c2, c1_0, c1_1, c1_2, c1_3, c1_4);

        s1.SetNextState(w3);
        s4.SetNextState(w3);
        s6.SetNextState(w3);
        c1_0.SetNextState(c1_1);
        c1_1.SetNextState(c1_2);
        c1_2.SetNextState(c1_3);
        c1_3.SetNextState(c1_4);

        this.Manager.AddList(s6);
        this.Manager.AddList(s4);
        this.Manager.AddList(s1);
        this.Manager.AddList(v1);
        this.Manager.AddList(c1_0);

        // this.Hp = 1;
        // this.isVulnerable = true;
    }

    public override void Update()
    {
        base.Update();
        if (Manager.Current is WaitState)
            AnimateBoss(0, 3);
        else if (Manager.Current is VulnerabilityState)
            AnimateBoss(5, 7);
        else if (Manager.Current is PlatformState)
            AnimateBoss(10, 13);
        else if (Manager.Current is DeadState)
        {
            foreach (var gameObject in CollisionManager.GameObjects)
            {
                if (gameObject is Platform)
                    CollisionManager.RemoveGameObject(gameObject);
            }

            this.Sprite = Resources.Felix[9];
            this.isAlive = false;
            MapManager.Current.SetBackground(Resources.Maps[7]);
        }
        else
            AnimateBoss(0, 3);
    }

    public void AnimateBoss(int start, int end)
    {
        SlowFrameRate += 1;

        if (SlowFrameRate > 6)
        {
            Steps++;
            SlowFrameRate = 0;
        }

        if (Steps > end || Steps < start)
            Steps = start;

        this.Sprite = Resources.Felix[Steps];
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
