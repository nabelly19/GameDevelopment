public class LichTheHottes : Boss
{
    public LichTheHottes(int x, int y) 
    : base("Lich, The Hottes", x, y, "../../../assets/Sprites/Bosses/Feiticeira/L_0.png")
    {
        var w3 = new WaitState(4); // 5 segundos

        var s1 = new TrackingProjectileStateLich(GameEngine.Current.Player);
        var s2 = new CircularlWaveState();
        var s3 = new XProjectileState(GameEngine.Current.Player);
        this.Manager.AddContext(
            w3,
            s1, s2, s3
            
        );

        // this.Manager.AddList(w3);
        // this.Manager.AddList(s1);
        // this.Manager.AddList(s2);
        this.Manager.AddList(s3);
    }

    public LichTheHottes(int x, int y, params WallMoveable[] movableWalls) 
    : base("Lich, The Hottes", x, y, "../../../assets/Sprites/Bosses/Feiticeira/L_0.png")
    {
        var w3 = new WaitState(4); // 5 segundos

        var s1 = new TrackingProjectileStateLich(GameEngine.Current.Player, movableWalls);
        var s2 = new CircularlWaveState();
        var s3 = new XProjectileState(GameEngine.Current.Player);
        this.Manager.AddContext(
            w3,
            s1, s2, s3
            
        );

        s1.SetNextState(w3);
        s2.SetNextState(w3);

        // this.Manager.AddList(w3);
        // this.Manager.AddList(s1);
        // this.Manager.AddList(s2);
        this.Manager.AddList(s3);
    }
}