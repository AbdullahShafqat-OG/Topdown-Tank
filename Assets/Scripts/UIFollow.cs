using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _objectToFollow;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_objectToFollow != null)
            _rectTransform.anchoredPosition = _objectToFollow.localPosition;
    }
}
