using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerScriptableData : ScriptableObject
{
    public string namePlayer;

    public int Coin;
    public int Diamond;

    public int Level = 1;
    public float exp;
    public float currentExp; 
    
    
    public void ResetValues()
    {
        namePlayer = "";
        Coin = 0;
        Diamond = 0;
        Level = 1;
        exp = 0f;
        currentExp = 0f;
    }
}
