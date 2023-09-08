using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlimeHoleInput : MonoBehaviour
{
    [SerializeField] private GameObject slime;
    [SerializeField] private TMP_Text xText;
    [SerializeField] private TMP_Text yText;
    [SerializeField] private TMP_Text zText;
    [SerializeField] private TMP_Text forceText;
    [SerializeField] private Canvas debugText;
    Vector3 startingPosition;

    PlinkoControls controls;
    InputAction touch;
    InputAction reset;

    Rigidbody rb;

    Vector3 inputDirection;

    float forwardLaunchPower = 10f;
    float verticalPower = 0.5f;
    float horizontalPower = 0.2f;

    float maxY = 20f;
    private bool _hasLaunched;

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
        if (!_hasLaunched)
        {
            inputDirection = touch.ReadValue<Vector3>();
            xText.SetText($"X: {inputDirection.x}");
            yText.SetText($"Y: {inputDirection.y}");
            zText.SetText($"Z: {inputDirection.z}");
        }

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
            forceText.SetText($"Force: X: {forceVector.x}, Y: {forceVector.y}, Z: {forceVector.z}");
            rb.AddForce(forceVector, ForceMode.Impulse);
            _hasLaunched = true;
            inputDirection = Vector3.zero;
            rb.useGravity = true;
        }
    }

    void ResetSlime()
    {
        rb.useGravity = false; 
        rb.velocity = Vector3.zero;
        rb.Sleep();
        rb.WakeUp();
        slime.transform.position = startingPosition;
        _hasLaunched = false;
        
    }
}
