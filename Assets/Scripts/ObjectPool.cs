using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject _objectToPool;
    private int _poolSize = 10;

    private Queue<GameObject> _objectPool;

    private Transform _spawnedObjectsParent;

    private void Awake()
    {
        _objectPool = new Queue<GameObject>();
    }

    public void Initialize(GameObject objectToPool, int poolSize = 10)
    {
        _objectToPool = objectToPool;
        _poolSize = poolSize;
    }

    public GameObject CreateObject()
    {
        CreateObjectParent();

        GameObject spawnedObject = null;

        if (_objectPool.Count < _poolSize)
        {
            spawnedObject = Instantiate(_objectToPool, transform.position, Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + _objectToPool.name + "_" + _objectPool.Count;
            spawnedObject.transform.SetParent(_spawnedObjectsParent);
        }
        else
        {
            spawnedObject = _objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }

        _objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    private void CreateObjectParent()
    {
        if (_spawnedObjectsParent == null)
        {
            string name = "ObjectPool_" + transform.root.name + "_" + _objectToPool.name;
            _spawnedObjectsParent = new GameObject(name).transform;
        }
    }

    private void OnDestroy()
    {
        if (_spawnedObjectsParent != null)
            Destroy(_spawnedObjectsParent.gameObject);
    }
}
