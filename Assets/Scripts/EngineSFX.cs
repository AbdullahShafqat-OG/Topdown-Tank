using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSFX : MonoBehaviour
{
    [SerializeField]
    private float _minVolume = 0.05f, _maxVolume = 0.1f;
    private float _currentVolume;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentVolume = _minVolume;
    }

    private void Start()
    {
        _audioSource.volume = _currentVolume;
    }
    
    public void ControlEngineVolume(float speed)
    {
        _currentVolume = speed * (_maxVolume - _minVolume) + _minVolume;
        _audioSource.volume = _currentVolume;
    }
}
