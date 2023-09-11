using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : MonoBehaviour
{
    [SerializeField] private Prize bucketPrize;
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
            minigameFinish.DisplayGameOverUi(0, bucketPrize);
        }
    }

    public void SetBucketPrize(Prize prize, Sprite prizeIcon)
    {
        bucketPrize = prize;

        prizeImage = prizeIcon;

        if (prizeImage != null)
        {
            prizeImageHolder.sprite = prizeImage;
        }
    }
}
