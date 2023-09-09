using TMPro;
using UnityEngine;

namespace SlimeHole
{
    public class GravitationalPull : MonoBehaviour
    {
        [SerializeField] private float gravitationalPullStrength;

        private void OnTriggerStay(Collider other)
        {
            var rigidBody = other.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                var directionTowardsCenter = (transform.position - other.transform.position).normalized;
                rigidBody.AddForce(directionTowardsCenter * gravitationalPullStrength, ForceMode.Acceleration);
            }
        }
    }
}
