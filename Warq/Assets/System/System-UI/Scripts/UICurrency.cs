using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICurrency : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] coinText;
    [SerializeField] private TextMeshProUGUI[] diamondText;

    private void Update()
    {
        UpdateCoinText(DataCenter.instance.GetPlayerData().Coin);
        UpdateDiamondText(DataCenter.instance.GetPlayerData().Diamond);
    }
    private void UpdateCoinText(int amount)
    {
        foreach (var coin in coinText)
        {
            coin.text = FormatNumber(amount);
        }
    }

    private void UpdateDiamondText(int amount)
    {
        foreach (var diamond in diamondText)
        {
            diamond.text = FormatNumber(amount);
        }
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
