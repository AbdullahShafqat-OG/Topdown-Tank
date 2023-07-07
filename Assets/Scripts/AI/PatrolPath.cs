using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] _patrolPoints;
    public int PatrolPointsLength { get => _patrolPoints.Length; }

    [SerializeField]
    private Color _pointColor = Color.blue;
    [SerializeField]
    private float _pointRadius = 0.1f;
    [SerializeField]
    private Color _lineColor = Color.magenta;

    public struct PathPoint
    {
        public int Index;
        public Vector2 Position;
    }

    public PathPoint GetClosestPathPoint(Vector2 tankPosition)
    {
        float minDistance = float.MaxValue;
        int index = -1;
        for (int i = 0; i < _patrolPoints.Length; i++)
        {
            float tempDistance = Vector2.Distance(tankPosition, _patrolPoints[i].position);
            if (tempDistance < minDistance)
            {
                minDistance = tempDistance;
                index = i;
            }
        }

        return new PathPoint { Index = index, Position = _patrolPoints[index].position };
    }

    public PathPoint GetNextPathPoint(int index)
    {
        index = (index + 1) % _patrolPoints.Length;
        return new PathPoint { Index = index, Position = _patrolPoints[index].position };
    }

    private void OnDrawGizmos()
    {
        if (_patrolPoints == null)
            return;

        for (int i = 0; i < _patrolPoints.Length; i++)
        {
            Helpers.DrawFilledDisk(_patrolPoints[i].position, _pointRadius, _pointColor);

            if (i >= _patrolPoints.Length - 1)
                break;

            Helpers.DrawArrowFromPoints(_patrolPoints[i].position, _patrolPoints[i + 1].position, color: _lineColor);
        }

        if (_patrolPoints.Length >= 2)
        {
            Helpers.DrawArrowFromPoints(_patrolPoints[_patrolPoints.Length - 1].position, _patrolPoints[0].position, color: _lineColor);
        }
    }
}
