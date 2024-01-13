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
    public Background Background { get; set; }
    public SoundPlayer Sound { get; set; }
    public Player Player { get; set; }
    public List<Boss> BossList { get; set; } = new List<Boss>();

    //TODO Para carregamento do jogo
    // private Game(string text) => this.SomeProperty = text;

    public void StartSound() => Sound.Play();

    public void StartBackground(Graphics g, PictureBox pb) => Background.Draw(g, pb);
    public void TestColision (Hittable hit1, Hittable hit2)
    {
        if (hit1.Hitbox.IntersectsWith(hit2.Hitbox))
        {
            var x = hit2.X - hit2.Old_X;
            var y = hit2.Y - hit2.Old_Y;

            if (x > 0)
            {
                hit2.X = hit2.Old_X - 1;
                hit1.X += 5;
            }
            if (x < 0)
            {
                hit2.X = hit2.Old_X + 1;
                hit1.X -= 5;

            }
            
            if (y > 0)
            {
                hit2.Y = hit2.Old_Y - 1;
                hit1.Y += 5;

            }
            if (y < 0)
            {
                hit2.Y = hit2.Old_Y + 1;
                hit1.Y -= 5;
            }
        }

    }

    public static void New() => crr = new Game();

    //TODO Para carregamento do jogo
    // public static void New(string text) => crr = new Singleton(text);
}
