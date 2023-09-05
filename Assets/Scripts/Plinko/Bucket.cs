using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private string bucketPrize;
    [SerializeField] private Sprite prizeImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slime"))
        {
            Debug.Log("Player wins " + bucketPrize);
        }
    }
}
