public class LichTheHottes : Boss
{
    public LichTheHottes(int x, int y) 
    : base("Lich, The Hottes", x, y, "../../../assets/Sprites/Bosses/Feiticeira/litch.png")
    {
        var w3 = new WaitState(4); // 5 segundos
        this.Manager.AddContext(
            w3
        );

        this.Manager.AddList(w3);
    }
}