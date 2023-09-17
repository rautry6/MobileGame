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

    [SerializeField] private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentPrize(Prize prize, int quantity)
    {
        currentPrize = prize;

        prizeName.text = prize.PrizeName;
        prizeQuantity.text = quantity.ToString();
        prizeImage.sprite = prize.PrizeSprite;
        prizeImage.enabled = true;
    }

    public void Selected()
    {
        inventory.SetSelectedPrize(currentPrize);
    }

    public void ClearItems()
    {
        currentPrize = null;
        prizeName.text = "";
        prizeQuantity.text = "";
        prizeImage.enabled = false;
    }

}
