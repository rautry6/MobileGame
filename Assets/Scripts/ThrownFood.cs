using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownFood : MonoBehaviour
{
    public Prize currentPrize;
    public FoodThrowing foodThrowing;

    private void OnTriggerEnter(Collider other)
    {
        if (currentPrize != null && other.CompareTag("Slime"))
        {
            foodThrowing.FoodAte();
            Inventory.Instance.EatItem(currentPrize);
            PlayerPrefs.SetInt("FoodEatenNum", PlayerPrefs.GetInt("FoodEatenNum", 0) + 1);
            Destroy(gameObject);
        }
    }
}
