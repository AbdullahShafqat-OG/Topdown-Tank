using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerUtil : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectPrefab;
    [SerializeField]
    private float _radius = 0.1f;

    Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * _radius + (Vector2)transform.position;
    }

    Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    public void CreateObject()
    {
        GameObject obj = GetObject();
        obj.transform.position = GetRandomPosition();
        obj.transform.rotation = GetRandomRotation();
    }

    protected virtual GameObject GetObject()
    {
        return Instantiate(_objectPrefab);
    }

    private void OnDrawGizmosSelected()
    {
        Helpers.DrawWireDisk(transform.position, _radius, Color.magenta);
    }
}
