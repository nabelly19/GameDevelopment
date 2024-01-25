public interface IAttackable
{
    public int Hp { get; set; }
    public void Attack() { }
    public void ReceiveDamage();
}
