using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class PlayerInitialization : MonoBehaviour
{
    void Start()
    {
        InitializeStat();
    }

    private void InitializeStat()
    {
        if (DataCenter.instance.GetCharacterStatData() != null)
        {
            if (DataCenter.instance.GetCharacterStatData().ObjectType == CharacterStat.Type.Player)
            {
                DataCenter.instance.GetCharacterStatData().NamePlayer = DataCenter.instance.GetPlayerData().namePlayer;
                DataCenter.instance.GetCharacterStatData().level = DataCenter.instance.GetPlayerData().Level;

                DataCenter.instance.GetCharacterStatData().CalculateExpForLevel
                    (DataCenter.instance.GetCharacterStatData().level);

                DataCenter.instance.GetPlayerData().exp = DataCenter.instance.GetCharacterStatData().Exp;



            }

            Debug.Log($"Type: {DataCenter.instance.GetCharacterStatData().ObjectType}, " + 
                $" Name Player: {DataCenter.instance.GetCharacterStatData().NamePlayer}" +
                $"Level: {DataCenter.instance.GetCharacterStatData().level}," + 
                $" Exp: {DataCenter.instance.GetCharacterStatData().Exp}");
        }
        else
        {
            Debug.LogError("CharacterCenterData is null.");
        }
    }
}
