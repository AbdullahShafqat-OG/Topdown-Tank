using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatic : MonoBehaviour
{
    [SerializeField]
    private AIBehaviour _shootBehavior, _patrolBehavior;

    private TankController _tank;
    private AIDetector _detector;

    private void Awake()
    {
        _tank = GetComponentInChildren<TankController>();
        _detector = GetComponentInChildren<AIDetector>();
    }

    private void Update()
    {
        if (_detector.TargetVisible)
            _shootBehavior.PerformAction(_tank, _detector);
        else
            _patrolBehavior.PerformAction(_tank, _detector);
    }
}
