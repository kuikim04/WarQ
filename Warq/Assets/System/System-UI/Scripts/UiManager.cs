using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI lvText;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private Image expFill;

    [SerializeField] private GameObject joyStick;
    private void Start()
    {
        if (PlayerPrefs.HasKey(Key.KEY_PLAYERNAME) && PlayerPrefs.HasKey(Key.KEY_GETSTARTERPACK))
            Initialize();
    }
    private void OnEnable()
    {
        TimeScripts.OnDayChanged += RewardCheck;
    }
    private void OnDisable()
    {
        TimeScripts.OnDayChanged -= RewardCheck;
    }

    public void Initialize()
    {
        DataCenter.instance.LoadName();
        DataCenter.instance.LoadCurrency();

        nameText.text = DataCenter.instance.GetPlayerData().namePlayer;
        lvText.text = DataCenter.instance.GetPlayerData().Level.ToString();

        expFill.fillAmount = DataCenter.instance.GetPlayerData().currentExp / DataCenter.instance.MaxExp;

#if UNITY_EDITOR
        joyStick.SetActive(true);
#else
    joyStick.SetActive(Application.isMobilePlatform);
#endif

    }

    private void Update()
    {
        if (PlayerPrefs.HasKey(Key.KEY_PLAYERNAME) && PlayerPrefs.HasKey(Key.KEY_GETSTARTERPACK))
            Initialize();

        if (TimeScripts.Instance.HasDayChanged())
        {
            Debug.Log("Day has changed!");
            TimeScripts.OnDayChanged?.Invoke();
        }

        if (TimeScripts.Instance.IsTimeLodaed)
        {
            DateTime currentDateTime = TimeScripts.Instance.GetCurrentDateTime();
            timeText.text = $"Version: {Application.version} \n{currentDateTime}"; ;
        }
    }

    public void RewardCheck()
    {
        Debug.Log("Diary Reward!!");
    }
}
