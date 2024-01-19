using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class CollisionManager
{
    private static CollisionManager current;
    public static CollisionManager Current => current;
    public List<GameObject> gameObjects { get; private set; }

    private CollisionManager()
    {
        gameObjects = new List<GameObject>();
        current = this;
    }

    public void AddGameObject(GameObject gameObject)
    {
        gameObjects.Add(gameObject);
    }

    public bool CheckCollisions(GameObject obj)
    {
        for (int j = 0; j < gameObjects.Count; j++)
        {
            GameObject other = gameObjects[j];
            if (other == obj)
                continue;

            if (CollisionDetected(obj, other) && other.isHittable)
                return true;
        }
        return false;
    }

    public IEnumerable<GameObject> GetCollisions(GameObject obj)
    {
        for (int j = 0; j < gameObjects.Count; j++)
        {
            GameObject other = gameObjects[j];
            if (other == obj)
                continue;

            if (CollisionDetected(obj, other) && other.isHittable)
                yield return other;
        }
    }

    private bool CollisionDetected(GameObject obj1, GameObject obj2)
    {
        // Lógica para detectar colisões entre dois objetos
        return obj2.Hitbox.IntersectsWith(obj1.Hitbox);
    }

    public void HandleDamage(IAttackable attackable)
    {
        attackable.ReceiveDamage();
    }

    public static void New() => current = new CollisionManager();
}
