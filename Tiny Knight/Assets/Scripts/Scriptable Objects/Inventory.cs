using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> itemList = new List<Item>();
    public int numberOfKeys;
    public int numberOfCoins;

    public void AddItem(Item item)
    {
        if (item.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if (!itemList.Contains(item))
            {
                itemList.Add(item);
            }
        }
    }
}
