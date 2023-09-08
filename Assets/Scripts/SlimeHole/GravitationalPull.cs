using TMPro;
using UnityEngine;

namespace SlimeHole
{
    public class GravitationalPull : MonoBehaviour
    {
        [SerializeField] private float gravitationalPullStrength;
        [SerializeField] private TMP_Text gravText;

        private float elapsedTime;
        private void OnTriggerStay(Collider other)
        {
            var rigidBody = other.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                elapsedTime += Time.deltaTime;
                gravText.SetText($"{other.name}, ET {elapsedTime}");
                var directionTowardsCenter = (transform.position - other.transform.position).normalized;
                rigidBody.AddForce(directionTowardsCenter * gravitationalPullStrength, ForceMode.Acceleration);
            }
        }
    }
}
