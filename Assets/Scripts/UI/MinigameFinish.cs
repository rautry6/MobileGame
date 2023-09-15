using System.Collections;
using JelloFelloInterfaces;
using SlimeCare;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MinigameFinish : MonoBehaviour, IOrientPortrait
    {
        [SerializeField] GameObject gameOverUi;
        [SerializeField] TMP_Text gameOverUiScore;
        [SerializeField] Image prizeImage;
        [SerializeField] TMP_Text prizeText;

        // Start is called before the first frame update
        void Start()
        {
            LockScreenToPortrait();
        }

        public void LockScreenToPortrait()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        public void DisplayGameOverUi(int scoreValue, Prize prize)
        {
            Debug.Log(prize);

            if (gameOverUi == null)
            {
                return;
            }

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
            SlimeCareStats.Instance.UpdateHealthAndHappiness(30f, 150f);
            StartCoroutine(LoadSlimeCareRoutine());
            
        }

        public void BackToHomeNoPrize()
        {
            StartCoroutine(LoadSlimeCareRoutine());
        }

        private IEnumerator LoadSlimeCareRoutine()
        {
            yield return StartCoroutine(SceneChangeInstance.Instance.FillImage());
            SceneManager.LoadScene("SlimeCare", LoadSceneMode.Single);
        }
    }
}
