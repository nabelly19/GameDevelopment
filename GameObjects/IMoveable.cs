public interface IMoveable
{
    public float Base_Speed { get; set; }
    public float Vx { get; set; }
    public float Vy { get; set; }
    public void Move() { }
}
