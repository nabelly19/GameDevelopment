public abstract class State
{
    public Boss boss = null;
    public State nextState { get; set; }
    public abstract void Attack();
    public abstract void Move();
}
