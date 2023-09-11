using JelloFelloInterfaces;
using SlimeCare;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MinigameFinish : MonoBehaviour, IOrientPortrait
    {
        [SerializeField] GameObject gameOverUi;
        [SerializeField] TMP_Text gameOverUiScore;

        // Start is called before the first frame update
        void Start()
        {
            LockScreenToPortrait();
        }

        public void LockScreenToPortrait()
        {
            Screen.orientation = ScreenOrientation.Portrait;
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
            SlimeCareStats.Instance.UpdateHealthAndHappiness(30f, 150f);
            SceneManager.LoadScene("SlimeCare", LoadSceneMode.Single);
        }

        public void BackToHomeNoPrize()
        {
            SceneManager.LoadScene("SlimeCare", LoadSceneMode.Single);
        }
    }
}
