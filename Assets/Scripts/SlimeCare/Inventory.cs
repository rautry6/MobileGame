using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SlimeCare;
using UnityEngine.InputSystem.EnhancedTouch;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryButton[] inventoryButtons;
    [SerializeField] Image selectedItemImage;
    [SerializeField] TMP_Text hungerChangeText;
    [SerializeField] TMP_Text happinessChangeText;


    private List<InventoryItem> _prizeInventory;

    [SerializeField] private Prize[] testPrizes;

    private Prize currentPrize;
    // Start is called before the first frame update
    void Awake()
    {
        _prizeInventory = new List<InventoryItem>();
        //SceneManager.sceneLoaded += RetrieveWonPrize;

        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            inventoryButtons[i].ClearItems();
        }


        //Testing
        foreach (Prize prize in testPrizes)
        {
            AddPrizeToInventory(prize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RetrieveWonPrize(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (SlimeCareStats.Instance == null || SlimeCareStats.Instance.WonPrize == null)
        {
            return;
        }

        AddPrizeToInventory(SlimeCareStats.Instance.WonPrize);
        SlimeCareStats.Instance.SetPrize(null);
    }

    public void SetSelectedPrize(Prize prize)
    {
        currentPrize = prize;

        if(prize == null)
        {
            ClearSelectedPrize();
            return;
        }

        selectedItemImage.sprite = prize.PrizeSprite;
        hungerChangeText.text = prize.HungerChange >= 0 ? "+" + prize.HungerChange.ToString() : prize.HungerChange.ToString();
        happinessChangeText.text = prize.HappinessChange >= 0 ? "+" + prize.HappinessChange.ToString() : prize.HappinessChange.ToString();
    }

    void ClearSelectedPrize()
    {
        selectedItemImage.sprite = null;
        hungerChangeText.text = "Hunger Change";
        happinessChangeText.text = "Happiness Change";
    }

    public void AddPrizeToInventory(Prize prizeToAdd)
    {
        if(_prizeInventory == null)
        {
            return;
        }

        foreach(InventoryItem inventoryItem in _prizeInventory)
        {
            if(inventoryItem.Prize == prizeToAdd)
            {
                inventoryItem.Quantity++;
                return;
            }
        }

        //Prize not in list
        InventoryItem item = new InventoryItem(prizeToAdd, 1);

        _prizeInventory.Add(item);
    }

    public void UsePrize()
    {

        if(currentPrize == null)
        {
            return;
        }

        foreach (InventoryItem inventoryItem in _prizeInventory)
        {
            if (inventoryItem.Prize == currentPrize)
            {
                inventoryItem.Quantity--;

                if(inventoryItem.Quantity <= 0)
                {
                    _prizeInventory.Remove(inventoryItem);
                    ClearSelectedPrize();
                }

                UpdateUi();

                Prize prize = inventoryItem.Prize;

                //Code to update values for health and happiness
                SlimeCareStats.Instance.UpdateHealthAndHappiness(prize.HungerChange, prize.HappinessChange);

                return;
            }
        }

        Debug.LogError("Trying to use prize not found in inventory");
    }

    public void UpdateUi()
    {
        for(int i = 0; i < inventoryButtons.Length; i++)
        {
            inventoryButtons[i].ClearItems();
        }

        for(int i = 0; i < _prizeInventory.Count; i++)
        {
            InventoryItem currentItem = _prizeInventory[i];
            inventoryButtons[i].SetCurrentPrize(currentItem.Prize, currentItem.Quantity);
        }
    }


}
