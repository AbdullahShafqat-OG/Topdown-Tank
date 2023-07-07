using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    private BulletData _bulletData;

    private Vector2 _startPosition;
    private float _coveredDistance = 0;
    private Rigidbody2D _rb;

    public UnityEvent OnHit = new UnityEvent();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(BulletData bulletData)
    {
        _bulletData = bulletData;
        _startPosition = transform.position;
        _rb.velocity = transform.up * _bulletData.speed;
    }

    private void Update()
    {
        _coveredDistance = Vector2.Distance(transform.position, _startPosition);
        if (_coveredDistance > _bulletData.maxDistance)
            DisableObject();
    }

    private void DisableObject()
    {
        _rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit?.Invoke();

        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
            damageable.Hit(_bulletData.damage);

        DisableObject();
    }
}
