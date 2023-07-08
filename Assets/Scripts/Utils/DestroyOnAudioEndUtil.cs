using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAudioEndUtil : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Invoke("DestroyWrapper", _audioSource.clip.length);
    }

    private void DestroyWrapper()
    {
        Destroy(gameObject);
    }
}
