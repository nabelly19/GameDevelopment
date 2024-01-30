public class Interactable : GameObject, IInteractable
{
    public Interactable(string name, float x, float y, string sprite) : base(name, x, y, sprite)
    {
    }

    public bool isInteractable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}