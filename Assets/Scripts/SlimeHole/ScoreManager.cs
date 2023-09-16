using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SlimeHole
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Prize[] prizes;
        [SerializeField] private int[] scoreThresholds;

        public int currentScore { get; private set; }
        public Prize currentPrize { get; private set; }

        [SerializeField] private TMP_Text[] popUpScores;
        [SerializeField] private Image[] popUpPowerUps;

        int currentScoreThreshold = 0;

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < prizes.Length; i++)
            {
                popUpPowerUps[i].sprite = prizes[i].PrizeSprite;
                popUpScores[i].text = scoreThresholds[i].ToString();
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
                currentPrize = prizes[currentScoreThreshold];
                currentScoreThreshold++;
            }

            scoreText.SetText(currentScore.ToString());
        }
        
    }
}
