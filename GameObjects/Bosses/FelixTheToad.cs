public class FelixTheToad : Boss
{
    public FelixTheToad(int x, int y)
        // : base("Felix, the Toad", x, y, "./assets/Sprites/Bosses/Felix/felix.png")
        : base("Felix, the Toad", x, y, "../../../assets/Sprites/Bosses/Felix/felix.png")
    {
        var s1 = new SpiralProjectileState();
        var s2 = new WaitState();
        // var s3 = new CircularlWaveState();
        var s4 = new SpiralWaveState();
        TrackingProjectileState s5 = new TrackingProjectileState(GameEngine.Current.Player);
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

        // this.Manager.AddList(w1);
        // this.Manager.AddList(s1);
        // this.Manager.AddList(s2);
        // this.Manager.AddList(s3);
        // this.Manager.AddList(s4);
        this.Manager.AddList(s5);
        // this.Manager.AddList(s6);

    }

    public override void Update()
    {
        base.Update();
        if (Manager.Current is WaitState || Manager.Current is VulnerabilityState || Manager.Current is PlatformState)
            setImage("../../../assets/Sprites/Bosses/Felix/felix.png");
        else
            setImage("../../../assets/Sprites/Bosses/Felix/felix2.png");
    }
}
