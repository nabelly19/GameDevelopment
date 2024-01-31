using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Windows.Forms;

public static class CollisionManager
{
    public static List<GameObject> GameObjects { get; set; }

    public static void AddGameObject(GameObject gameObject) => GameObjects.Add(gameObject);

    public static void RemoveGameObject(GameObject gameObject) => GameObjects.Remove(gameObject);

    public static bool CheckCollisions(GameObject obj)
    {
        for (int j = 0; j < GameObjects.Count; j++)
        {
            GameObject other = GameObjects[j];

            if (other == obj)
                continue;

            if (collisionDetected(obj, other) && other.isHittable)
                return true;
        }
        return false;
    }

    public static IEnumerable<GameObject> GetCollisions(GameObject obj)
    {
        for (int j = 0; j < GameObjects.Count; j++)
        {
            GameObject other = GameObjects[j];
            if (other == obj)
                continue;
            if (collisionDetected(obj, other) && other.isHittable)
                yield return other;
        }
    }

    private static bool collisionDetected(GameObject obj1, GameObject obj2) =>
        obj2.Hitbox.IntersectsWith(obj1.Hitbox);

    public static bool CheckCollisionbyPoint(RectangleF hitbox, PointF p) => hitbox.Contains(p);

    public static bool ScreenColision(GameObject obj)
    {
        var Xmax = Screen.PrimaryScreen.Bounds.Width;
        var Ymax = Screen.PrimaryScreen.Bounds.Height;
        if (
            obj.X + obj.Hitbox.Width / 2 > Xmax
            || obj.X - obj.Hitbox.Width / 2 < 0
            || obj.Y + obj.Hitbox.Height / 2 > Ymax
            || obj.Y - obj.Hitbox.Height / 2 < 0
        )
            return true;

        return false;
    }

    public static void SetGameobjects(List<GameObject> list)
    {
        GameEngine.Current.AddPlayer(list);
        GameObjects = list.ToList();
    }

    public static void ResetList() => GameObjects = new List<GameObject>();
}