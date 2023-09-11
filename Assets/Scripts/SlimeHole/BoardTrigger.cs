using UnityEngine;

namespace SlimeHole
{
    public class BoardTrigger : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private AudioSource boardAudioSource;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Slime"))
            {
                SlimeBoardHistory.WasOnBoardPreviously = true;
                boardAudioSource.PlayOneShot(boardAudioSource.clip);
                scoreManager.UpdateScore(1);
            }
        }
    }
}
