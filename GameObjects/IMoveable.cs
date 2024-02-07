public interface IMoveable
{
    public float Vx { get; set; }
    public float Vy { get; set; }
    public float BaseAcceleration { get; set; }
    public float Ax { get; set; }
    public float Ay { get; set; }
    public bool isMoving { get; set; }
    public void Move() { }
}
