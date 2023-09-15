using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace SlimeHole
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Prize[] prizes;
        [SerializeField] private int[] scoreThresholds;

        public int currentScore { get; private set; }
        public Prize currentPrize { get; private set; }

        private int currentScoreThreshold = 0;

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
