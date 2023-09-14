using UnityEngine;

namespace SlimeHole
{
    public class OffBoardDetection : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Slime"))
            {
                if (SlimeBoardHistory.WasOnBoardPreviously)
                {
                    scoreManager.UpdateScore(-1);
                }
            }
        }
    }
}
