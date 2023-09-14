using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SlimeHole
{
    public class SlimeHoleInput : MonoBehaviour
    {
        [SerializeField] private GameObject slime;
        [SerializeField] private TMP_Text xText;
        [SerializeField] private TMP_Text yText;
        [SerializeField] private TMP_Text zText;
        [SerializeField] private TMP_Text forceText;
        [SerializeField] private float textYDirection;
        [SerializeField] private SlimeThrowUI slimeThrowUI;
        [SerializeField] private MinigameFinish miniGameFinish;
        [SerializeField] private ScoreManager slimeHoleScoreManager;

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
        private int slimesRemaining = 8;
        private int currentThrow = 0;
        private bool _setScoreOnce = true;

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
            EnableInputActions();
        }

        private void EnableInputActions()
        {
            touch.Enable();
            reset.Enable();
        }

        private void OnDisable()
        {
            DisableInputActions();
        }

        private void DisableInputActions()
        {
            touch.Disable();
            reset.Disable();
        }

        void Update()
        {
            
            if (slimesRemaining <= 0 && _setScoreOnce)
            {
                touch.Disable();
                _setScoreOnce = false;
                StartCoroutine(WaitThenDisplayGameOverUI());

            }
            
            Debug.Log(currentThrow);
            if (!_hasLaunched)
            {
                inputDirection = touch.ReadValue<Vector3>();
                xText.SetText($"X: {inputDirection.x}");
                yText.SetText($"Y: {inputDirection.y}");
                zText.SetText($"Z: {inputDirection.z}");
            }
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                inputDirection = new Vector3(0.2f, textYDirection, 0);
            }
#endif
            // if (reset.WasPerformedThisFrame())
            // {
            //     ResetSlime();
            // }
        }

        private IEnumerator WaitThenDisplayGameOverUI()
        {
            yield return new WaitForSeconds(3.5f);
            miniGameFinish.DisplayGameOverUi(slimeHoleScoreManager.currentScore, slimeHoleScoreManager.currentPrize);
        }

        private void FixedUpdate()
        {
            float y = inputDirection.y < maxY ? inputDirection.y : maxY;
            float x = inputDirection.x;

            if(y > 0)
            {
                inputDirection = Vector3.zero;
                currentThrow++;
                slimeThrowUI.Thrown(currentThrow);
                Debug.Log("y " + y);
                Debug.Log("x " + x);
                Vector3 forceVector = new Vector3(x * horizontalPower, y * verticalPower, forwardLaunchPower);
                forceText.SetText($"Force: X: {forceVector.x}, Y: {forceVector.y}, Z: {forceVector.z}");
                rb.AddForce(forceVector, ForceMode.Impulse);
                StartCoroutine(SlimeThrowCooldown());
                _hasLaunched = true;
                rb.useGravity = true;
                
            }
        }

        private IEnumerator SlimeThrowCooldown()
        {
            slimesRemaining--;
            DisableInputActions();
            yield return new WaitForSeconds(3.5f);
            if (slimesRemaining > 0)
            {
                EnableInputActions();
                ResetSlime();    
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
            SlimeBoardHistory.WasOnBoardPreviously = false;
        }
    }
}
