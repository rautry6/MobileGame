using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace SlimeHole
{
    public class SlimeHoleInputVTwo : MonoBehaviour
    {
        [SerializeField] private GameObject slime;
        [SerializeField] private SlimeThrowUI slimeThrowUI;
        [SerializeField] private MinigameFinish miniGameFinish;
        [SerializeField] private ScoreManager slimeHoleScoreManager;
        [SerializeField] private float testForceScalar = 100f;
        [SerializeField] private TMP_Text countDownText;
        
        private PlinkoControls _controls;
        private Rigidbody _rb;
        private bool _hasLaunched;
        private int _slimesRemaining = 8;
        private int _currentThrow;
        private bool _setScoreOnce = true;
        private JellyMesh _jellyMesh;
        private bool _canThrow;
        private Vector3 _startMousePosition;
        private Vector3 _endMousePosition;
        private float _touchStartTime;
        private InputAction _newInput;
        private Vector3 _startingPosition;
        private float _waitTimeAfterClickingPlayInEditor = 2f;
        private float _waitTimeAfterStartGame = 3f;

        private void Awake()
        {
            //Create new instance of the Input Controller
            _controls = new PlinkoControls();
            _rb = slime.GetComponent<Rigidbody>();
            _startingPosition = slime.transform.position;
            _jellyMesh = slime.GetComponent<JellyMesh>();
        }

        private void OnEnable()
        {
            _newInput = _controls.SlimeHole.NewInput;
            EnableInputActions();
        }

        private void EnableInputActions()
        {
            _newInput.Enable();
        }

        private void OnDisable()
        {
            DisableInputActions();
        }

        private void DisableInputActions()
        {
            _newInput.Disable();
        }

        void Update()
        {
            if(!_canThrow) { return; }

            if (_slimesRemaining <= 0 && _setScoreOnce)
            {
                DisableInputActions();
                _setScoreOnce = false;
                StartCoroutine(WaitThenDisplayGameOverUI());
            }

            if (!_hasLaunched && _newInput.WasPressedThisFrame())
            {
                StartTouch(Touchscreen.current.position.ReadValue());
            } else if (!_hasLaunched && _newInput.WasReleasedThisFrame())
            {
                EndTouch(Touchscreen.current.position.ReadValue());
            }
            
#if UNITY_EDITOR
            _waitTimeAfterClickingPlayInEditor -= Time.deltaTime;
            if (_waitTimeAfterClickingPlayInEditor <= 0)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    ResetSlime();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    // Simulate touch start
                    StartTouch(Input.mousePosition);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    // Simulate touch end
                    EndTouch(Input.mousePosition);
                }
            }
#endif
        }

        private void StartTouch(Vector3 position)
        {
            _startMousePosition = position;
            _touchStartTime = Time.time;
        }

        private void EndTouch(Vector3 position)
        {
            _rb.useGravity = true;
            _endMousePosition = position;
            var swipeDirection = (_endMousePosition - _startMousePosition).normalized;
            var swipeMagnitude = swipeDirection.magnitude;
            var swipeTime = Time.time - _touchStartTime;
            var swipeSpeed = swipeMagnitude / swipeTime;
            var verticalLift = 5f;
            var forceVector = new Vector3(swipeDirection.x * swipeSpeed, verticalLift, swipeDirection.y
                * swipeSpeed) * testForceScalar;
            _rb.AddForce(forceVector);
            _currentThrow++;
            _hasLaunched = true;
            slimeThrowUI.Thrown(_currentThrow);
            StartCoroutine(SlimeThrowCooldown());
        }

        private IEnumerator WaitThenDisplayGameOverUI()
        {
            yield return new WaitForSeconds(3.5f);
            miniGameFinish.DisplayGameOverUi(slimeHoleScoreManager.currentScore, slimeHoleScoreManager.currentPrize);
        }

        private IEnumerator SlimeThrowCooldown()
        {
            _slimesRemaining--;
            DisableInputActions();
            yield return new WaitForSeconds(3.5f);
            if (_slimesRemaining > 0)
            {
                EnableInputActions();
                ResetSlime();    
            }
        }

        void ResetSlime()
        {
            _jellyMesh.enabled = false;
            _rb.useGravity = false; 
            _rb.velocity = Vector3.zero;
            _rb.Sleep();
            _rb.WakeUp();
            slime.transform.position = _startingPosition;
            _hasLaunched = false;
            SlimeBoardHistory.WasOnBoardPreviously = false;
        }

        public void StartGame()
        {
            StartCoroutine(StartGameDelay());
        }

        private IEnumerator StartGameDelay()
        {
            countDownText.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            countDownText.SetText(2.ToString());
            yield return new WaitForSecondsRealtime(1f);
            countDownText.SetText(1.ToString());
            yield return new WaitForSecondsRealtime(1f);
            countDownText.SetText("Play!");
            yield return new WaitForSecondsRealtime(0.5f);
            countDownText.gameObject.SetActive(false);
            _canThrow = true;
        }
    }
}
