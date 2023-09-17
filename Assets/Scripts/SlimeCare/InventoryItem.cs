using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem 
{
    public Prize Prize;
    public int Quantity;

    public InventoryItem(Prize prize, int quantity)
    {
        this.Prize = prize;
        this.Quantity = quantity;
    }
}
