using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    private TankMover _tankMover;
    public TankMover TankMover { get => _tankMover; private set => _tankMover = value; }


    private AimTurret _aimTurret;
    public AimTurret AimTurret { get => _aimTurret; private set => _aimTurret = value; }

    private Turret[] _turrets;

    private void Awake()
    {
        TankMover = GetComponentInChildren<TankMover>();
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
        TankMover.Move(movementVector);
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        AimTurret.Aim(pointerPosition);
    }
}
