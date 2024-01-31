using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public static class HUD
{
    public static GameObject Obj { get; private set; }

    public static void SetObject(GameObject obj)
        => Obj = obj;
    public static void Reset()
        => Obj = null;
    public static void Render(Graphics g, PictureBox pb)
    {
        if (Obj is null)
            return;
        Obj.Render(g, pb);
    }

}