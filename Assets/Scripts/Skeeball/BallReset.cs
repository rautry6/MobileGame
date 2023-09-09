using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    [SerializeField] SkeeballInput input;
    [SerializeField] SlimeThrowUI ui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slime"))
        {
            input.ResetLastThrow();
            ui.ResetLastThrow();
        }
    }
}
