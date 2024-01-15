using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Game
{
    // Singleton
    private static Game crr = new Game();
    public static Game Current => crr;

    private Game() { }

    // Propriedades do jogo (bg, player, sound)
    public Background Background{ get; set; }
    public SoundPlayer Sound    { get; set; }
    public Player Player        { get; set; }
    public List<Boss> BossList  { get; set; } = new List<Boss>();
    public List<Map> Dungeons   { get; private set; } = new List<Map>();
    public Map CurrentMap       { get; set; }
    public Colision Colision = new Colision();

    //TODO Para carregamento do jogo
    // private Game(string text) => this.SomeProperty = text;

    public void StartSound() => Sound.Play();
    public void StartBackground(Graphics g, PictureBox pb) => Background.Draw(g, pb);
    public void PlayerBossColision (Boss boss, Player player)
    {
        Colision.BossPlayer(boss, player);
        // if (boss.Hitbox.IntersectsWith(player.Hitbox))
        // {
        //     var x = player.X - player.Old_X;
        //     var y = player.Y - player.Old_Y;

        //     if (x > 0)
        //     {
        //         player.X = player.Old_X - 1;
        //         boss.X += 5;
        //     }
        //     if (x < 0)
        //     {
        //         player.X = player.Old_X + 1;
        //         boss.X -= 5;

        //     }
            
        //     if (y > 0)
        //     {
        //         player.Y = player.Old_Y - 1;
        //         boss.Y += 5;

        //     }
        //     if (y < 0)
        //     {
        //         player.Y = player.Old_Y + 1;
        //         boss.Y -= 5;
        //     }
        // }

    }

    public void CreateDungeons (Graphics g)
    {
        var dg1 = new Dungeon_01("./Midia/Maps/widen_1220x0.png");
        this.Dungeons.Add(dg1);
        this.CurrentMap = dg1;
    }

    public void DrawMap (Graphics g, PictureBox pb)
        => this.CurrentMap.Draw(g, pb);
    

    public static void New() => crr = new Game();

    //TODO Para carregamento do jogo
    // public static void New(string text) => crr = new Singleton(text);
}
