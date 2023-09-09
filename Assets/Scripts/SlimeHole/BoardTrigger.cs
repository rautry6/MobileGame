using UnityEngine;

namespace SlimeHole
{
    public class BoardTrigger : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Slime"))
            {
                SlimeBoardHistory.WasOnBoardPreviously = true;
                scoreManager.UpdateScore(1);
            }
        }
    }
}
