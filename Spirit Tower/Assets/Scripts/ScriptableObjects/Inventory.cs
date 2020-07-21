using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item CurrentItem;
    public List<Item> Items = new List<Item>();
    public int NumberOfKeys;
    public int coins;

    public void AddItem(Item ItemToAdd)
    {
        if (ItemToAdd.isKey)
        {
            NumberOfKeys++;
        }
        else
        {
            if (!Items.Contains(ItemToAdd))
            {
                Items.Add(ItemToAdd);
            }
        }
    }
}
