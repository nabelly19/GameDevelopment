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
    public Background Background    { get; set; }
    public SoundPlayer Sound        { get; set; }
    public List<Boss> BossList      { get; set; } = new List<Boss>();

    //TODO Para carregamento do jogo
    // private Game(string text) => this.SomeProperty = text;

    public void StartSound() 
        => Sound.Play();
    
    public void StartBackground(Graphics g, PictureBox pb) 
        => Background.Draw(g, pb);
    public static void New() => crr = new Game();

    //TODO Para carregamento do jogo
    // public static void New(string text) => crr = new Singleton(text);
}
