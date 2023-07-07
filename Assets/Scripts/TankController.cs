using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    private TankMover _tankMover;

    private AimTurret _aimTurret;
    public AimTurret AimTurret { get => _aimTurret; private set => _aimTurret = value; }


    private Turret[] _turrets;

    private void Awake()
    {
        _tankMover = GetComponentInChildren<TankMover>();
        AimTurret = GetComponentInChildren<AimTurret>();
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
        AimTurret.Aim(pointerPosition);
    }
}
