using System;
using System.Collections;
using System.Collections.Generic;
using SlimeCare;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using DG.Tweening;

public class SlimeWander : MonoBehaviour
{
    [SerializeField] private GameObject _slimeGameObject;
    [SerializeField] private Transform _slimeWaitingZone;
    [SerializeField] private float maxWanderZoneRadius;
    [SerializeField] private float minWanderZoneRadius;
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float minWanderTime;
    [SerializeField] private float maxWanderTime;
    [SerializeField] private float maxIdleTime;
    [SerializeField] private float runSpeed = 5f;
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
                    if (_slimeWaitingZone.position.x != _slimeGameObject.transform.position.x)
                    {
                        var direction = (_slimeWaitingZone.position - _slimeGameObject.transform.position).normalized;
                        _slimeRigidbody.velocity = direction * (runSpeed * Time.deltaTime);
                    }
                    else
                    {
                        _slimeRigidbody.velocity = Vector3.zero;
                    }
                    break;
                }
            case SlimeCareStates.Eating:
                {
                    if (_foodLocation.position.x != _slimeGameObject.transform.position.x)
                    {
                        var direction = (_foodLocation.position - _slimeGameObject.transform.position).normalized;
                        _slimeRigidbody.velocity = direction * (runSpeed * Time.deltaTime);
                    }
                    else
                    {
                        _slimeRigidbody.velocity = Vector3.zero;
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
}
