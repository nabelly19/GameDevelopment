using System;
using System.CodeDom;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

Bitmap bmp = null;
Graphics g = null;
var pb = new PictureBox { Dock = DockStyle.Fill, };

Resources.New();
CollisionManager.New();
GameEngine engine = new();
engine.StartUp(pb);


var timer = new Timer { Interval = 1000 / 60, };

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

DateTime lastchecked = DateTime.Now;
float fps =0;


timer.Tick += (o, e) =>
{
    fps = (int)(1/(float)(DateTime.Now-lastchecked).TotalSeconds);
    lastchecked = DateTime.Now;
    g.Clear(Color.Black);
    engine.Update();
    engine.Render(g, pb);
    g.DrawString($"FPS: {fps.ToString()}", SystemFonts.DefaultFont, Brushes.White, 10, 10);
    pb.Refresh();
    
};


//295,4 467,93552

form.KeyDown += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.Escape:
            Application.Exit();
            break;

        case Keys.I:
            engine.player.Info();
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

        case Keys.Space:
            engine.player.Attack();
            break;
        case Keys.L:
            CollisionManager.New();
            CollisionManager.Current.AddGameObject(engine.player);
            break;
        case Keys.K:
            break;

    }
};

form.KeyUp += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.W:
            engine.player.Ay = 0;
            break;

        case Keys.A:
            engine.player.Ax = 0;
            break;

        case Keys.S:
            engine.player.Ay = 0;
            break;

        case Keys.D:
            engine.player.Ax = 0;
            break;
        
        case Keys.Space:
            break;
    }
};

Application.Run(form);
