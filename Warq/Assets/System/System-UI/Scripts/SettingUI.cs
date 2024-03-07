using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private GameObject noticeDeleteAccount;
    [SerializeField] private Button  deleteAccountBtn;

    private void OnEnable()
    {
        noticeDeleteAccount.SetActive(false);
        deleteAccountBtn.onClick.RemoveAllListeners();
    }
    private void OnDisable()
    {
        noticeDeleteAccount.SetActive(false);
        deleteAccountBtn.onClick.RemoveAllListeners();
    }

    void Update()
    {

    }
    public void DeleteAccountButton()
    {
        noticeDeleteAccount.SetActive(true);
        deleteAccountBtn.onClick.AddListener(DeleteAccount);
    }

    public void DeleteAccount()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
