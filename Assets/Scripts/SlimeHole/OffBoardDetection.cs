using UnityEngine;

namespace SlimeHole
{
    public class OffBoardDetection : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private AudioSource audioSource;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Slime"))
            {
                if (SlimeBoardHistory.WasOnBoardPreviously)
                {
                    if (scoreManager.currentScore > 0)
                    {
                        scoreManager.UpdateScore(-1);    
                    }
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
        }
    }
}
