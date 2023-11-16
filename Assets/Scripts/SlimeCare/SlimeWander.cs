using System;
using System.Collections;
using System.Collections.Generic;
using SlimeCare;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using DG.Tweening;
using System.Globalization;

public class SlimeWander : MonoBehaviour
{
    [SerializeField] private GameObject _slimeGameObject;
    [SerializeField] private SkinnedMeshRenderer _slimeSkin;
    [SerializeField] private Transform _slimeWaitingZone;
    [SerializeField] private float maxWanderZoneRadius;
    [SerializeField] private float minWanderZoneRadius;
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float minWanderTime;
    [SerializeField] private float maxWanderTime;
    [SerializeField] private float maxIdleTime;
    [SerializeField] private float stopDistance = .1f;
    [SerializeField] private float smoothTime = 0.3f;
    
    private Rigidbody _slimeRigidbody;
    private Vector3 _desiredDestination;
    private float _currentWanderTime;
    private float _maxWanderTime;
    private float _currentIdleTime;
    private Vector3 _slimeStartingPosition;
    private SlimeCareStates _currentCareState;
    private bool _isWandering;
    private bool _isWaiting;
    private bool _isEating;
    private Transform _foodLocation;
    private Vector3 _currentVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _slimeRigidbody = _slimeGameObject.GetComponent<Rigidbody>();
        _slimeStartingPosition = _slimeGameObject.transform.position;
        UpdateCurrentState(SlimeCareStates.Idle);
    }

    private void FindNewDestination()
    {
        var angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        var distance = Random.Range(minWanderZoneRadius, maxWanderZoneRadius);
        
        _desiredDestination = new Vector3(_slimeStartingPosition.x + distance * Mathf.Cos(angle),
            _slimeStartingPosition.y, _slimeStartingPosition.z + distance * Mathf.Sin(angle));
        _maxWanderTime = Random.Range(minWanderTime, maxWanderTime);
#if UNITY_EDITOR
        Debug.DrawRay(_desiredDestination, Vector3.up * 5f, Color.red, _maxWanderTime);
#endif
        _currentWanderTime = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, maxWanderZoneRadius);
    }

    private void MoveToWanderDestination()
    {
        _isWandering = true;
        var direction = (_desiredDestination - _slimeGameObject.transform.position).normalized;
        _slimeRigidbody.velocity = direction * (wanderSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentCareState)
        {
            case SlimeCareStates.Idle:
            {
                _slimeRigidbody.velocity = Vector3.zero;
                _currentIdleTime += Time.deltaTime;
                if (_currentIdleTime > maxIdleTime)
                {
                    _currentIdleTime = 0f;
                    UpdateCurrentState(SlimeCareStates.Wander);
                }
                break;
            }
            case SlimeCareStates.Wander:
            {
                if (!_isWandering)
                {
                    FindNewDestination();    
                    MoveToWanderDestination();
                } else if (_isWandering)
                {
                    _currentWanderTime += Time.deltaTime;
                    if (_currentWanderTime > _maxWanderTime)
                    {
                        _isWandering = false;
                        UpdateCurrentState(SlimeCareStates.Idle);
                    }
                }
                
                break;
            }
            case SlimeCareStates.Waiting:
                {
                    if (Vector3.Distance(_slimeGameObject.transform.position, _slimeWaitingZone.position) > stopDistance)
                    {
                        MoveSmoothlyTo(_slimeWaitingZone.position);
                    }
                    else
                    {
                        MoveSmoothlyTo(GetExcitedPosition());
                    }
                    break;
                }
            case SlimeCareStates.Eating:
                {
                    if (Vector3.Distance(_slimeGameObject.transform.position, _foodLocation.position) > .01f)
                    {
                        MoveSmoothlyTo(_foodLocation.position);
                    }
                    break;
                }
        }
    }

    public void StartWaiting()
    {
        _isWaiting = false;
        UpdateCurrentState(SlimeCareStates.Waiting);
    }

    public void StartEating(Transform foodPosition)
    {
        _foodLocation = foodPosition;
        _isEating = false;
        UpdateCurrentState(SlimeCareStates.Eating);
    }

    public void DoneEating()
    {
        UpdateCurrentState(SlimeCareStates.Idle);
    }

    private void UpdateCurrentState(SlimeCareStates newState)
    {
        _currentCareState = newState;
    }

    private void MoveSmoothlyTo(Vector3 destination)
    {
        var newPosition = Vector3.SmoothDamp(_slimeGameObject.transform.position, destination, ref _currentVelocity,
            smoothTime);
        _slimeRigidbody.MovePosition(newPosition);
    }

    private Vector3 GetExcitedPosition()
    {
        var angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        var distance = Random.Range(-1f, 1f);
        return new Vector3(_slimeWaitingZone.position.x + distance * Mathf.Cos(angle), _slimeWaitingZone.position.y,
            _slimeWaitingZone.position.z + distance * Mathf.Sin(angle));
    }
}
