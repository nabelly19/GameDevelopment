public class FelixTheToad : Boss
{
    public int Steps { get; set; } = 0;
    public int SlowFrameRate { get; set; } = 0;
    public FelixTheToad(int x, int y)
        // : base("Felix, the Toad", x, y, "./assets/Sprites/Bosses/Felix/F_0.pn")
        : base("Felix, the Toad", x, y, "../../../assets/Sprites/Bosses/Felix/F_0.png")
    {
        var s1 = new SpiralProjectileState();
        var s2 = new WaitState();
        var s3 = new CircularlWaveState();
        var s4 = new SpiralWaveState();
        var s5 = new TrackingProjectileState(GameEngine.Current.Player);
        var s6 = new PlatformState(3_500);

        var w1 = new WaitState();
        var w2 = new WaitState();

        var c2 = new SpiralWaveState();
        // var c3 = new CircularlWaveState();

        w1.SetContext(Manager);
        w2.SetContext(Manager);
        c2.SetContext(Manager);
        s5.SetContext(Manager);

        c2.SetNextState(w1);
        w1.SetNextState(s5);
        // c3.SetNextState(w2);

        this.Manager.AddList(w1);
        // this.Manager.AddList(s1);
        // this.Manager.AddList(s2);
        // this.Manager.AddList(s3);
        // this.Manager.AddList(s4);
        // this.Manager.AddList(s5);
        this.Manager.AddList(s6);
        this.isVulnerable = true;
    }

    public override void Update()
    {
        base.Update();
        if (Manager.Current is WaitState)
            AnimateBoss(0,3);
        else if (Manager.Current is VulnerabilityState)
            AnimateBoss(5,7);
        else if (Manager.Current is PlatformState)
            AnimateBoss(10,13);
        else
            AnimateBoss(0,3);
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
}
