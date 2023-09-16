using JelloFelloInterfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SlimeCare
{
    public class SlimeCareSceneManager : MonoBehaviour, IOrientLandscape
    {
        private void Start()
        {
            SceneManager.sceneLoaded += OnSlimeCareSceneLoaded;
            LockScreenToLandscape();
        }

        private void OnSlimeCareSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {

        }

        public void LockScreenToLandscape()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        public void LoadPlinkoScene()
        {
            SceneManager.LoadScene("Plinko", LoadSceneMode.Single);
        }
        public void LoadSkeeballScene()
        {
            SceneManager.LoadScene("Skeeball", LoadSceneMode.Single);
        }
        public void LoadSlimeHoleScene()
        {
            SceneManager.LoadScene("TEST_SlimeHole", LoadSceneMode.Single);
        }
    
    }
}
