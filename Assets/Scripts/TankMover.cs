using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankMover : MonoBehaviour
{
    [SerializeField]
    private TankMovementData _movementData;

    private Rigidbody2D _rb;
    private Vector2 _movementVector;

    private float _currentSpeed = 0;
    private float _currentForwardDirection = 1;

    public UnityEvent<float> OnSpeedChange = new UnityEvent<float>();

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        _movementVector = movementVector;
        CalculateSpeed();
        OnSpeedChange?.Invoke(_movementVector.magnitude);

        if (movementVector.y > 0)
        {
            if (_currentForwardDirection == -1)
                _currentSpeed = 0;
            _currentForwardDirection = 1;
        }
        else if (movementVector.y < 0)
        {
            if (_currentForwardDirection == 1)
                _currentSpeed = 0;
            _currentForwardDirection = -1;
        }
    }

    private void CalculateSpeed()
    {
        if (Mathf.Abs(_movementVector.y) > 0)
        {
            _currentSpeed += _movementData.acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= _movementData.deacceleration * Time.deltaTime;
        }
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _movementData.maxSpeed);
    }

    private void FixedUpdate()
    {
        _rb.velocity = (Vector2)transform.up * _currentSpeed * _currentForwardDirection * Time.deltaTime;
        _rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -_movementVector.x * _movementData.rotationSpeed * Time.deltaTime));
    }
}
