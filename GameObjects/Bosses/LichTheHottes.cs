public class LichTheHottes : Boss
{
    public int Steps { get; set; } = 0;
    public int SlowFrameRate { get; set; } = 0;
    public LichTheHottes(int x, int y) 
    : base("Lich, The Hottes", x, y, "../../../assets/Sprites/Bosses/Feiticeira/L_0")
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
    : base("Lich, The Hottes", x, y, "../../../assets/Sprites/Bosses/Feiticeira/L_0)")
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

    public override void Update()
    {
        
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