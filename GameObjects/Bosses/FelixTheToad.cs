public class FelixTheToad : Boss
{
    public FelixTheToad(int x, int y, string sprite) : base("Felix, the Toad", x, y, sprite)
    {
        var s1 = new BulletState();
        var s2 = new WaitState();
        this.Manager.AddList(s1);
        this.Manager.AddList(s2);
    }
}