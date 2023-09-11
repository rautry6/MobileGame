using UnityEngine;
using UnityEngine.SceneManagement;

namespace SlimeHole
{
    public class ResetScene : MonoBehaviour
    {
        public void OnResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
