using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : MonoBehaviour
{
    [SerializeField] private string bucketPrize;
    [SerializeField] private Sprite prizeImage;
    [SerializeField] private Image prizeImageHolder;
    [SerializeField] private MinigameFinish minigameFinish;

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
            minigameFinish.DisplayGameOverUi(0);
        }
    }

    public void SetBucketPrize(string prize, Sprite prizeIcon)
    {
        bucketPrize = prize;

        prizeImage = prizeIcon;

        if (prizeImage != null)
        {
            prizeImageHolder.sprite = prizeImage;
        }
    }
}
