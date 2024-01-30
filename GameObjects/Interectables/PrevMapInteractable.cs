public class PrevMapInteractable : Interactable
{
    public PrevMapInteractable(string name, float x, float y, string sprite) : base(name, x, y, sprite)
    {
    }

    public override void Interact()
        => MapManager.PrevMap();
    
}