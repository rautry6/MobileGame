using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BucketManager : MonoBehaviour
{
    [SerializeField] private Bucket[] buckets;

    [Header("Options and Sprites for prizes"), Tooltip("Indexes of items need to match up between arrays")]
    [SerializeField] private Prize[] prizeOptions;

    private List<Prize> possiblePrizes;
    private List<Sprite> possibleSprites;

    private void Start()
    {
        possiblePrizes = new List<Prize>();
        possibleSprites = new List<Sprite>();

        //Add all sprites and prizes to lists
        for(int i = 0; i < prizeOptions.Length; i++) 
        {
            possiblePrizes.Add(prizeOptions[i]);
            possibleSprites.Add(prizeOptions[i].PrizeSprite);
        }

        RandomizePrizes();
    }

    void RandomizePrizes()
    {
        foreach(Bucket bucket in buckets)
        {
            Random random = new Random();

            int randomPrizeIndex = random.Next(0, possiblePrizes.Count);

            bucket.SetBucketPrize(possiblePrizes[randomPrizeIndex], possibleSprites[randomPrizeIndex]);

            possiblePrizes.RemoveAt(randomPrizeIndex);
            possibleSprites.RemoveAt(randomPrizeIndex);
        }
    }

}
