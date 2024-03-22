using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SpawnerButton[] spawnButton;
    public List<string> IdSpawnList;

    public int LvMana;
    public int Mana; 
    private int MaxMana;
    public int ManaIncreaseRateCost;
    public float ManaIncreaseRateTime;

    [SerializeField] private TextMeshProUGUI textMana;
    [SerializeField] private TextMeshProUGUI textLvMana;
    [SerializeField] private TextMeshProUGUI textUpgradeMana;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate GameManager instance found, destroying the new one.");
            Destroy(gameObject);
        }


        IdSpawnList = TeamManager.Instance.IDTeam;
        foreach (SpawnerButton button in spawnButton)
        {
            button.Init(IdSpawnList[Array.IndexOf(spawnButton, button)]);
        }

    }
    public static GameManager GetInstance()
    {
        if (Instance == null)
        {
            Debug.LogError("No GameManager instance found in the scene.");
        }
        return Instance;
    }

    private void Start()
    {
        LvMana = 1;
        CheckMaxMana();
        StartCoroutine(IncreaseMana());

        textMana.text = $"{Mana} / {MaxMana}";
        textLvMana.text = $"LV: {LvMana}";
        textUpgradeMana.text = UpgradeManaCost().ToString();
    }

    private void Update()
    {
        if(Mana >= MaxMana)
            Mana = MaxMana;
    }

    private void CheckMaxMana()
    {
        MaxMana = LvMana switch
        {
            1 => 50,
            2 => 70,
            3 => 90,
            4 => 110,
            5 => 130,
            6 => 150,
            7 => 170,
            8 => 190,
            9 => 210,
            10 => 230,
            _ => 50,
        };
    }

    private int UpgradeManaCost()
    {
        return MaxMana / 2;
    }

    public void UpgradeLv()
    {
        int upgradeCost = UpgradeManaCost();

        if (Mana < upgradeCost)
            return;

        Mana -= upgradeCost;
        LvMana += ManaIncreaseRateCost;

        CheckMaxMana();

        textLvMana.text = $"LV: {LvMana}";
        textUpgradeMana.text = UpgradeManaCost().ToString();
    }

    private IEnumerator IncreaseMana()
    {
        while (true && Mana < MaxMana)
        {
            yield return new WaitForSeconds(ManaIncreaseRateTime);
            Mana += Mathf.RoundToInt(1);
            textMana.text = $"{Mana} / {MaxMana}";
        }
    }
}
