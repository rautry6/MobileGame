using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkeeballScoreManager : MonoBehaviour
{
    [SerializeField] SkeeballInput input;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] ScoreSlider scoreSlider;

    [SerializeField] Prize[] prizes;
    [SerializeField] int[] scoreThresholds;

    int currentScoreThreshold = 0;
    
    public int currentScore { get; private set; }
    public Prize currentPrize { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
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
            currentScoreThreshold++;
        }

        scoreSlider.UpdateScore(currentScore);

        input.ResetSlime();
    }
}
