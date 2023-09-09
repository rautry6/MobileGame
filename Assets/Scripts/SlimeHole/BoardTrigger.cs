using UnityEngine;

namespace SlimeHole
{
    public class BoardTrigger : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                scoreManager.UpdateScore(1);
            }
        }

        // private void OnTriggerExit(Collider other)
        // {
        //     if(other.CompareTag("Player"))
        //     {
        //         ScoreManager.CurrentScore--;
        //     }
        // }
    }
}
