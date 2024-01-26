public interface IAttackable
{
    public bool isVulnerable { get; set; }
    public int Hp { get; set; }
    public void Attack() { }
    public void ReceiveDamage();
}
