using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateUtil : MonoBehaviour
{
    [SerializeField]
    private GameObject _object;

    public void InstantiateObject()
    {
        Instantiate(_object);
    }
}
