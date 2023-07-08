using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public float speed = 10;
    public int damage = 5;
    public float maxDistance = 10;
    public AudioClip impactSFX;
}
