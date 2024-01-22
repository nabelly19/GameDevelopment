public interface IAttackable
{
    public int Hp { get; set; }
    public void Attack() { }
    public virtual void ReceiveDamage() => this.Hp--;
}
