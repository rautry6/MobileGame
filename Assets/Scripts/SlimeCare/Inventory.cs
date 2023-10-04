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
    public static Inventory Instance { get; private set; }

    [SerializeField] GameObject inventoryPanel;
    [SerializeField] InventoryButton[] inventoryButtons;
    private int startingButtonCount;
    [SerializeField] Image selectedItemImage;
    [SerializeField] TMP_Text hungerChangeText;
    [SerializeField] TMP_Text happinessChangeText;


    private List<InventoryItem> _prizeInventory;

    [SerializeField] private Prize[] testPrizes;

    private Prize currentPrize;
    // Start is called before the first frame update
    void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            // If an instance already exists and it's not this, then destroy this. This enforces our singleton pattern.
            Destroy(gameObject);
            return;
        }

        // If no instance exists, then this becomes the instance.
        Instance = this;

        _prizeInventory = new List<InventoryItem>();
        //SceneManager.sceneLoaded += RetrieveWonPrize;

        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            inventoryButtons[i].ClearItems();
        }


        //Testing
        foreach (Prize prize in testPrizes)
        {
            //AddPrizeToInventory(prize);
        }

        startingButtonCount = inventoryButtons.Length;
        inventoryPanel.SetActive(false);

        DontDestroyOnLoad(gameObject);
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

    public void GetReferences()
    {
        inventoryPanel = GameObject.Find("InventoryPanel");

        if(inventoryPanel != null)
        {
            GameObject inventoryButtonsObject = GameObject.Find("InventoryButtons");

            inventoryButtons = new InventoryButton[startingButtonCount];

            if (inventoryButtonsObject != null)
            {
                for (int i = 0; i < inventoryButtonsObject.transform.childCount; i++)
                {
                    inventoryButtons[i] = inventoryButtonsObject.transform.GetChild(i).GetComponent<InventoryButton>();
                }
            }

            selectedItemImage = GameObject.Find("Selected Prize Image").GetComponent<Image>();
            happinessChangeText = GameObject.Find("HappinessText").GetComponent<TMP_Text>();
            hungerChangeText = GameObject.Find("HungerText").GetComponent<TMP_Text>();
            inventoryPanel.SetActive(false);
        }
    }

}
