using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuCanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;

        public void OpenSettingsPanel()
        {
            settingsPanel.SetActive(true);
        }
        public void CloseSettingsPanel()
        {
            settingsPanel.SetActive(false);
        }

        public void StartGame()
        {
            StartCoroutine(HandleStartGameRoutine());
        }

        private IEnumerator HandleStartGameRoutine()
        {
            yield return StartCoroutine(SceneChangeInstance.Instance.FillImage());

            if (PlayerPrefs.GetInt("FirstPlay", 0) == 0)
            {
                SceneManager.LoadScene("SlimeCreation");
            }
            else
            {
                SceneManager.LoadScene("SlimeCare");
            }
        }
    }
}
