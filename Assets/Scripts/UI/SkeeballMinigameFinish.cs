using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkeeballMinigameFinish : MonoBehaviour
{
    [SerializeField] GameObject gameOverUi;
    [SerializeField] TMP_Text gameOverUiScore;
    [SerializeField] private SkeeballScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayGameOverUi()
    {
        if (gameOverUi == null)
        {
            return;
        }

        gameOverUi.SetActive(true);
        gameOverUiScore.text = scoreManager.score.ToString();
    }

    public void BackToHomeWithPrize()
    {

    }

    public void BackToHomeNoPrize()
    {

    }
}
