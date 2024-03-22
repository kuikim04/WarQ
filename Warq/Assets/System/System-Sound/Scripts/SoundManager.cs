using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource mainAudio;
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioSource efxAudio;

    [Space(8)]

    [Header("Audio Clips")]
    [Header("UI")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip typeSound;
    [SerializeField] private AudioClip showResultSound;
    [SerializeField] private AudioClip spoilResultSound;
    [SerializeField] private AudioClip noMeoneySound;

    [Header("ACTION")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip attackSound;

    [Header("BACKGROUND")]
    [SerializeField] private AudioClip home;
    [SerializeField] private AudioClip city;
    [SerializeField] private AudioClip forest;

    [Space(8)]

    [Header("Prefab")]
    [SerializeField] private GameObject soundPrefab;
    private List<GameObject> soundEffectPool = new List<GameObject>();
    private Transform contents;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeSoundSettings();
        InitializeContents();
    }

    private void InitializeSoundSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            LoadAllSoundSetting();
        else
            SetDefaultVolume();
    }

    private void InitializeContents()
    {
        if (contents == null)
        {
            contents = new GameObject("Contents").transform;
            contents.SetParent(transform);
        }
    }
    public void PlaySoundEffect(AudioClip clip)
    {
        efxAudio.clip = clip;
        efxAudio.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicAudio.clip = clip;
        musicAudio.Play();

    }

    public void ChangeBackgroundMusic(AudioClip newClip)
    {
        musicAudio.Stop();
        musicAudio.clip = newClip;
        musicAudio.Play();
    }

    public void PlaySound(AudioClip clip, GameObject obj)
    {
        if (!obj.TryGetComponent(out AudioSource audioSource))
        {
            audioSource = obj.AddComponent<AudioSource>();
        }

        audioSource.volume = efxAudio.volume;
        audioSource.PlayOneShot(clip);
    }
    public void SetVolume(string mixerParameter, string playerPrefsKey, Slider slider, float volume)
    {
        audioMixer.SetFloat(mixerParameter, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(playerPrefsKey, volume);

        if (slider != null)
            slider.value = volume;

        switch (mixerParameter)
        {
            case "MasterVolume":
                mainAudio.volume = volume;
                break;
            case "MusicVolume":
                musicAudio.volume = volume;
                break;
            case "SFXVolume":
                efxAudio.volume = volume;
                break;
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

    private void SetDefaultVolume()
    {
        float initialVolume = 0.5f;
        string[] parameters = { "MasterVolume", "MusicVolume", "SFXVolume", "UIVolume" };

        foreach (string parameter in parameters)
        {
            audioMixer.SetFloat(parameter, Mathf.Log10(initialVolume) * 20);
            PlayerPrefs.SetFloat(parameter, initialVolume);
        }
        PlayerPrefs.Save();
    }

    #region Sound Action
    public void OnClickSound()
    {
        PlaySoundEffect(clickSound);
    }
    public void OnTypeSound()
    {
        PlaySoundEffect(typeSound);
    }
    public void OnOpenGachaSound()
    {
        PlaySoundEffect(showResultSound);
    }
    public void OnSpoilGachaSound()
    {
        PlaySoundEffect(spoilResultSound);
    }
    public void OnNoMoneySound()
    {
        PlaySoundEffect(noMeoneySound);
    }

    public void OnSceneHomeSound()
    {
        if(musicAudio.clip != home)
        {
            ChangeBackgroundMusic(home);
        }
        else
        {
            PlayMusic(home);
        }
    }

    public void OnSceneForestSound()
    {
        if (musicAudio.clip != forest)
        {
            ChangeBackgroundMusic(forest);
        }
        else
        {
            PlayMusic(forest);
        }
    }
    public void OnSceneCitySound()
    {
        if (musicAudio.clip != city)
        {
            ChangeBackgroundMusic(city);
        }
        else
        {
            PlayMusic(city);
        }
    }

    public void OnShootSound()
    {
        GameObject pooledObject = GetPooledObject();

        if (pooledObject)
        {
            AudioSource audioSource = pooledObject.GetComponent<AudioSource>();
            if (audioSource != null)
                audioSource.enabled = true;

            ActivateObject(pooledObject, shootSound);
        }
        else
        {
            soundEffectPool.Add(CreateObject(soundPrefab, shootSound));
        }
    }

    #endregion

    private void ActivateObject(GameObject obj, AudioClip clip)
    {
        obj.SetActive(true);
        SetVolumeForObject(obj);
        PlaySound(clip, obj);
        StartCoroutine(EndObject(obj, clip.length));
    }

    private void SetVolumeForObject(GameObject obj)
    {
        if (obj.TryGetComponent(out AudioSource audioSource))
        {
            audioSource.enabled = true;
            audioSource.volume = efxAudio.volume;
            audioSource.pitch = efxAudio.pitch;
            audioSource.outputAudioMixerGroup = efxAudio.outputAudioMixerGroup;
        }
    }

    private GameObject CreateObject(GameObject obj, AudioClip clip)
    {
        GameObject ef = Instantiate(obj);
        ActivateObject(ef, clip);
        return ef;
    }

    private GameObject GetPooledObject()
    {
        foreach (var obj in soundEffectPool)
        {
            if (!obj.activeInHierarchy)
            {
                SetVolumeForObject(obj);
                return obj;
            }
        }
        GameObject newObj = CreatePooledObject();
        soundEffectPool.Add(newObj);
        return newObj;
    }

    private GameObject CreatePooledObject()
    {
        GameObject newObj = Instantiate(soundPrefab, contents);
        newObj.SetActive(false);
        SetVolumeForObject(newObj);
        return newObj;
    }

    IEnumerator EndObject(GameObject obj, float delayTime = 1)
    {
        yield return new WaitForSeconds(delayTime);
        obj.SetActive(false);
    }

    public void DeactivateOrDestroySoundObjects()
    {
        foreach (var obj in soundEffectPool)
        {
            if (obj != null)
                Destroy(obj);
        }
        soundEffectPool.Clear();
    }

    public void DestroyChildrenContent()
    {
        foreach (Transform child in contents)
            Destroy(child.gameObject);
        soundEffectPool.Clear();
    }
}
