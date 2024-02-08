using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public static class HUD
{
    public static List<GameObject> StaticObjs { get; set; } = new(); 
    public static List<GameObject> Objs { get; private set; } = new();

    public static void AddObject(GameObject obj) => Objs.Add(obj);
    public static void AddStaticObject(GameObject sobj) => StaticObjs.Add(sobj);

    public static void Reset() => Objs = new();

    public static void Render(Graphics g, PictureBox pb)
    {
        // if (StaticObjs.Count == 0)
        //     return;
        // foreach (var obj in StaticObjs)
        //     obj.Render(g, pb);
        if (Objs.Count == 0)
            return;
        foreach (var obj in Objs)
            obj.Render(g, pb);
    }

    public static void Update()
    {
        if (Objs.Count == 0)
            return;
        foreach (var obj in Objs.ToList())
            obj.Update();
    }
}
