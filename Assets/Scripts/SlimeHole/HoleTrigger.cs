using UnityEngine;

namespace SlimeHole
{
    public class HoleTrigger : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Slime"))
            {
                scoreManager.UpdateScore(3);
            }
        }
    }
}
