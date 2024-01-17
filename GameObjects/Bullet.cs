// namespace Entity;

public class Bullet : GameObject
{
    public Bullet(string name, int x, int y, string sprite) : base(name, x, y, sprite)
    {
    }

    public void Move()
    {
        // Lógica de movimento da bala
        X += 1;
        Y += 1;
    }
}
