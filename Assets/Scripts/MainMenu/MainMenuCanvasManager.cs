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
            SceneManager.LoadScene("SlimeCare");
        }
    }
}
