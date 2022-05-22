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

    private AudioSource audioSource;

    private void Awake()
    {
        ManageSingleton();
    }

    private void Start()
    {
        Reset();
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
    }

    private void ManageSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
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
