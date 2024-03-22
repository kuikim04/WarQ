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



    [Space(8)]
    [Header("PAGE")]
    [SerializeField] private GameObject settingPage;
    [SerializeField] private GameObject homePage;
    [SerializeField] private GameObject gachaPage;

    [Space(8)]
    [Header("BUTTON")]
    [SerializeField] private Button settingButton;
    [SerializeField] private Button gachaButton;


    private void Start()
    {
        if (PlayerPrefs.HasKey(Key.KEY_PLAYERNAME) && PlayerPrefs.HasKey(Key.KEY_GETSTARTERPACK))
            Initialize();


        SoundManager.Instance.OnSceneHomeSound();
    }
    private void OnEnable()
    {
        TimeScripts.OnDayChanged += RewardCheck;

        settingButton.onClick.AddListener(OpenSettingPage);
        gachaButton.onClick.AddListener(OpenPage);

    }
    private void OnDisable()
    {
        TimeScripts.OnDayChanged -= RewardCheck;

        settingButton.onClick.RemoveAllListeners();
        gachaButton.onClick.RemoveAllListeners();
    }

    public void Initialize()
    {
        DataCenter.instance.LoadName();
        DataCenter.instance.LoadCurrency();

        nameText.text = DataCenter.instance.GetPlayerData().namePlayer;
        lvText.text = $"LV:\n {DataCenter.instance.GetPlayerData().Level}" ;

        expFill.fillAmount = DataCenter.instance.GetPlayerData().currentExp / DataCenter.instance.GetPlayerData().exp;

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

    #region Page

    public void OpenPage()
    {
        SoundManager.Instance.OnClickSound();

        bool isHomePageActive = homePage.activeSelf;

        homePage.SetActive(!isHomePageActive);
        gachaPage.SetActive(isHomePageActive);
    }
    public void OpenSettingPage()
    {
        SoundManager.Instance.OnClickSound();

        settingPage.SetActive(!settingPage.activeSelf);
    }
    public void RewardCheck()
    {
        Debug.Log("Diary Reward!!");
    }

    #endregion

}
