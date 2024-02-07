using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public static class ItemManager
{
    public static List<Item> AllItens;
    public static List<Item> Bought = new();
    private static Random random = new Random();

    public static void Startup()
    {
        AllItens = new()
        {
            new ItemWindBlade("Item Wind", 0, 0, 100, 100),
            new ItemWindBlade("Item Wind", 0, 0, 100, 100),
            new ItemWindBlade("Item Wind", 0, 0, 100, 100),
            new ItemSpeed("Speed 5%", 0, 0, 100, 100, 0.5f),
            new ItemSpeed("Speed 2%", 0, 0, 100, 100, 0.2f),
            new ItemSpeed("Speed 1%", 0, 0, 100, 100, 0.01f),
            new ItemHp("+1 Heart", 0, 0, 100, 100, 1),
            new ItemHp("+2 Heart", 0, 0, 100, 100, 2),
            new ItemBlockChance("Block 7%", 0, 0, 100, 100, 0.07f),
            new ItemBlockChance("Block 5%", 0, 0, 100, 100, 0.05f),
            new ItemBlockChance("Block 2%", 0, 0, 100, 100, 0.02f),
            new ItemCritChance("Crit 5%", 0, 0, 100, 100, 0.05f),
            new ItemCritChance("Crit 3%", 0, 0, 100, 100, 0.03f),
            new ItemCritChance("Crit 1%", 0, 0, 100, 100, 0.01f),

        };
    }

    public static void getRandomItems(Item[] items)
    {
        var rndItems =
            from it in AllItens
            orderby Random.Shared.Next()
            select it;
        var selectedItens = rndItems
            .Take(items.Length)
            .ToArray();

        for (int i = 0; i < items.Length; i++)
            items[i] = selectedItens[i];
    }
}