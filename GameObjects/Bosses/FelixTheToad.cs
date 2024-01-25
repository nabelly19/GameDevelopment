public class FelixTheToad : Boss
{
    public FelixTheToad(int x, int y)
        : base("Felix, the Toad", x, y, "./assets/Sprites/Bosses/Felix/felix.png")
        // : base("Felix, the Toad", x, y, "../../../assets/Sprites/Bosses/Felix/felix.png")
    {
        var s1 = new SpiralProjectileState();
        var s2 = new WaitState();
        this.Manager.AddList(s1);
        this.Manager.AddList(s2);
    }
}
