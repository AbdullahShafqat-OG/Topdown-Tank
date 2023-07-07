using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDynamicPatrolBehaviour : AIBehaviour
{
    // TODO patrolling behaviour breaks with longer wait times
    private PatrolPath _patrolPath;
    [SerializeField]
    private float _distanceThreshold = 1;
    [SerializeField]
    private float _waitTime = 0.5f;

    private bool _isWaiting = false;
    private Vector2 _currentPatrolTarget = Vector2.zero;
    private bool _isInitialized = false;

    private int _currentIndex = -1;

    private void Awake()
    {
        _patrolPath = GetComponentInChildren<PatrolPath>();
    }

    public override void PerformAction(TankController tank, AIDetector detector)
    {
        if (!_isWaiting)
        {
            if (_patrolPath.PatrolPointsLength < 2)
                return;
            if (!_isInitialized)
            {
                var currentPathPoint = _patrolPath.GetClosestPathPoint(tank.transform.position);
                this._currentIndex = currentPathPoint.Index;
                this._currentPatrolTarget = currentPathPoint.Position;
                _isInitialized = true;
            }
            if (Vector2.Distance(tank.transform.position, _currentPatrolTarget) < _distanceThreshold)
            {
                _isWaiting = true;
                StartCoroutine(WaitCoroutine());
                return;
            }
            Vector2 directionToGo = _currentPatrolTarget - (Vector2)tank.TankMover.transform.position;
            var dotProduct = Vector2.Dot(tank.TankMover.transform.up, directionToGo.normalized);

            if (dotProduct < 0.98f)
            {
                var crossProduct = Vector3.Cross(tank.TankMover.transform.up, directionToGo.normalized);
                int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                tank.HandleMoveBody(new Vector2(rotationResult, 1));
            }
            else
            {
                tank.HandleMoveBody(Vector2.up);
            }

        }

        IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_waitTime);
            var nextPathPoint = _patrolPath.GetNextPathPoint(_currentIndex);
            _currentPatrolTarget = nextPathPoint.Position;
            _currentIndex = nextPathPoint.Index;
            _isWaiting = false;
        }
    }
}
