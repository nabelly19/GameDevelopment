using System;
using System.Drawing;
using System.Windows.Forms;

public partial class GameForm : Form
{
    Bitmap bmp = null;
    Graphics g = null;

    PictureBox pb = new PictureBox { Dock = DockStyle.Fill, };
    Timer timer = new Timer { Interval = 20, };

    public GameEngine Engine { get; set; }

    public GameForm()
    {
        StartUp();
    }

    private void StartUp()
    {
        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;
        Name = "GameForm";
        Text = "Rogue-Like";

        this.Controls.Add(pb);

        Load += GameForm_Load;
    }

    private void GameForm_Load(object sender, EventArgs e)
    {
        bmp = new Bitmap(pb.Width, pb.Height);
        g = Graphics.FromImage(bmp);
        g.Clear(Color.Black);
        pb.Image = bmp;
        timer.Start();
    }
}
