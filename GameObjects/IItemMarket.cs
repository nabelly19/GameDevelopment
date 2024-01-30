using System.Collections.Generic;
using System.Windows.Forms;

public interface IItemMarket
{
    public void BuyIt();
    int itemValue { get; set; }
    int itemOccurrence { get; set; }
    
}