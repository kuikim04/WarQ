using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolCharacter", menuName = "Data/PoolCharacter")]
public class PoolCharacter : ScriptableObject
{
    public CharacterStat[] Characters;
}
