using UnityEngine;

namespace SlimeHole
{
    public class HoleTrigger : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                scoreManager.UpdateScore(3);
            }
        }
    }
}
