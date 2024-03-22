using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundUI : MonoBehaviour
{
    #region Use Volume Number

    [Header("ValueNumber")]
    public TextMeshProUGUI textAudioVolume;
    public TextMeshProUGUI textMusicVolume;
    public TextMeshProUGUI textEfxVolume;

    private float volumeAudio = 10;
    private float volumeMusic = 10;
    private float volumeEfx = 10;

    #endregion

    void Start()
    {
        LoadVolumeSettingsNumber();
    }

    private void LoadVolumeSettingsNumber()
    {
        LoadAudioVolumeNumber();
        LoadMusicVolumeNumber();
        LoadEfxVolumeNumber();
    }

    private void LoadAudioVolumeNumber()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeAudio = Mathf.Round(savedVolume * 10);

        SetVolumeNumber("MasterVolume", volumeAudio, textAudioVolume);
    }
    private void LoadMusicVolumeNumber()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volumeMusic = Mathf.Round(savedVolume * 10);

        SetVolumeNumber("MusicVolume", volumeMusic, textMusicVolume);
    }
    private void LoadEfxVolumeNumber()
    {
        float savedVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        volumeEfx = Mathf.Round(savedVolume * 10);
        SetVolumeNumber("SFXVolume", volumeEfx, textEfxVolume);
    }

    private void SetVolumeNumber(string key, float volume, TMP_Text textVolume)
    {
        if (textVolume != null)
        {
            float normalizedVolume = volume / 10f;

            if (volume == 0f)
            {
                normalizedVolume = 0.0001f;
            }

            SoundManager.Instance.SetVolume(key, key, null, normalizedVolume);
            UpdateTextVolume(textVolume, volume);

        }
    }
    private void UpdateTextVolume(TMP_Text textVolume, float volume)
    {
        textVolume.text = volume.ToString("F0");
    }

    public void AdjustAudioVolumeNumber(float delta)
    {
        SoundManager.Instance.OnClickSound();

        int currentVolume = Mathf.RoundToInt(volumeAudio);
        currentVolume = Mathf.Clamp(currentVolume + Mathf.RoundToInt(delta), 0, 10);
        volumeAudio = currentVolume;
        SetVolumeNumber("MasterVolume", volumeAudio, textAudioVolume);
        LoadVolumeSettingsNumber();
    }
    public void AdjustEfxVolumeNumber(float delta)
    {
        SoundManager.Instance.OnClickSound();

        int currentVolume = Mathf.RoundToInt(volumeEfx);
        currentVolume = Mathf.Clamp(currentVolume + Mathf.RoundToInt(delta), 0, 10);
        volumeEfx = currentVolume;
        SetVolumeNumber("SFXVolume", volumeEfx, textEfxVolume);
    }

    public void AdjustMusicVolumeNumber(float delta)
    {
        SoundManager.Instance.OnClickSound();

        int currentVolume = Mathf.RoundToInt(volumeMusic);
        currentVolume = Mathf.Clamp(currentVolume + Mathf.RoundToInt(delta), 0, 10);
        volumeMusic = currentVolume;
        SetVolumeNumber("MusicVolume", volumeMusic, textMusicVolume);
    }


}
