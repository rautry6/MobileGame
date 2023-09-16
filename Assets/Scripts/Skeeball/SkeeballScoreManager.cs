using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkeeballScoreManager : MonoBehaviour
{
    [SerializeField] SkeeballInput input;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] ScoreSlider scoreSlider;
    [SerializeField] Image backwallPrizeImage;

    [SerializeField] Prize[] prizes;
    [SerializeField] int[] scoreThresholds;

    [SerializeField] private TMP_Text[] popUpScores;
    [SerializeField] private Image[] popUpPowerUps;

    int currentScoreThreshold = 0;
    
    public int currentScore { get; private set; }
    public Prize currentPrize { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < prizes.Length; i++)
        {
            popUpPowerUps[i].sprite = prizes[i].PrizeSprite;
            popUpScores[i].text = scoreThresholds[i].ToString();
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
            currentPrize = prizes[currentScoreThreshold];

            backwallPrizeImage.sprite = prizes[currentScoreThreshold].PrizeSprite;
            backwallPrizeImage.enabled = true;

            currentScoreThreshold++;
        }

        scoreSlider.UpdateScore(currentScore);

        input.ResetSlime();
    }
}
