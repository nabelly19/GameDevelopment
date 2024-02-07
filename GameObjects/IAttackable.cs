public interface IAttackable
{
    public bool isVulnerable { get; set; }
    public bool isAlive { get; set; }
    public int Hp { get; set; }
    public float BlockChance { get; set; }
    public void Attack();
    public void ReceiveDamage();
}
