using System;
using System.CodeDom;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

Bitmap bmp = null;
Graphics g = null;
var pb = new PictureBox { Dock = DockStyle.Fill, };

GameEngine.New();
GameEngine.Current.StartUp(pb);

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
    GameEngine.Current.StartSound();
};

DateTime lastchecked = DateTime.Now;
float fps = 0;

timer.Tick += (o, e) =>
{
    fps = (int)(1 / (float)(DateTime.Now - lastchecked).TotalSeconds);
    lastchecked = DateTime.Now;
    g.Clear(Color.Black);

    MapManager.Current.RenderMapOrFade(g, pb);
    GameEngine.Current.Update();
    GameEngine.Current.Render(g, pb);
    g.DrawString($"FPS: {fps.ToString()}", SystemFonts.DefaultFont, Brushes.White, 10, 10);
  
    pb.Refresh();
};

form.KeyDown += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.Escape:
            Application.Exit();
            break;

        case Keys.I:
            // GameEngine.Current.Player.Info();
            GameEngine.Current.Player.Weapon.WindBlade = !GameEngine.Current.Player.Weapon.WindBlade;
            
            break;

        case Keys.W:
            GameEngine.Current.Player.MoveUp();
            break;

        case Keys.A:

            GameEngine.Current.Player.MoveLeft();
            break;

        case Keys.S:
            GameEngine.Current.Player.MoveDown();
            break;

        case Keys.D:
            GameEngine.Current.Player.MoveRight();
            break;

        case Keys.Space:
            GameEngine.Current.Player.Attack();
            break;
        case Keys.L:
            // CollisionManager.New();
            // CollisionManager.Current.AddGameObject(engine.player);
            break;
        
        case Keys.Y:
            MapManager.Current.PrevMap();
            break;
        case Keys.T:
            MapManager.Current.nextMap();
            // CollisionManager.New();
            // CollisionManager.Current.AddGameObject(GameEngine.Current.Player);
            break;
        case Keys.K:
            GameEngine.Current.AddObject(new RotateBeam("Bullet", 200, 200, 50, 50, 90, GameEngine.Current.Player));
            break;
    }
};

form.KeyUp += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.W:
            GameEngine.Current.Player.Ay = 0;
            break;

        case Keys.A:
            GameEngine.Current.Player.Ax = 0;
            break;

        case Keys.S:
            GameEngine.Current.Player.Ay = 0;
            break;

        case Keys.D:
            GameEngine.Current.Player.Ax = 0;
            break;

        case Keys.Space:
            break;
    }
};

Application.Run(form);
