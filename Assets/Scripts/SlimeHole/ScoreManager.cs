using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace SlimeHole
{
    public class ScoreManager : MonoBehaviour
    {
        public int currentScore { get; private set; }
        [SerializeField] private TMP_Text scoreText;

        private void Update()
        {
            Debug.Log($"CurrentScore: {currentScore}");
        }

        public void UpdateScore(int points)
        {
            currentScore += points;
            scoreText.SetText(currentScore.ToString());
        }
        
    }
}
