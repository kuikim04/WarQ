using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager soundInstance;
    public static SoundManager Instance { get { return soundInstance; } }

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource mainAudio;
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioSource efxAudio;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);

        else
        {
            soundInstance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadAllSoundSetting();
        }
        else
        {
            float initialVolume = 0.5f;

            audioMixer.SetFloat("MasterVolume", Mathf.Log10(initialVolume) * 20);
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(initialVolume) * 20);
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(initialVolume) * 20);

            mainAudio.volume = initialVolume;
            musicAudio.volume = initialVolume;
            efxAudio.volume = initialVolume;

            PlayerPrefs.SetFloat("MasterVolume", initialVolume);
            PlayerPrefs.SetFloat("MusicVolume", initialVolume);
            PlayerPrefs.SetFloat("SFXVolume", initialVolume);
            PlayerPrefs.Save();
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
}
