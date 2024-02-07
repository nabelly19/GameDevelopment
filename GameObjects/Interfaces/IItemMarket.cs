using System.Collections.Generic;
using System.Windows.Forms;

public interface IItemMarket
{
    public void BuyIt();
    int Value { get; set; }
    int Occurrence { get; set; }
    public bool Temporary { get; set; }
    
}