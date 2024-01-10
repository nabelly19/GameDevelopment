using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

Bitmap bmp = null;

Graphics g = null;

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
};

float x = 300, y = 300;
float vx = 0, vy = 0;

timer.Tick += (o, e) =>
{
    g.Clear(Color.Black);
    g.FillRectangle(
        Brushes.Red,
        new RectangleF
        {
            X = x - 5,
            Y = y - 5,
            Width = 10,
            Height = 10
        }
    );
    x += vx;
    y += vy;
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
            vy = -5;
            break;

        case Keys.A:
            vx = -5;
            break;

        case Keys.S:
            vy = 5;
            break;

        case Keys.D:
            vx = 5;
            break;
    }
};

form.KeyUp += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.W:
            vy = 0;
            break;

        case Keys.A:
            vx = 0;
            break;

        case Keys.S:
            vy = 0;
            break;

        case Keys.D:
            vx = 0;
            break;
    }
};

Application.Run(form);
