using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private GameObject noticeDeleteAccount;
    [SerializeField] private Button  deleteAccountBtn;
    [SerializeField] private Button openDeleteAccountBtn;

    private void OnEnable()
    {
        noticeDeleteAccount.SetActive(false);
        deleteAccountBtn.onClick.RemoveAllListeners();
        openDeleteAccountBtn.onClick.AddListener(DeleteAccountButton);
    }
    private void OnDisable()
    {
        noticeDeleteAccount.SetActive(false);
        deleteAccountBtn.onClick.RemoveAllListeners();
        openDeleteAccountBtn.onClick.RemoveAllListeners();

    }

    public void DeleteAccountButton()
    {
        noticeDeleteAccount.SetActive(true);
        deleteAccountBtn.onClick.AddListener(DeleteAccount);
    }

    public void DeleteAccount()
    {
        DataCenter.instance.GetPlayerData().ResetValues();
        DataCenter.instance.GetCharacterStatData().ResetValues();

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
