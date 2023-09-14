using UnityEngine;

namespace SlimeHole
{
    public class HoleTrigger : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private AudioSource holeAudioSource;
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Slime"))
            {
                holeAudioSource.PlayOneShot(holeAudioSource.clip);
                scoreManager.UpdateScore(3);
            }
        }
    }
}
