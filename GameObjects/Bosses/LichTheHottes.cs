public class LichTheHottes : Boss
{
    public LichTheHottes(int x, int y) 
    : base("Lich, The Hottes", x, y, "../../../assets/Sprites/Bosses/Feiticeira/litch.png")
    {
        var w3 = new WaitState(4); // 5 segundos

        var s1 = new TrackingProjectileStateLich(GameEngine.Current.Player);

        this.Manager.AddContext(
            w3,
            s1
            
        );

        // this.Manager.AddList(w3);
        this.Manager.AddList(s1);
    }
}