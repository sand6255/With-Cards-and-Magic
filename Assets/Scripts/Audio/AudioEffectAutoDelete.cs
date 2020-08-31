using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectAutoDelete : MonoBehaviour
{
    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }
    void Update()
    {
        if (!source.isPlaying)
            Destroy(gameObject);
    }
}
