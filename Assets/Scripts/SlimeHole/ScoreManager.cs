using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

namespace SlimeHole
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Prize[] prizes;
        private List<Prize> possiblePrizes;
        private List<Prize> winablePrizes;
        [SerializeField] private int[] scoreThresholds;

        public int currentScore { get; private set; }
        public Prize currentPrize { get; private set; }

        [SerializeField] private TMP_Text[] popUpScores;
        [SerializeField] private Image[] popUpPowerUps;

        int currentScoreThreshold = 0;

        // Start is called before the first frame update
        void Start()
        {
            possiblePrizes = new List<Prize>();
            winablePrizes = new List<Prize>();

            for (int i = 0; i < prizes.Length; i++)
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

        private void Update()
        {
            Debug.Log($"CurrentScore: {currentScore}");
        }

        public void UpdateScore(int points)
        {
            currentScore += points;

            if( currentScoreThreshold < scoreThresholds.Length && currentScore >= scoreThresholds[currentScoreThreshold])
            {
                currentPrize = winablePrizes[currentScoreThreshold];
                currentScoreThreshold++;
            }

            scoreText.SetText(currentScore.ToString());
        }
        
    }
}
