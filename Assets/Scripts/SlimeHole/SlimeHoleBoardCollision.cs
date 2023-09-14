using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHoleBoardCollision : MonoBehaviour
{
    private JellyMesh jellyMesh;

    private void Start()
    {
        jellyMesh = GetComponent<JellyMesh>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Board"))
        {
            jellyMesh.enabled = true;
        }
    }
}
