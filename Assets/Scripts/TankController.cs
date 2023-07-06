using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    private TankMover _tankMover;
    private AimTurret _aimTurret;
    private Turret[] _turrets;

    private void Awake()
    {
        _tankMover = GetComponentInChildren<TankMover>();
        _aimTurret = GetComponentInChildren<AimTurret>();
        _turrets = GetComponentsInChildren<Turret>();
    }

    public void HandleShoot()
    {
        foreach (Turret turret in _turrets)
        {
            turret.Shoot();
        }
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        _tankMover.Move(movementVector);
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        _aimTurret.Aim(pointerPosition);
    }
}
