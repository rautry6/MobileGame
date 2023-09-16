using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using UI;

public class SkeeballInput : MonoBehaviour
{
    [SerializeField] private Transform slime;
    [SerializeField] private SlimeThrowUI slimeThrowUI;
    [SerializeField] private MinigameFinish gameOver;
    [SerializeField] private SkeeballScoreManager scoreManager;
    
    private int currentThrow = 0;
    private int maxThrows;

    [Header("Position")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float moveTime = 3f;

    private bool confirmingPosition;
    private bool moving;
    private bool moveRight;

    //This shows the player the direction the slime is going to go
    [SerializeField] private GameObject trajectoryIndicator;

    [Header("Rotation")]
    [SerializeField] private float maxRotationOffset;
    [SerializeField] private float rotationTime = 2f;
    private bool confirmingRotation;

    private bool rotating;
    private bool rotateRight;
    private float currentRotation;

    [Header("Power")]

    //This displays how much power the player is going to throw the slime with
    [SerializeField] private Slider powerBar;
    [SerializeField] private float powerChangeAmount = 2f;
    [SerializeField] private Vector3 power;

    private bool confirmingPower;
    private float powerMultiplier;
    private float maxPowerMultiplier;
    private bool powerIncreasing;

    PlinkoControls inputController;
    InputAction confirm;
    InputAction reset;

    Rigidbody rb;
    Vector3 startingLocation;
    Quaternion startingRotation;
    private bool canConfirm;

    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera vc;
    Vector3 vcStartingPosition;
    Vector3 vcOffsets;

    private JellyMesh jellyMesh;

    private void Awake()
    {
        inputController = new PlinkoControls();

        canConfirm = false;
        confirmingPosition = true;

        maxPowerMultiplier = powerBar.maxValue;
        powerBar.value = powerBar.minValue;
        powerMultiplier = powerBar.minValue;

        powerBar.gameObject.SetActive(false);

        rb = slime.GetComponent<Rigidbody>();
        startingLocation = slime.transform.position;
        startingRotation = slime.transform.rotation;
        vcStartingPosition = vc.transform.position;
        vcOffsets = vc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        maxThrows = slimeThrowUI.numberOfThrows;

        jellyMesh = slime.GetComponent<JellyMesh>();
    }

    private void OnEnable()
    {
        confirm = inputController.Skeeball.Confirm;
        confirm.Enable();

        reset = inputController.Skeeball.Reset;
        //reset.Enable();
    }

    private void OnDisable()
    {
        confirm.Disable();
        reset.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(reset.WasPerformedThisFrame())
        {
            ResetSlime();
        }

        if(!canConfirm)
        {
            return;
        }

        if(confirmingPosition)
        {
            if (!moving) 
            {

                moving = true;

                //Keeps the speed of the slime the same no matter starting position

                float fullDistance = Vector3.Distance(leftPoint.position, rightPoint.position);

                //Calculate velocity of object given the full distance and move time
                var velocity = fullDistance / moveTime;

                Vector3 moveEndPosition;

                if(moveRight)
                {
                    moveEndPosition = rightPoint.position;
                }
                else
                {
                    moveEndPosition = leftPoint.position;
                }

                //Calculates how much further the object needs to move
                var remainingDistance = Vector3.Distance(slime.transform.position, moveEndPosition);

                //Calculate time to move the required distance
                float time = remainingDistance / velocity;

                slime.transform.DOMoveX(moveEndPosition.x, time)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                {
                    moveRight = !moveRight;
                    moving = false;
                });

            }

            //Player confirms their position so stop moving and start rotation confirmation stuff
            if (confirm.WasPerformedThisFrame())
            {
                DOTween.Kill(slime.transform);
                confirmingPosition = false;
                confirmingRotation = true;
            }

            return;
        }

        if(confirmingRotation)
        {
            if (!rotating)
            {
                rotating = true;

                float targetRotation;

                if (rotateRight)
                {
                    targetRotation = maxRotationOffset * 2;
                }
                else
                {
                    targetRotation = -maxRotationOffset * 2;
                }
                
                if(slime.transform.rotation.y == 0)
                {
                    targetRotation /= 2;
                }

                //Calculate time to move the required distance
                float time = Mathf.Abs(targetRotation) == maxRotationOffset ? rotationTime/2 : rotationTime;

                slime.transform.DORotate(new Vector3(0, targetRotation, 0), time, RotateMode.WorldAxisAdd)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                {
                    rotateRight = !rotateRight;
                    rotating = false;
                });
            }

            //Player confirms their rotation so stop rotating and handle power stuff
            if (confirm.WasPerformedThisFrame())
            {
                DOTween.Kill(slime.transform);
                confirmingRotation = false;
                confirmingPower = true;

                power.x =(slime.transform.localEulerAngles.y > maxRotationOffset ? slime.transform.localEulerAngles.y - 360 : slime.transform.localEulerAngles.y) * 0.05f;

                powerBar.gameObject.SetActive(true);
            }

            return;
        }

        if(confirmingPower)
        {
            if (powerIncreasing)
            {
                powerMultiplier += powerChangeAmount * Time.deltaTime;

                if(powerMultiplier >= maxPowerMultiplier)
                {
                    powerIncreasing = false;
                }
            }
            else
            {
                powerMultiplier -= powerChangeAmount * Time.deltaTime;

                if(powerMultiplier <= powerBar.minValue)
                {
                    powerIncreasing = true;
                }

            }

            powerBar.value = powerMultiplier;

            if (confirm.WasPerformedThisFrame())
            {
                LaunchBall();
                confirmingPower = false;
                canConfirm = false;
            }
            return;
        }
    }

    void LaunchBall()
    {
        currentThrow++;
        slimeThrowUI.Thrown(currentThrow);

        vc.Follow = null;

        vc.transform.position = vcStartingPosition + vcOffsets;

        trajectoryIndicator.SetActive(false);

        rb.AddForce(power * powerMultiplier, ForceMode.Impulse);
        rb.useGravity = true;
        jellyMesh.enabled = true;
    }

    public void ResetSlime()
    {

        if(currentThrow >= maxThrows)
        {
            //Game Over stuff
            powerBar.gameObject.SetActive(false);
            gameOver.DisplayGameOverUi(scoreManager.currentScore, scoreManager.currentPrize);

            return;
        }

        DOTween.KillAll();
        jellyMesh.enabled = false;


        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        slime.transform.position = startingLocation;
        slime.transform.rotation = startingRotation;

        rb.constraints = RigidbodyConstraints.None;

        vc.Follow = slime;
      

        moving = false;
        rotating = false;

        canConfirm = true;
        confirmingPosition = true;
        trajectoryIndicator.SetActive(true);

        powerBar.value = powerBar.minValue;
        powerMultiplier = powerBar.minValue;
        powerBar.gameObject.SetActive(false);

    }

    public void ResetLastThrow()
    {
        currentThrow--;
        ResetSlime();
    }

    public void StartGame()
    {
        canConfirm = true;
    }
}
