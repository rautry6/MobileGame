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

    private float v;
    private float t;
    private float d;

    private void Awake()
    {
        //Create new instance of the Input Controller
        plinkoController = new PlinkoControls();

        rb = plinkoDrop.GetComponent<Rigidbody>();
        startingLocation = plinkoDrop.transform.position;
        startingRotation = plinkoDrop.transform.rotation;

        moveOffset = (transform.right * moveXOffset);
        moveEndPosition = plinkoDrop.transform.position + moveOffset;
    }

    private void OnEnable()
    {
        //Get the input actions from the cotroller
        drop = plinkoController.Plinko.Drop; 
        reset = plinkoController.Plinko.Reset;

        //Enable them
        drop.Enable();
        reset.Enable();
    }

    private void OnDisable()
    {
        //Disable actions (Avoids memory leaks)
        drop.Disable();
        reset.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drop.WasPressedThisFrame())
        {
            move = false;
            DOTween.Kill(plinkoDrop.transform);

            rb.useGravity = true;
        }

        if (reset.WasPressedThisFrame())
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
            //Keeps the speed of the slime the same no matter starting position

            float fullDistance = Vector3.Distance(startingLocation - moveOffset, startingLocation + moveOffset);

            //Calculate velocity of object given the full distance and move time
            var velocity = fullDistance / moveTime;


            //Calculates how much further the object needs to move
            var remainingDistance = Vector3.Distance(plinkoDrop.transform.position, moveEndPosition);

            //Calculate time to move the required distance
            float time = remainingDistance / velocity;


            if(!inMotion)
            {
                inMotion = true;

                plinkoDrop.transform.DOMove(moveEndPosition, time)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                {
                    moveOffset = -moveOffset;
                    moveEndPosition = startingLocation + moveOffset;
                    inMotion = false;
                })
                    .OnKill(() =>
                {
                    rb.AddForce((moveOffset.x > 0 ? new Vector3(rb.mass * (velocity / time) * 0.5f, 0, 0) : new Vector3(-rb.mass * (velocity / time) * 0.5f, 0, 0)), ForceMode.Impulse);
                });
            }
        }
    }
}
