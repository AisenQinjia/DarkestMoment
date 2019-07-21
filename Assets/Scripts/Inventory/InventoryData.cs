using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData
{
    private List<ItemData> items;

    public List<ItemData> Items { get { return items; } }

    public InventoryData()
    {
        items = new List<ItemData>();
    }
    public void AddItem(int cfgId)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].cfgId == cfgId)
            {
                items[i].count++;
                return;
            }
        }

        ItemData newItem = new ItemData(ConfigManager.Instance.GetItemData(cfgId));
        items.Add(newItem);
        newItem.count++;
    }

    public void RemoveItem(int cfgId)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].cfgId == cfgId && items[i].count > 0)
            {
                items[i].count--;
            }
        }
    }

    public bool HasItem(int cfgId)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].cfgId == cfgId && items[i].count > 0)
            {
                return true;
            }
        }
        return false;
    }
}
