using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlinkoController : MonoBehaviour
{
    [SerializeField] private GameObject plinkoDrop;
    [SerializeField]private float moveXOffset = 20f;
    [SerializeField]private float moveTime = 1.5f;

    private Vector3 startingLocation;
    private Quaternion startingRotation;
    private Vector3 moveEndPosition;
    private Vector3 moveOffset;

    private bool move = true;
    private bool inMotion = false;

    private Rigidbody rb;

    PlinkoControls plinkoController;
    InputAction drop;
    InputAction reset;
    InputAction m;

    private float v;
    private float t;
    private float d;

    private float moveSpeed = 5f;
    private float maxX;
    private float minX;

    private void Awake()
    {
        //Create new instance of the Input Controller
        plinkoController = new PlinkoControls();

        rb = plinkoDrop.GetComponent<Rigidbody>();
        startingLocation = plinkoDrop.transform.position;
        startingRotation = plinkoDrop.transform.rotation;

        moveOffset = (transform.right * moveXOffset);
        moveEndPosition = plinkoDrop.transform.position + moveOffset;

        maxX = plinkoDrop.transform.position.x + moveOffset.x;
        minX = plinkoDrop.transform.position.x - moveOffset.x;
    }

    private void OnEnable()
    {
        //Get the input actions from the cotroller
        drop = plinkoController.Plinko.Drop; 
        reset = plinkoController.Plinko.Reset;
        m = plinkoController.Plinko.Move;

        //Enable them
        drop.Enable();
        //reset.Enable();
        m.Enable();
    }

    private void OnDisable()
    {
        //Disable actions (Avoids memory leaks)
        drop.Disable();
        reset.Disable();
        m.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drop.WasPerformedThisFrame())
        {
            move = false;
            DOTween.Kill(plinkoDrop.transform);

            rb.useGravity = true;
        }

        if (reset.WasPerformedThisFrame())
        {
            move = false;
            DOTween.Kill(plinkoDrop.transform);

            rb.useGravity = false;

            //Stops any spinning of slime
            rb.constraints = RigidbodyConstraints.FreezeAll;

            plinkoDrop.transform.position = startingLocation;
            plinkoDrop.transform.rotation = startingRotation;

            moveOffset = (transform.right * moveXOffset);
            moveEndPosition = startingLocation + moveOffset;

            rb.constraints = RigidbodyConstraints.FreezePositionZ;

            inMotion = false;

            move = true;
        }

        if(move)
        {
            Vector3 direction = m.ReadValue<Vector3>();

            float targetX = plinkoDrop.transform.position.x + (direction.x * moveSpeed * Time.deltaTime);

            if (targetX < minX)
            {
                targetX = minX;
            }
            else if (targetX > maxX)
            {
                targetX = maxX;
            }

            plinkoDrop.transform.position = new Vector3 (targetX, plinkoDrop.transform.position.y, plinkoDrop.transform.position.z);
        }
    }
}
