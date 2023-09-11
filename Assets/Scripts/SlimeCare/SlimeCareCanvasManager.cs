using UnityEngine;

namespace SlimeCare
{
    public class SlimeCareCanvasManager : MonoBehaviour
    {
        [SerializeField] private GameObject miniGamesPanel;
        [SerializeField] private GameObject settingsPanel;

        public void OpenMiniGamesPanel()
        {
            miniGamesPanel.SetActive(true);
        }
        public void CloseMiniGamesPanel()
        {
            miniGamesPanel.SetActive(false);
        }
    
        public void OpenSettingsPanel()
        {
            settingsPanel.SetActive(true);
        }
        public void CloseSettingsPanel()
        {
            settingsPanel.SetActive(false);
        }
    }
}
