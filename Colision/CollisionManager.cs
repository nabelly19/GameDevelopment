using System;
using System.Collections.Generic;
using System.Windows.Forms;
public class CollisionManager
{
    public List<GameObject> gameObjects {get;private set;}

    public CollisionManager()
    {
        gameObjects = new List<GameObject>();
    }

    public void AddGameObject(GameObject gameObject)
    {
        gameObjects.Add(gameObject);
    }

    public void CheckCollisions()
    {
        // Lógica para verificar colisões entre os objetos do jogo
        for (int i = 0; i < gameObjects.Count; i++)
        {
            for (int j = i + 1; j < gameObjects.Count; j++)
            {
                GameObject obj1 = gameObjects[i];
                GameObject obj2 = gameObjects[j];

                if (CollisionDetected(obj1, obj2))
                {
                    HandleCollision(obj1, obj2);
                }
            }
        }
    }

    private bool CollisionDetected(GameObject obj1, GameObject obj2)
    {
        // Lógica para detectar colisões entre dois objetos
        return Math.Abs(obj1.X - obj2.X) < 10 && Math.Abs(obj1.Y - obj2.Y) < 10;
    }

    private void HandleCollision(GameObject obj1, GameObject obj2)
    {
        // Lógica para lidar com a colisão entre dois objetos
        if (obj1 is Player && obj2 is Boss)
        {
            Console.WriteLine("Player colidiu com o Boss - Jogador sofre dano!");
            // Adicione lógica específica para colisão entre Player e Boss
        }
        else if (obj1 is Bullet && obj2 is Boss)
        {
            Console.WriteLine("Bala atingiu o Boss - Boss sofre dano!");
            // Adicione lógica específica para colisão entre Bala e Boss
        }
        // Adicione mais condições para outros tipos de colisões, se necessário
    }
}
