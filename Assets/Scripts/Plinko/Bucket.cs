using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : MonoBehaviour
{
    [SerializeField] private string bucketPrize;
    [SerializeField] private Sprite prizeImage;
    [SerializeField] private Image prizeImageHolder;

    private void Start()
    {
        if (prizeImage != null)
        {
            prizeImageHolder.sprite = prizeImage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slime"))
        {
            Debug.Log("Player wins " + bucketPrize);
        }
    }
}
