using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeeballMinigameFinish : MonoBehaviour
{
    [SerializeField] GameObject gameOverUi;
    [SerializeField] TMP_Text gameOverUiScore;

    public void DisplayGameOverUi(int scoreValue)
    {
        if (gameOverUi == null)
        {
            Debug.LogError($"Missing gameOverUI reference in Scene: {SceneManager.GetActiveScene().name}");
            return;
        }

        gameOverUi.SetActive(true);
        gameOverUiScore.SetText(scoreValue.ToString());
    }

    public void BackToHomeWithPrize()
    {

    }

    public void BackToHomeNoPrize()
    {

    }
}
