using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CHA0-Stat" , menuName = "Data/CharacterStat")]
public class CharacterStat : ScriptableObject
{
    public string NamePlayer;
    public string IDCharacter;
    public int Cost;
    public float CoolDownTime;

    [TextArea(3, 10)] public string DetailCharacter1;
    [TextArea(3, 10)] public string DetailCharacter2;

    public Sprite ImageCharacter;

    public GameObject CharacterPrefab;
    public GameObject CharacterProfile;
    public GameObject GachaCharacter;

    public int level = 1;
    public int Maxlevel = 80;

    public float Exp;
    public float CurrentExp;

    public enum Type
    {
        Player,
        Character,
        Pet
    }
    public enum Element
    {
        None,
        Fire,
        Water,
        Earth,
        Air
    }
    public enum Career
    {
        None,
        Tank,
        Mage,
        Support,
        Heal,
        DPS,
        Assassin
    }
    public enum Tier
    {
        None,
        Epic,
        Legend
    }

    public Type ObjectType;
    public Element CharacterElement;
    public Career CareerCharacter;
    public Tier CharacterTier;

    #region Character Status

    public float MaxHealthPoints;

    public float AttackPoints;
    public float DefensePoints;
    public float AttackResistance;

    public float SpeedWalk;
    public float SpeedAttack;

    public float MaxEnergyUniqSkill;
    public float EnergyRegenRate;

    public float EffectHitRate;
    public float EffectResistance;

    public float CriticalRate;
    public float CriticalDamage;
    public float CriticalResistance;

    #endregion

    #region PetStatus

    public float BonusMaxHealthPoints;

    public float BonusAttackPoints;
    public float BonusDefensePoints;
    public float BonusAttackResistance;

    public float BonusSpeedWalk;
    public float BonusSpeedAttack;

    public float BonusMaxEnergyUniqSkill;
    public float BonusEnergyRegenRate;

    public float BonusEffectHitRate;
    public float BonusEffectResistance;

    public float BonusCriticalRate;
    public float BonusCriticalDamage;
    public float BonusCriticalResistance;

    #endregion

    public float CalculateExpForLevel(int level)
    {
        float a = 100.0f;
        float b = 1.5f;

        Exp = Mathf.CeilToInt(a * Mathf.Pow(level, b) / 5) * 5;

        switch (CharacterTier)
        {
            case Tier.Epic:
                Exp *= 1.2f;
                break;
            case Tier.Legend:
                Exp *= 1.5f;
                break;
        }

        return Exp;
    }

    public void ResetValues()
    {
        level = 1;
        Maxlevel = 80;

        CurrentExp = 0f;
    }
}
