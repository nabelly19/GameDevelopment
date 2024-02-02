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

    private static bool collisionDetected(GameObject obj1, GameObject obj2) 
        => obj2.Hitbox.IntersectsWith(obj1.Hitbox);

    public static bool CheckCollisionByPoint(RectangleF hitbox, PointF p) 
        => hitbox.Contains(p);

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

    public static float GetCollisionAngle(GameObject obj1, GameObject obj2)
    {
        var upleft1 = new PointF(obj1.Hitbox.X, obj1.Hitbox.Y);
        var upright1 = new PointF(obj1.Hitbox.X + obj1.Hitbox.Width, obj1.Hitbox.Y);
        var bottomleft1 = new PointF(obj1.Hitbox.X, obj1.Hitbox.Y + obj1.Hitbox.Height);
        var bottomright1 = new PointF(
            obj1.Hitbox.X + obj1.Hitbox.Width,
            obj1.Hitbox.Y + obj1.Hitbox.Height
        );

        var upleft2 = new PointF(obj2.Hitbox.X, obj2.Hitbox.Y);
        var upright2 = new PointF(obj2.Hitbox.X + obj2.Hitbox.Width, obj2.Hitbox.Y);
        var bottomleft2 = new PointF(obj2.Hitbox.X, obj2.Hitbox.Y + obj2.Hitbox.Height);
        var bottomright2 = new PointF(
            obj2.Hitbox.X + obj2.Hitbox.Width,
            obj2.Hitbox.Y + obj2.Hitbox.Height
        );

        float vx = 0;
        float vy = 0;

        if ((obj1 is IMoveable ob1) && (obj2 is IMoveable ob2))
        {
            vx = ob1.Vx - ob2.Vx;
            vy = ob1.Vy - ob2.Vy;
        }

        if (obj1.Hitbox.Contains(upleft2) && obj1.Hitbox.Contains(bottomleft2))
        {
            // Bateu da direita pra esquerda
            return MathF.PI / 2;
        }

        if (obj1.Hitbox.Contains(upright2) && obj1.Hitbox.Contains(bottomright2))
        {
            // Bateu da esquerda pra direita
            return 3 * MathF.PI / 2;
        }

        if (obj1.Hitbox.Contains(upleft2) && obj1.Hitbox.Contains(upright2))
        {
            // Bateu de baixo pra cima
            return MathF.PI;
        }

        if (obj1.Hitbox.Contains(bottomleft2) && obj1.Hitbox.Contains(bottomright2))
        {
            // Bateu de cima pra baixo
            return 0f;
        }

        if (obj1.Hitbox.Contains(upleft2))
        {
            var dx = bottomright1.X - upleft2.X;
            var dy = bottomright1.Y - upleft2.Y;

            var tx = MathF.Abs(dx / vx);
            var ty = MathF.Abs(dy / vy);
            // MessageBox.Show("TX: " + tx.ToString() + " TY: " + ty.ToString());

            if (tx < ty)
            {
                // Bateu da direita pra esquerda
                return MathF.PI / 2;
            }
            else
            {
                // Bateu de baixo pra cima
                return MathF.PI;
            }
        }
        
        if (obj1.Hitbox.Contains(bottomleft2))
        {
            var dx = upright1.X - bottomleft2.X;
            var dy = bottomleft2.Y - upright1.Y;

            var tx = dx / vx;
            var ty = dy / vy;

            if (tx > ty)
            {
                // Bateu da direita pra esquerda
                return MathF.PI / 2;
            }
            else
            {
                // Bateu de cima pra baixo
                return 0f;
            }
        }
        
        if (obj1.Hitbox.Contains(bottomright2))
        {
            var dx = bottomright2.X - upleft1.X;
            var dy = bottomright2.Y - upleft1.Y;

            var tx = dx / vx;
            var ty = dy / vy;

            if (tx > ty)
            {
                // Bateu da direita pra esquerda
                return 3 * MathF.PI / 2;
            }
            else
            {
                // Bateu de cima pra baixo
                return 0f;
            }
        }

        if (obj1.Hitbox.Contains(upright2))
        {
            var dx = upright2.X - bottomleft1.X;
            var dy = bottomleft1.Y - upright2.Y;

            var tx = dx / vx;
            var ty = dy / vy;

            if (tx < ty)
            {
                // Bateu da direita pra esquerda
                return 3 * MathF.PI / 2;
            }
            else
            {
                // Bateu de baixo pra cima
                return MathF.PI;
            }
        }

        return float.MaxValue;
    }

    public static void ChangeVelocity(IMoveable obj, float angleR)
    {
        float vt = obj.Vx * MathF.Cos(angleR) + obj.Vy * MathF.Sin(angleR);
        float vn = -obj.Vx * MathF.Sin(angleR) + obj.Vy * MathF.Cos(angleR);

        vn *= -1;

        obj.Vx = vt * MathF.Cos(angleR) - vn * MathF.Sin(angleR);
        obj.Vy = vt * MathF.Sin(angleR) + vn * MathF.Cos(angleR);
    }

    public static void ResetList() => GameObjects = new List<GameObject>();
}
