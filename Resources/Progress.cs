using System.Collections.Generic;

public class Progress
{
    public List<Item> PlayerUpgrades { get; set; }

    public void ApplyBuffs()
    {
        foreach (var item in this.PlayerUpgrades)
        {
            item.ApplyBuff();
        }
    }
}