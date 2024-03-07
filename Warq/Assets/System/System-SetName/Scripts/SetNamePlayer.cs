using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetNamePlayer : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;

    [SerializeField] private GameObject setNamePanel;
    [SerializeField] private GameObject rewardPanel;

    [SerializeField] private GameObject[] inFoPanel;
    [SerializeField] private GameObject noticeObj;

    [SerializeField] private Button enterNameBtn;
    [SerializeField] private Button clamBtn;

    private string nameSet;

    private void Start()
    {
        if (PlayerPrefs.HasKey(Key.KEY_PLAYERNAME))
        {
            gameObject.SetActive(false);
            return;
        }

        setNamePanel.SetActive(true);
        rewardPanel.SetActive(false);

        clamBtn.onClick.AddListener(ClamReward);
        enterNameBtn.onClick.AddListener(SetNameButton);

        foreach (GameObject go in inFoPanel)
        {
            go.SetActive(false);
        }

    }
    public void SetNameButton()
    {
        DataCenter.instance.GetPlayerData().namePlayer = nameSet;
        DataCenter.instance.SaveName();

        setNamePanel.SetActive(false);
        rewardPanel.SetActive(true);
    }

    public void SetNameValue(string value)
    {
        nameSet = value;
    }  

    private void ClamReward()
    {
        DataCenter.instance.IncreaseCoin(100);
        DataCenter.instance.IncreaseDiamond(100);

        PlayerPrefs.SetString(Key.KEY_GETSTARTERPACK,"");
        foreach (GameObject go in inFoPanel)
        {
            go.SetActive(true);
        }

        uiManager.Initialize();
        noticeObj.SetActive(true);
        gameObject.SetActive(false);


    }
}
