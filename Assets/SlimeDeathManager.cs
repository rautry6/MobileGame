using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SlimeDeathManager : MonoBehaviour
{
    public TMP_Text descripText;
    public TMP_Text titleText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("SlimeHappiness") < PlayerPrefs.GetFloat("SlimeHunger"))
        {
            titleText.SetText("" + PlayerPrefs.GetString("SlimeName") + " died of Sadness");
        }
        else
        {
            titleText.SetText("" + PlayerPrefs.GetString("SlimeName") + " died of Hunger");
        }

        descripText.SetText("" + PlayerPrefs.GetString("SlimeName", "unnamed")
            + "\nAte " + PlayerPrefs.GetInt("FoodEatenNum", 0) + " food"
            + "\nAnd"
            + "\nWon " + PlayerPrefs.GetInt("PrizesWonNum", 0) + " Prizes");

        PlayerPrefs.SetFloat("SlimeHappiness", 600);
        PlayerPrefs.SetFloat("SlimeHunger", 600);
        PlayerPrefs.SetString("SlimeName", "Unnamed");
        PlayerPrefs.SetInt("FoodEatenNum", 0);
        PlayerPrefs.SetInt("PrizesWonNum", 0);
        PlayerPrefs.SetInt("CoinAmount", 0);
        PlayerPrefs.SetInt("FirstPlay", 0);
    }

    public void RestartPressed ()
    {
        SceneManager.LoadScene("SlimeCreation");
    }
}
