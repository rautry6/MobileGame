using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class SkeeballScoreManager : MonoBehaviour
{
    [SerializeField] SkeeballInput input;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] ScoreSlider scoreSlider;
    [SerializeField] Image backwallPrizeImage;

    [SerializeField] Prize[] prizes;
    private List<Prize> possiblePrizes;
    private List<Prize> winablePrizes;
    [SerializeField] int[] scoreThresholds;

    [SerializeField] private TMP_Text[] popUpScores;
    [SerializeField] private Image[] popUpPowerUps;

    int currentScoreThreshold = 0;
    
    public int currentScore { get; private set; }
    public Prize currentPrize { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        possiblePrizes = new List<Prize>();
        winablePrizes = new List<Prize>();

        for(int i = 0;  i < prizes.Length; i++)
        {
            possiblePrizes.Add(prizes[i]);
        }

        for (int i = 0; i < popUpPowerUps.Length; i++)
        {
            Random random = new Random();

            int randomPrizeIndex = random.Next(0, possiblePrizes.Count);

            popUpPowerUps[i].sprite = possiblePrizes[randomPrizeIndex].PrizeSprite;
            popUpScores[i].text = scoreThresholds[i].ToString();

            winablePrizes.Add(possiblePrizes[randomPrizeIndex]);
            possiblePrizes.RemoveAt(randomPrizeIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        currentScore += score;

        if (currentScoreThreshold < scoreThresholds.Length && currentScore >= scoreThresholds[currentScoreThreshold])
        {
            currentPrize = winablePrizes[currentScoreThreshold];

            backwallPrizeImage.sprite = winablePrizes[currentScoreThreshold].PrizeSprite;
            backwallPrizeImage.enabled = true;

            currentScoreThreshold++;
        }

        scoreSlider.UpdateScore(currentScore);

        input.ResetSlime();
    }
}
