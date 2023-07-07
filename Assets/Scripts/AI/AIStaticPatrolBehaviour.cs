using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStaticPatrolBehaviour : AIBehaviour
{
    [SerializeField]
    private float _patrolDelay = 4;

    private Vector2 _randomDirection = Vector2.zero;
    private float _currentPatrolDelay = 0;

    private void Awake()
    {
        _randomDirection = Random.insideUnitCircle;
    }

    public override void PerformAction(TankController tank, AIDetector detector)
    {
        float angle = Vector2.Angle(tank.AimTurret.transform.right, _randomDirection);
        if (_currentPatrolDelay <= 0 && (angle < 2))
        {
            _randomDirection = Random.insideUnitCircle;
            _currentPatrolDelay = _patrolDelay;
        }
        else
        {
            if (_currentPatrolDelay > 0)
                _currentPatrolDelay -= Time.deltaTime;
            else
                tank.HandleTurretMovement((Vector2)tank.AimTurret.transform.position + _randomDirection);
        }
    }
}
