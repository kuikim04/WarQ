using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private PoolGachaManager gacha;

    [Range(0.0f, 1f)]
    public float EpicRate = 0.057f;

    [Range(0.0f, 1f)]
    public float LegendaryRate = 0.006f;

    private int totalDrawsChar = 0;
    private int totalDrawsPet = 0;


    [Header("PAGE LIST GACHA")]
    [SerializeField] private GameObject charNormalPanel;
    [SerializeField] private GameObject charRate1Panel;
    [SerializeField] private GameObject charRate2Panel;
    [SerializeField] private GameObject charRate3Panel;

    [SerializeField] private GameObject petNormalPanel;
    [SerializeField] private GameObject petRate1Panel;
    [SerializeField] private GameObject petRate2Panel;


    [SerializeField] private GameObject noticeCoinPanel;
    [SerializeField] private GameObject noticeDiamondPanel;
    private void Start()
    {
        totalDrawsChar = PlayerPrefs.GetInt(Key.KEY_DRAWCHAR, 0);
        totalDrawsPet = PlayerPrefs.GetInt(Key.KEY_DRAWPET, 0);
    }
    private void OnEnable()
    {
        noticeCoinPanel.SetActive(false);
        noticeDiamondPanel.SetActive(false);
    }
    private void SaveTotalDraws()
    {
        PlayerPrefs.SetInt(Key.KEY_DRAWCHAR, totalDrawsChar);
        PlayerPrefs.SetInt(Key.KEY_DRAWPET, totalDrawsPet);
        PlayerPrefs.Save();
    }

    #region x1
    public void NoticeGacha()
    {
        foreach (GameObject obj in gacha.characterShow)
        {
            obj.SetActive(false);
        }

        if (gacha.charNormalPanel.activeInHierarchy || gacha.petNormalPanel.activeInHierarchy)
        {
            noticeCoinPanel.SetActive(true);
        }
        else if (!gacha.charNormalPanel.activeInHierarchy || !gacha.petNormalPanel.activeInHierarchy)
        {
            noticeDiamondPanel.SetActive(true);
        }
    }
    public void CloseNotice()
    {
        foreach (GameObject obj in gacha.characterShow)
        {
            obj.SetActive(true);
        }

        noticeCoinPanel.SetActive(false);
        noticeDiamondPanel.SetActive(false);

    }
    public void DrawGachaUntilCondition()
    {
        if(gacha.charNormalPanel.activeInHierarchy || gacha.petNormalPanel.activeInHierarchy)
        {
            if (DataCenter.instance.GetPlayerCoinData() < 160000)
            {
                SoundManager.Instance.OnNoMoneySound();
                return;
            }

            SoundManager.Instance.OnSpoilGachaSound();
            DataCenter.instance.DecreaseCoin(160000);
        }
        else if(!gacha.charNormalPanel.activeInHierarchy || !gacha.petNormalPanel.activeInHierarchy)
        {
            if (DataCenter.instance.GetPlayerDiamondData() < 160)
            {
                SoundManager.Instance.OnNoMoneySound();
                return;
            }

            SoundManager.Instance.OnSpoilGachaSound();
            DataCenter.instance.DecreaseDiamond(160);
        }

        noticeCoinPanel.SetActive(false);
        noticeDiamondPanel.SetActive(false);

        if (charNormalPanel.activeInHierarchy)
            HandleResultDrawChar();

        else if (charRate1Panel.activeInHierarchy
            || charRate2Panel.activeInHierarchy || charRate3Panel.activeInHierarchy)
            HandleResultDrawCharRateup();

        else if(petNormalPanel.activeInHierarchy)
            HandleResultDrawPet();

        else if(petRate1Panel.activeInHierarchy
            || petRate2Panel.activeInHierarchy)
            HandleResultDrawPetRateup();
    }

    private void HandleResultDrawChar()
    {
        float randomValue = Random.Range(0.0f, 1.0f);

        if (randomValue <= LegendaryRate)
        {
            LegendaryCase();
        }
        else if (randomValue <= EpicRate + LegendaryRate)
        {
            EpicCase();
        }
        else
        {
            NoEpicOrLegendaryCase();
        }
        SaveTotalDraws();
    }
    private void HandleResultDrawPet()
    {
        float randomValue = Random.Range(0.0f, 1.0f);

        if (randomValue <= LegendaryRate)
        {
            LegendaryCase();
        }
        else if (randomValue <= EpicRate + LegendaryRate)
        {
            EpicCase();
        }
        else
        {
            NoEpicOrLegendaryCase();
        }

        SaveTotalDraws();
    }
    private void HandleResultDrawCharRateup()
    {
        totalDrawsChar++;

        float randomValue = Random.Range(0.0f, 1.0f);

        if (totalDrawsChar == 30)
        {
            LegendaryCase();
            totalDrawsChar = 0;
        }
        else if (totalDrawsChar % 10 == 0)
        {
            if (randomValue <= LegendaryRate)
            {
                LegendaryCase();
                totalDrawsChar = 0;
            }
            else
            {
                EpicCase();
            }
        }
        else
        {
            if (randomValue <= LegendaryRate)
            {
                LegendaryCase();
                totalDrawsChar = 0;
            }
            else if (randomValue <= EpicRate + LegendaryRate)
            {
                EpicCase();
            }
            else
            {
                NoEpicOrLegendaryCase();
            }
        }
        SaveTotalDraws();
        Debug.Log($"Results after {totalDrawsChar} draws");
    }
    private void HandleResultDrawPetRateup()
    {
        totalDrawsPet++;

        float randomValue = Random.Range(0.0f, 1.0f);

        if (totalDrawsPet == 30)
        {
            LegendaryCase();
            totalDrawsPet = 0;
        }
        else if (totalDrawsPet % 10 == 0)
        {
            if (randomValue <= LegendaryRate)
            {
                LegendaryCase();
                totalDrawsPet = 0;
            }
            else
            {
                EpicCase();
            }
        }
        else
        {
            if (randomValue <= LegendaryRate)
            {
                LegendaryCase();
                totalDrawsPet = 0;
            }
            else if (randomValue <= EpicRate + LegendaryRate)
            {
                EpicCase();
            }
            else
            {
                NoEpicOrLegendaryCase();
            }
        }
        SaveTotalDraws();
        Debug.Log($"Results after {totalDrawsPet} draws");
    }


    private void LegendaryCase()
    {
        gacha.ResultReward(CharacterStat.Tier.Legend);
        Debug.Log("LEGEN");
    }

    private void EpicCase()
    {
        gacha.ResultReward(CharacterStat.Tier.Epic);
        Debug.Log("EPIC");
    }
    private void NoEpicOrLegendaryCase()
    {
        gacha.ResultReward(CharacterStat.Tier.None);
        Debug.Log("NONE");
    }
    #endregion
}
