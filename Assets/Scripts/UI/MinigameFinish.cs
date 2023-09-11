using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameFinish : MonoBehaviour
{
    [SerializeField] GameObject gameOverUi;
    [SerializeField] TMP_Text gameOverUiScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayGameOverUi(int scoreValue)
    {
        if (gameOverUi == null)
        {
            return;
        }

        gameOverUi.SetActive(true);

        if (gameOverUiScore != null)
        {
            gameOverUiScore.text = scoreValue.ToString();
        }

        //Set prize stuff
    }

    public void BackToHomeWithPrize()
    {

    }

    public void BackToHomeNoPrize()
    {

    }
}
