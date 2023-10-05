using System.Collections;
using JelloFelloInterfaces;
using SlimeCare;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MinigameFinish : MonoBehaviour
    {
        [SerializeField] GameObject gameOverUi;
        [SerializeField] TMP_Text gameOverUiScore;
        [SerializeField] Image prizeImage;
        [SerializeField] TMP_Text prizeText;

        private Prize prizeToTakeBack;
        
        public void DisplayGameOverUi(int scoreValue, Prize prize)
        {
            PlayerPrefs.SetInt("PrizesWonNum", PlayerPrefs.GetInt("PrizesWonNum", 0) + 1);
            Debug.Log(prize);

            if (gameOverUi == null)
            {
                return;
            }

            prizeToTakeBack = prize;

            gameOverUi.SetActive(true);

            if (gameOverUiScore != null)
            {
                gameOverUiScore.text = scoreValue.ToString();
            }

            if(prizeImage != null)
            {
                prizeImage.sprite = prize.PrizeSprite;
            }

            if(prizeText != null)
            {
                prizeText.text = prize.PrizeName;
            }

            //Set prize stuff
        }

        public void BackToHomeWithPrize()
        {
            SlimeCareStats.Instance.SetPrize(prizeToTakeBack);
            StartCoroutine(LoadSlimeCareRoutine());
            
        }

        public void BackToHomeNoPrize()
        {
            SlimeCareStats.Instance.SetPrize(null);
            StartCoroutine(LoadSlimeCareRoutine());
        }

        private IEnumerator LoadSlimeCareRoutine()
        {
            yield return StartCoroutine(SceneChangeInstance.Instance.FillImage());
            SceneManager.LoadScene("SlimeCare", LoadSceneMode.Single);
        }
    }
}
