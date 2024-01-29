public class TrackingProjectile : Projectile
{
    public TrackingProjectile
    (
        string name, 
        int x, 
        int y, 
        string sprite, 
        float direction, 
        IAttackable owner
    ) : base(name, x, y, sprite, direction, owner)
    {
        //set something
    }
}