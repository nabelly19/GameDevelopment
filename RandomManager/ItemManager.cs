using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class ItemManager
{
    private Item item = null;
    public List<Item> AllItens = new();
    public List<Item> ItemsPerMarketList = new();
    public List<Item> IsTemporary = new();
    public bool temporary { get; set; } = false;
    private Random random = new Random();

    public ItemManager (Item item)
    {
        this.item = item;
    }

    public void AddToMarket()
    {
        int totalOccurrence = AllItens.Sum(item => item.itemOccurrence);
        int randomOcNumber = random.Next(totalOccurrence);
        int sumOcNumber = 0;

        if(temporary)
            addToChoice(item);
        else{
        foreach(Item item in ItemsPerMarketList)
        {
            sumOcNumber += item.itemOccurrence;
            if (randomOcNumber < sumOcNumber)
                this.ItemsPerMarketList.Add(item);
        }
    }
    }
    public void addToChoice(Item temporaryItem)
    {
        this.IsTemporary.Add(temporaryItem);
    }

    public void showChoice(Item item1, Item item2, Graphics g, PictureBox pb)
    {
        int totalOccurrence = IsTemporary.Sum(item => item.itemOccurrence);
        int randomOcNumber = random.Next(totalOccurrence);
        int sumOcNumber = 0;
        
        foreach(Item item in IsTemporary)
        {
            sumOcNumber += item.itemOccurrence;
            if(randomOcNumber < sumOcNumber)
                item1 = item;
        }

        foreach(Item item in IsTemporary)
        {
            sumOcNumber += item.itemOccurrence;
            if(randomOcNumber < sumOcNumber)
                item2 = item;
        }

        
    }

}