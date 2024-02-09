using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

Bitmap bmp = null;
Graphics g = null;
var pb = new PictureBox { Dock = DockStyle.Fill, };

GameEngine.New();
GameEngine.Current.StartUp();

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
    g.InterpolationMode = InterpolationMode.NearestNeighbor;
    g.Clear(Color.Black);
    pb.Image = bmp;
    timer.Start();
};

DateTime lastchecked = DateTime.Now;
float fps = 0;

timer.Tick += (o, e) =>
{
    fps = (int)(1 / (float)(DateTime.Now - lastchecked).TotalSeconds);
    lastchecked = DateTime.Now;
    g.Clear(Color.Black);
    if (StartScreen.goStart)
    {
        GameEngine.Current.Update();
        GameEngine.Current.Render(g, pb);
    }
    else
        StartScreen.Render(g, pb);
    g.DrawString($"FPS: {fps.ToString()}", SystemFonts.DefaultFont, Brushes.White, 10, 10);

    pb.Refresh();
};

form.KeyDown += (o, e) =>
{
    if (!StartScreen.goStart)
    {
        StartScreen.SetgoStart();
        return;
    }
    switch (e.KeyCode)
    {
        case Keys.Escape:
            Application.Exit();
            break;

        // case Keys.I:
        //     // GameEngine.Current.Player.Info();
        //     // MessageBox.Show(CollisionManager.GameObjects.Count.ToString());
        //     // MessageBox.Show(Cursor.Position.X.ToString());
        //     // MessageBox.Show(Cursor.Position.Y.ToString());
        //     break;

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

        // case Keys.R:
        //     GameEngine.Current.Player.Hp = GameEngine.Current.Player.baseHp;
        //     break;

        case Keys.Q:
            GameEngine.Current.Player.Hp = 1;
            GameEngine.Current.Player.ReceiveDamage();
            break;

        case Keys.F:
            GameEngine.Current.Player.Interact();
            break;

        case Keys.Space:
            GameEngine.Current.Player.Attack();
            break;

        // case Keys.Y:
        //     MapManager.PreviousMap();
        //     break;
        // case Keys.T:
        //     MapManager.NextMap();
        //     break;
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

pb.MouseDown += (o, e) =>
{
    HUD.Update();
};

Application.Run(form);
