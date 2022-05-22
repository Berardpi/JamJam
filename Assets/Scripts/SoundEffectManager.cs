using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField]
    AudioClip blinkClip;

    [SerializeField]
    float blinkVolume = 0.5f;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBlinkEffect()
    {
        // Vector3 cameraPos = Camera.main.transform.position;
        audioSource.PlayOneShot(blinkClip, blinkVolume);
    }
}
