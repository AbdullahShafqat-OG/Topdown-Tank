using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform[] _turretBarrels;

    [SerializeField]
    private TurretData _turretData;

    private bool _canShoot = true;
    private Collider2D[] _tankColliders;
    private float _currentDelay = 0;

    private ObjectPool _bulletPool;
    [SerializeField]
    private int _bulletPoolSize = 10;

    public UnityEvent OnShoot, OnCantShoot;
    public UnityEvent<float> OnReloading;

    private void Awake()
    {
        _tankColliders = GetComponentsInChildren<Collider2D>();
        _bulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        _bulletPool.Initialize(_turretData.bulletPrefab, _bulletPoolSize);
        OnReloading?.Invoke(_currentDelay);
    }

    public void Shoot()
    {
        if (_canShoot)
        {
            _canShoot = false;
            _currentDelay = _turretData.reloadDelay;

            foreach (Transform barrel in _turretBarrels)
            {
                GameObject bullet = _bulletPool.CreateObject();
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize(_turretData.bulletData);
                foreach (Collider2D collider in _tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }

            OnShoot?.Invoke();
            OnReloading?.Invoke(_currentDelay);
        }
        else
        {
            OnCantShoot?.Invoke();
        }
    }

    private void Update()
    {
        if (!_canShoot)
        {
            OnReloading?.Invoke(_currentDelay);

            _currentDelay -= Time.deltaTime;
            if (_currentDelay <= 0)
                _canShoot = true;
        }
    }
}
