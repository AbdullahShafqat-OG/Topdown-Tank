using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField]
    private float _fov = 5;
    [SerializeField]
    private float _detectionCheckDelay = 0.1f;
    [SerializeField]
    private LayerMask _playerLayerMask;
    [SerializeField]
    private LayerMask _visibilityLayer;

    private Transform _target = null;

    public bool TargetVisible { get; private set; }
    public Transform Target
    {
        get { return _target; }
        set
        {
            _target = value;
            TargetVisible = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        if (Target != null)
            TargetVisible = CheckTargetVisisble();
    }

    private bool CheckTargetVisisble()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Target.position - transform.position, _fov, _visibilityLayer);
        if (hit2D.collider != null)
        {
            return (_playerLayerMask & (1 << hit2D.collider.gameObject.layer)) != 0;
        }
        return false;
    }

    private void CheckIfTargetInFOV()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _fov, _playerLayerMask);
        if (collider != null)
            Target = collider.transform;
    }

    private void CheckIfTargetOutFOV()
    {
        if (!Target.gameObject.activeSelf || Vector2.Distance(transform.position, Target.position) > _fov + 0.5f)
            Target = null;
    }

    private void DetectTarget()
    {
        if (Target == null)
            CheckIfTargetInFOV();
        else if (Target != null)
            CheckIfTargetOutFOV();
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(_detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());
    }

    private void OnDrawGizmosSelected()
    {
        Helpers.DrawWireDisk(transform.position, _fov, Color.red);
    }
}
