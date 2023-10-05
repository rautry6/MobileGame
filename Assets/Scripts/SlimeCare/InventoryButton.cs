using SlimeCare;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    private Prize currentPrize;
    public Prize Prize { get { return currentPrize; } }

    [SerializeField] private TMP_Text prizeName;
    [SerializeField] protected TMP_Text prizeQuantity;
    [SerializeField] private Image prizeImage;

    public void SetCurrentPrize(Prize prize, int quantity)
    {
        Debug.Log(prize);
        currentPrize = prize;

        if(prize == null)
        {
            return;
        }

        prizeName.text = prize.PrizeName; 
        prizeQuantity.text = quantity.ToString();
        prizeImage.sprite = prize.PrizeSprite;
        prizeImage.enabled = true;
    }

    public void Selected()
    {
        Inventory.Instance?.SetSelectedPrize(currentPrize);
    }

    public void ClearItems()
    {
        currentPrize = null;
        prizeName.text = "";
        prizeQuantity.text = "";
        prizeImage.enabled = false;
    }

}
