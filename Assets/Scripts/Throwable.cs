using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField]
    private float _throwSpeed;
    private float _speed;
    private float _lastMouseX, _lastMouseY;

    private bool _thrown = false, _holding = false;

    private Rigidbody _rigidbody;
    private Vector3 _newPosition;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (_holding) {
            Debug.Log("holdind true");
            OnTouch();
        }

        if (_thrown) {
            return;
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f)) {
                Debug.Log("touching");
                if (hit.transform == transform) {
                    _holding = true;
                    transform.SetParent(null);
                }
            }
        }
        
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            if (_lastMouseY < Input.GetTouch(0).position.y) {
                Debug.Log("throwing");
                ThrowBall(Input.GetTouch(0).position);
            }
        }
        
        if (Input.touchCount == 1) {
            Debug.Log("this shit");
            _lastMouseX = Input.GetTouch(0).position.x;
            _lastMouseY = Input.GetTouch(0).position.y;
        }
    }

    void Reset() {
        CancelInvoke();
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, Camera.main.nearClipPlane * 7.5f));
        _newPosition = transform.position;
        _thrown = _holding = false;

        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0f, 200f, 0f);
        transform.SetParent(Camera.main.transform);
    }

    void OnTouch() {
        Vector3 mousePos = Input.GetTouch(0).position;
        mousePos.z = Camera.main.nearClipPlane * 7.5f;
        _newPosition = Camera.main.ScreenToWorldPoint(mousePos);

        transform.localPosition = Vector3.Lerp(transform.localPosition, _newPosition, 50f * Time.deltaTime);
    }

    void ThrowBall(Vector2 mousePos) {
        _rigidbody.useGravity = true;

        float differenceY = (mousePos.y - _lastMouseY) / (Screen.height * 100);
        _speed = _throwSpeed * differenceY;

        float x = (mousePos.x / Screen.width) - (_lastMouseX / Screen.width);
        x = Mathf.Abs(Input.GetTouch(0).position.x - _lastMouseX) / Screen.width * 100 * x;

        Vector3 direction = new Vector3(x, 0f, 1f);
        direction = Camera.main.transform.TransformDirection(direction);

        _rigidbody.AddForce((direction * _speed / 2f) + (Vector3.up * _speed));

        _holding = false;
        _thrown = true;

        Invoke("Reset", 5.0f);
    }
}
