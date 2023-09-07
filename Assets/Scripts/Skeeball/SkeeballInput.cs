using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;

public class SkeeballInput : MonoBehaviour
{
    [SerializeField] private Transform slime;

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

    [Header("Power")]

    //This displays how much power the player is going to throw the slime with
    [SerializeField] private Slider powerBar;
    [SerializeField] private float powerChangeAmount = 2f;

    private bool confirmingPower;
    private float powerMultiplier = 0f;
    private float maxPowerMultiplier;
    private bool powerIncreasing;

    PlinkoControls inputController;
    InputAction confirm;


    private bool canConfirm;
    private void Awake()
    {
        inputController = new PlinkoControls();


        //Testing
        canConfirm = true;
        confirmingPosition = true;

        maxPowerMultiplier = powerBar.maxValue;
        powerBar.value = 0;

        powerBar.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        confirm = inputController.Skeeball.Confirm;
        confirm.Enable();
    }

    private void OnDisable()
    {
        confirm.Disable();
    }

    // Update is called once per frame
    void Update()
    {
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

                if(powerMultiplier <= 0)
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

    }
}
