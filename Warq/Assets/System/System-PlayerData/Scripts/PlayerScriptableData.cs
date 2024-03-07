using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data", order = 1)]
public class PlayerScriptableData : ScriptableObject
{
    public string namePlayer;

    public int Coin;
    public int Diamond;

    public int Level = 1;
    public float exp;
    public float currentExp;
}
