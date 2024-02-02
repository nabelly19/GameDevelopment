using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public static class ItemManager
{
    public static List<Item> AllItens = new();
    private static Random random = new Random();

    public static void Startup()
    {
        AllItens.Add(new ItemWindBlade("Item Wind", 0, 0, 100, 100));
        AllItens.Add(new ItemWindBlade("Item Wind", 0, 0, 100, 100));
        AllItens.Add(new ItemWindBlade("Item Wind", 0, 0, 100, 100));
    }

    public static void getRandomItems(Item[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            var rdmItem = VerifyRandomCard(items);
            items[i] = rdmItem;
        }
    }

    private static Item VerifyRandomCard(Item[] items)
    {
        Item rdmItems;
        // do
        // {
            rdmItems = AllItens[Random.Shared.Next(AllItens.Count)];
        // } while (items.Contains(rdmItems));

        return rdmItems;
    }
}

// public void AddToMarket()
// {
//     int totalOccurrence = AllItens.Sum(item => item.itemOccurrence);
//     int randomOcNumber = random.Next(totalOccurrence);
//     int sumOcNumber = 0;

//     if(temporary)
//         addToChoice(item);
//     else{
//     foreach(Item item in ItemsPerMarketList)
//     {
//         sumOcNumber += item.itemOccurrence;
//         if (randomOcNumber < sumOcNumber)
//             this.ItemsPerMarketList.Add(item);
//     }
// }
// }

// public void addToChoice(Item temporaryItem)
// {
//     this.IsTemporary.Add(temporaryItem);
// }

// public void showChoice(Item item1, Item item2, Graphics g, PictureBox pb)
// {
//     int totalOccurrence = IsTemporary.Sum(item => item.itemOccurrence);
//     int randomOcNumber = random.Next(totalOccurrence);
//     int sumOcNumber = 0;

//     foreach(Item item in IsTemporary)
//     {
//         sumOcNumber += item.itemOccurrence;
//         if(randomOcNumber < sumOcNumber)
//             item1 = item;
//     }

//     foreach(Item item in IsTemporary)
//     {
//         sumOcNumber += item.itemOccurrence;
//         if(randomOcNumber < sumOcNumber)
//             item2 = item;
//     }


// }
