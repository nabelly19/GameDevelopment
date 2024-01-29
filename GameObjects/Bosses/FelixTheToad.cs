public class FelixTheToad : Boss
{
    public FelixTheToad(int x, int y)
        // : base("Felix, the Toad", x, y, "./assets/Sprites/Bosses/Felix/felix.png")
        : base("Felix, the Toad", x, y, "../../../assets/Sprites/Bosses/Felix/felix.png")
    {
        var s1 = new SpiralProjectileState();
        var s2 = new WaitState();
        var s3 = new CircularlWaveState();
        var s4 = new SpiralWaveState();

        var w1 = new WaitState();
        var w2 = new WaitState();
        var c2 = new SpiralWaveState();
        var c3 = new CircularlWaveState();

        w1.SetContext(Manager);
        w2.SetContext(Manager);
        c2.SetContext(Manager);
        c3.SetContext(Manager);
        c2.SetNextState(w1);
        w1.SetNextState(c3);
        c3.SetNextState(w2);

        this.Manager.AddList(c2);

        this.Manager.AddList(s1);
        // this.Manager.AddList(s2);
        // this.Manager.AddList(s3);
        // this.Manager.AddList(s4);
    }
}
