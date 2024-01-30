public interface IMoveable
{
    public float BaseAcceleration { get; set; }
    public float Ax { get; set; }
    public float Ay { get; set; }
    public void Move() { }
}
