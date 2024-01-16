using System;
using System.CodeDom;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
ApplicationConfiguration.Initialize();

Bitmap bmp = null;
Graphics g = null;

GameEngine engine = new();
engine.AddObject(new Boss("Ele", 500, 500, "./assets/Sprites/Bosses/pxArt.png"));
engine.AddObject(new Player("Ele", 0, 0, "./assets/Sprites/Player/download.png"));


var pb = new PictureBox { Dock = DockStyle.Fill, };

var timer = new Timer { Interval = 20, };

var form = new Form
{
    WindowState = FormWindowState.Maximized,
    FormBorderStyle = FormBorderStyle.None,
    Text = "Rogue-Like",
    Controls = { pb }
};

form.Load += (o, e) =>
{
    bmp = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bmp);
    g.Clear(Color.Black);
    pb.Image = bmp;
    timer.Start();
    engine.StartSound();
};

timer.Tick += (o, e) =>
{
    g.Clear(Color.Black);
    engine.Render(g, pb);

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
            engine.player.MoveUp();
            break;

        case Keys.A:

            engine.player.MoveLeft();
            break;

        case Keys.S:
            engine.player.MoveDown();
            break;

        case Keys.D:
            engine.player.MoveRight();
            break;

        // case Keys.Space:
        //     Game.Current.CurrentMap.UpdateBackground("./Midia/Maps/dungeon_pre.png");
        //     break;
    }
};

form.KeyUp += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.W:
            engine.player.Vy = 0;
            break;

        case Keys.A:
            engine.player.Vx = 0;
            break;

        case Keys.S:
            engine.player.Vy = 0;
            break;

        case Keys.D:
            engine.player.Vx = 0;
            break;
    }
};

Application.Run(form);
