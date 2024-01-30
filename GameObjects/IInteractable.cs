using System.Reflection.PortableExecutable;

public interface IInteractable
{
    public void Interact( ) { }
    public void ColectItem( ) { }
    public bool isInteractable { get; set; }
    
}