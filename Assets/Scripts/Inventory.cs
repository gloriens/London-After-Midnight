using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Iitem slot;
    
    public bool HasItem => slot != null;

    public void SetItem(Iitem item)
    {
        slot = item;
    }

    public Iitem GetItem()
    {
        return slot;
    }

    public void RemoveItem()
    {
        slot = null;
    }
}
