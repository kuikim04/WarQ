using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataCenter : MonoBehaviour
{
    public static DataCenter instance;

    [SerializeField] private PlayerScriptableData data;
    [SerializeField] private CharacterStat characterData;

    private void Awake()
    {
        if (DataCenter.instance)
        {
            Destroy(DataCenter.instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    public PlayerScriptableData GetPlayerData()
    {
        return data;
    }

    public int GetPlayerCoinData()
    {
        return data.Coin;
    }

    public int GetPlayerDiamondData()
    {
        return data.Diamond;
    }

    public CharacterStat GetCharacterStatData()
    {
        return characterData;
    }

    #region Name

    public void SaveName()
    {
        PlayerPrefs.SetString(Key.KEY_PLAYERNAME, data.namePlayer);
        PlayerPrefs.Save();
    }
    public void LoadName()
    {
        GetPlayerData().namePlayer = PlayerPrefs.GetString(Key.KEY_PLAYERNAME);
    }

    #endregion

    #region Currency

    public void IncreaseCoin(int amount)
    {
        GetPlayerData().Coin += amount;
        SaveCurrency();
    }
    public void DecreaseCoin(int amount)
    {
        GetPlayerData().Coin -= amount;
        SaveCurrency();
    }
    public void IncreaseDiamond(int amount)
    {
        GetPlayerData().Diamond += amount;
        SaveCurrency();
    }
    public void DecreaseDiamond(int amount)
    {
        GetPlayerData().Diamond -= amount;
        SaveCurrency();
    }
    public void SaveCurrency()
    {
        PlayerPrefs.SetInt(Key.KEY_COIN, data.Coin);
        PlayerPrefs.SetInt(Key.KEY_DIAMOND, data.Diamond);
        PlayerPrefs.Save();
    }

    public void LoadCurrency()
    {
        GetPlayerData().Coin = PlayerPrefs.GetInt(Key.KEY_COIN);
        GetPlayerData().Diamond = PlayerPrefs.GetInt(Key.KEY_DIAMOND);
    }

    #endregion
}
