using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlimeHoleInput : MonoBehaviour
{
    [SerializeField] private GameObject slime;

    Vector3 startingPosition;

    PlinkoControls controls;
    InputAction touch;
    InputAction reset;

    Rigidbody rb;

    Vector3 inputDirection;

    float forwardLaunchPower = 10f;
    float verticalPower = 0.2f;
    float horizontalPower = 0.2f;

    float maxY = 20f;

    private void Awake()
    {
        //Create new instance of the Input Controller
        controls = new PlinkoControls();
        rb = slime.GetComponent<Rigidbody>();
        startingPosition = slime.transform.position;
    }

    private void OnEnable()
    {
        touch = controls.SlimeHole.Input;
        reset = controls.SlimeHole.Reset;
        touch.Enable();
        reset.Enable();
    }

    private void OnDisable()
    {
        touch.Disable();
        reset.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = touch.ReadValue<Vector3>();

        if (reset.WasPerformedThisFrame())
        {
            ResetSlime();
        }
    }

    private void FixedUpdate()
    {
        float y = inputDirection.y < maxY ? inputDirection.y : maxY;
        float x = inputDirection.x;

        if(y > 0)
        {
            Debug.Log("y " + y);
            Debug.Log("x " + x);

            Vector3 forceVector = new Vector3(x * horizontalPower, y * verticalPower, forwardLaunchPower);
            rb.AddForce(forceVector, ForceMode.Impulse);
            rb.useGravity = true;
        }
    }

    void ResetSlime()
    {
        rb.useGravity = false; 
        rb.velocity = Vector3.zero;
        slime.transform.position = startingPosition;
    }
}
