using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionMakeSound : MonoBehaviour
{
    public AudioSource src;

    private void OnCollisionEnter(Collision collision)
    {
        src.Play();
    }
}
