using System;
using TMPro;
using UnityEngine;

namespace SlimeHole
{
    public class ScoreManager : MonoBehaviour
    {
        public static int CurrentScore;
        [SerializeField] private TMP_Text scoreText;

        
        private void Update()
        {
            Debug.Log($"CurrentScore: {CurrentScore}");
        }

        public void UpdateScore(int points)
        {
            CurrentScore += points;
            scoreText.SetText(CurrentScore.ToString());
        }
    }
}
