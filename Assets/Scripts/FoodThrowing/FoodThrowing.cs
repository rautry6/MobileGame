using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FoodThrowing : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject UI;

    [SerializeField] private GameObject currentFood;
    [SerializeField] private Transform foodSpawnPoint;
    [SerializeField] private GameObject throwingFoodPrefab;

    private bool foodThrown = false;
    private bool throwingFood = false;
    private bool slimeAteFood = false;

    PlinkoControls controls;
    InputAction touch;

    [SerializeField] private Vector3 throwForce;
    [SerializeField] private GameObject slime;
    [SerializeField] private SlimeWander slimeWanderingManager;

    private void Awake()
    {
        controls = new PlinkoControls();
    }

    private void OnEnable()
    {
        touch = controls.FoodThrowing.Touch;
        //touch.Enable();
    }

    private void OnDisable()
    {
        touch.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (throwingFood)
        {
            if (touch.WasPerformedThisFrame())
            {
                throwingFood = false;
                currentFood.GetComponent<Rigidbody>().AddForce(throwForce, ForceMode.Impulse);
                currentFood.GetComponent<Rigidbody>().useGravity = true;
                foodThrown = true;
            }
        }

        if (foodThrown && currentFood.GetComponent<Rigidbody>().velocity.y < 0)
        {
            if(currentFood.transform.position.y <= slime.transform.position.y)
            {
                foodThrown = false;
                currentFood.GetComponent<Rigidbody>().useGravity = false;
                currentFood.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                slimeWanderingManager.StartEating(currentFood.transform);
            }
        }

    }

    public void StartThrowing(Prize currentPrize)
    {

        slimeWanderingManager.StartWaiting();

        currentFood = Instantiate(throwingFoodPrefab, foodSpawnPoint.position, Quaternion.identity);
        currentFood.GetComponent<SpriteRenderer>().sprite = currentPrize.PrizeSprite;
        currentFood.GetComponent<ThrownFood>().currentPrize = currentPrize;
        currentFood.GetComponent<ThrownFood>().foodThrowing = this;

        throwingFood = true;
        inventoryUI.SetActive(false);
        UI.SetActive(false);

        touch.Enable();
    }

    public void FoodAte()
    {
        throwingFood = false;
        foodThrown = false;
        slimeAteFood = false;
        UI.SetActive(true);
        currentFood = null;
        slimeWanderingManager.DoneEating();
    }
}
