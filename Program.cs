using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

Bitmap bmp = null;
Graphics g = null;

Game.Current.Background = new Background();
Game.Current.Sound = new SoundPlayer();
Game.Current.BossList.Add(new Boss("./Midia/Sprites/Bosses/pxArt.png", 10, 10));
Game.Current.Player = new Player("./Midia/Sprites/Player/download.png", 25, 25);

var pb = new PictureBox { Dock = DockStyle.Fill, };

var timer = new Timer { Interval = 20, };

var form = new Form
{
    WindowState = FormWindowState.Maximized,
    FormBorderStyle = FormBorderStyle.None,
    Controls = { pb }
};

form.Load += (o, e) =>
{
    bmp = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bmp);
    g.Clear(Color.Black);
    pb.Image = bmp;
    timer.Start();
    Game.Current.Sound.Play();
};

timer.Tick += (o, e) =>
{
    g.Clear(Color.Black);
    Game.Current.Background.Draw(g, pb);
    Game.Current.BossList[0].Draw(g);
    Game.Current.Player.Draw(g);
    Game.Current.Player.Move();

    pb.Refresh();
};

form.KeyDown += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.Escape:
            Application.Exit();
            break;

        case Keys.W:
            Game.Current.Player.MoveUp();
            break;

        case Keys.A:

            Game.Current.Player.MoveLeft();
            break;

        case Keys.S:
            Game.Current.Player.MoveDown();
            break;

        case Keys.D:
            Game.Current.Player.MoveRight();
            break;
    }
};

form.KeyUp += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.W:
            Game.Current.Player.Velocity_Y = 0;
            break;

        case Keys.A:
            Game.Current.Player.Velocity_X = 0;
            break;

        case Keys.S:
            Game.Current.Player.Velocity_Y = 0;
            break;

        case Keys.D:
            Game.Current.Player.Velocity_X = 0;
            break;
    }
};

Application.Run(form);
