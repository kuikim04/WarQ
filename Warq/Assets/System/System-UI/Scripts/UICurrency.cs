using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICurrency : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI diamondText;

    private void Update()
    {
        UpdateCoinText(DataCenter.instance.GetPlayerData().Coin);
        UpdateDiamondText(DataCenter.instance.GetPlayerData().Diamond);
    }
    private void UpdateCoinText(int amount)
    {
        coinText.text = FormatNumber(amount);
    }

    private void UpdateDiamondText(int amount)
    {
        diamondText.text = FormatNumber(amount);
    }

    private string FormatNumber(int number)
    {
        if (number >= 1000)
        {
            float formattedNumber = number / 1000.0f;
            return formattedNumber.ToString("0.0") + "k";
        }
        else
        {
            return number.ToString();
        }
    }
}
