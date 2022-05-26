using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get { return instance; }
    }

    [Header("Sound effects")]
    [SerializeField]
    AudioClip blinkClip;

    [SerializeField]
    [Range(0f, 1f)]
    float blinkVolume = 0.5f;

    private AudioSource audioSource;
    private bool inMusicFadeOut = false;
    private bool inMusicFadeIn = false;
    private float startVolume;

    private void Awake()
    {
        ManageSingleton();
    }

    private void Start()
    {
        Reset();
    }

    private void FixedUpdate()
    {
        if (inMusicFadeOut && audioSource.volume >= startVolume / 10)
        {
            audioSource.volume -= 0.01f;
        }

        if (inMusicFadeIn && audioSource.volume < startVolume)
        {
            audioSource.volume = Mathf.Min(audioSource.volume + 0.01f, startVolume);
        }
    }

    public void Reset()
    {
        LevelSettings levelSettings = FindObjectOfType<LevelSettings>();
        if (levelSettings != null)
        {
            if (levelSettings.music != audioSource.clip)
            {
                audioSource.clip = levelSettings.music;
                audioSource.Play();
            }
        }
        inMusicFadeOut = false;
        inMusicFadeIn = true;
    }

    public void TriggerMusicFadeOut()
    {
        inMusicFadeIn = false;
        inMusicFadeOut = true;
    }

    public void PlayBlinkEffect()
    {
        // Vector3 cameraPos = Camera.main.transform.position;
        audioSource.PlayOneShot(blinkClip, blinkVolume);
    }

    private void ManageSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            startVolume = audioSource.volume;
        }
        else if (instance != this)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
