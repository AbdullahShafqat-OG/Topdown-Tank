using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootBehaviour : AIBehaviour
{
    [SerializeField]
    private float _shootingFOV = 60;

    public override void PerformAction(TankController tank, AIDetector detector)
    {
        if (TargetInFOV(tank, detector))
        {
            tank.HandleMoveBody(Vector2.zero);
            tank.HandleShoot();
        }
        tank.HandleTurretMovement(detector.Target.position);
    }

    private bool TargetInFOV(TankController tank, AIDetector detector)
    {
        var direction = detector.Target.position - tank.AimTurret.transform.position;
        if (Vector2.Angle(tank.AimTurret.transform.right, direction) < _shootingFOV / 2)
        {
            return true;
        }
        return false;
    }
}
