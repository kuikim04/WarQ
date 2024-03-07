using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager soundInstance;
    public static SoundManager Instance { get { return soundInstance; } }

    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio Source")]
    [SerializeField] private AudioSource mainAudio;
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioSource efxAudio;

    [Header("Audio Clip")]

    [Header("Prefab")]
    public GameObject SoundPrefab;
    List<GameObject> soundEffectPool = new();

    private Transform contents;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);

        else
        {
            soundInstance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (!contents)
        {
            contents = new GameObject("Contents").transform;
            contents.SetParent(transform);
        }
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadAllSoundSetting();
        }
        else
        {
            SetVolumeFromPrefs();

        }
    }

    private void LoadAllSoundSetting()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);

        mainAudio.volume = musicVolume;
        musicAudio.volume = musicVolume;
        efxAudio.volume = sfxVolume;
    }
    private void SetVolumeFromPrefs()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float audioVolume = PlayerPrefs.GetFloat("MasterVolume");
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");

            audioMixer.SetFloat("MasterVolume", Mathf.Log10(audioVolume) * 20);
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);

            mainAudio.volume = audioVolume;
            musicAudio.volume = musicVolume;
            efxAudio.volume = sfxVolume;
        }
    }
}
