using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/TurretData")]
public class TurretData : ScriptableObject
{
    public GameObject bulletPrefab;
    public float reloadDelay = 0.5f;
    public BulletData bulletData;
}
