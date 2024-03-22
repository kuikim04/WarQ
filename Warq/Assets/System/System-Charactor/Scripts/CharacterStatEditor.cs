using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterStat))]
public class CharacterStatEditor : Editor
{
    private SerializedProperty namePlayerProp;
    private SerializedProperty IDCharacterProp;
    private SerializedProperty CostProp;
    private SerializedProperty CoolDownTimeProp;
    private SerializedProperty DetailCharacter1Prop;
    private SerializedProperty DetailCharacter2Prop;

    private SerializedProperty ImageCharacterProp;

    private SerializedProperty CharacterPrefabProp;
    private SerializedProperty CharacterProfileProp;
    private SerializedProperty GachaCharacterProp;

    private SerializedProperty levelProp;
    private SerializedProperty maxLevelProp;
    private SerializedProperty expProp;
    private SerializedProperty currentExpProp;

    private SerializedProperty objectTypeProp;
    private SerializedProperty characterElementProp;
    private SerializedProperty careerProp;
    private SerializedProperty characterTierProp;

    // Character Status
    private SerializedProperty maxHealthPointsProp;

    private SerializedProperty attackPointsProp;
    private SerializedProperty defensePointsProp;
    private SerializedProperty attackResistanceProp;

    private SerializedProperty speedWalkProp;
    private SerializedProperty speedAttackProp;

    private SerializedProperty maxEnergyUniqSkillProp;
    private SerializedProperty energyRegenRateProp;

    private SerializedProperty effectHitRateProp;
    private SerializedProperty effectResistanceProp;

    private SerializedProperty criticalRateProp;
    private SerializedProperty criticalDamageProp;
    private SerializedProperty criticalResistanceProp;

    // Pet Status
    private SerializedProperty bonusMaxHealthPointsProp;

    private SerializedProperty bonusAttackPointsProp;
    private SerializedProperty bonusDefensePointsProp;
    private SerializedProperty bonusAttackResistanceProp;

    private SerializedProperty bonusSpeedWalkProp;
    private SerializedProperty bonusSpeedAttackProp;

    private SerializedProperty bonusMaxEnergyUniqSkillProp;
    private SerializedProperty bonusEnergyRegenRateProp;

    private SerializedProperty bonusEffectHitRateProp;
    private SerializedProperty bonusEffectResistanceProp;

    private SerializedProperty bonusCriticalRateProp;
    private SerializedProperty bonusCriticalDamageProp;
    private SerializedProperty bonusCriticalResistanceProp;

    private void OnEnable()
    {
        namePlayerProp = serializedObject.FindProperty("NamePlayer");
        IDCharacterProp = serializedObject.FindProperty("IDCharacter");

        CostProp = serializedObject.FindProperty("Cost");
        CoolDownTimeProp = serializedObject.FindProperty("CoolDownTime");

        DetailCharacter1Prop = serializedObject.FindProperty("DetailCharacter1");
        DetailCharacter2Prop = serializedObject.FindProperty("DetailCharacter2");

        ImageCharacterProp = serializedObject.FindProperty("ImageCharacter");

        CharacterPrefabProp = serializedObject.FindProperty("CharacterPrefab");
        CharacterProfileProp = serializedObject.FindProperty("CharacterProfile");
        GachaCharacterProp = serializedObject.FindProperty("GachaCharacter");

        levelProp = serializedObject.FindProperty("level");
        maxLevelProp = serializedObject.FindProperty("Maxlevel");
        expProp = serializedObject.FindProperty("Exp");
        currentExpProp = serializedObject.FindProperty("CurrentExp");

        objectTypeProp = serializedObject.FindProperty("ObjectType");
        characterElementProp = serializedObject.FindProperty("CharacterElement");
        careerProp = serializedObject.FindProperty("CareerCharacter");
        characterTierProp = serializedObject.FindProperty("CharacterTier");

        //Character
        maxHealthPointsProp = serializedObject.FindProperty("MaxHealthPoints");

        attackPointsProp = serializedObject.FindProperty("AttackPoints");
        defensePointsProp = serializedObject.FindProperty("DefensePoints");
        attackResistanceProp = serializedObject.FindProperty("AttackResistance");

        speedWalkProp = serializedObject.FindProperty("SpeedWalk");
        speedAttackProp = serializedObject.FindProperty("SpeedAttack");

        maxEnergyUniqSkillProp = serializedObject.FindProperty("MaxEnergyUniqSkill");
        energyRegenRateProp = serializedObject.FindProperty("EnergyRegenRate");

        effectHitRateProp = serializedObject.FindProperty("EffectHitRate");
        effectResistanceProp = serializedObject.FindProperty("EffectResistance");

        criticalRateProp = serializedObject.FindProperty("CriticalRate");
        criticalDamageProp = serializedObject.FindProperty("CriticalDamage");
        criticalResistanceProp = serializedObject.FindProperty("CriticalResistance");


        //PET
        bonusMaxHealthPointsProp = serializedObject.FindProperty("BonusMaxHealthPoints");

        bonusAttackPointsProp = serializedObject.FindProperty("BonusAttackPoints");
        bonusDefensePointsProp = serializedObject.FindProperty("BonusDefensePoints");
        bonusAttackResistanceProp = serializedObject.FindProperty("BonusAttackResistance");

        bonusSpeedWalkProp = serializedObject.FindProperty("BonusSpeedWalk");
        bonusSpeedAttackProp = serializedObject.FindProperty("BonusSpeedAttack");

        bonusMaxEnergyUniqSkillProp = serializedObject.FindProperty("BonusMaxEnergyUniqSkill");
        bonusEnergyRegenRateProp = serializedObject.FindProperty("BonusEnergyRegenRate");

        bonusEffectHitRateProp = serializedObject.FindProperty("BonusEffectHitRate");
        bonusEffectResistanceProp = serializedObject.FindProperty("BonusEffectResistance");

        bonusCriticalRateProp = serializedObject.FindProperty("BonusCriticalRate");
        bonusCriticalDamageProp = serializedObject.FindProperty("BonusCriticalDamage");
        bonusCriticalResistanceProp = serializedObject.FindProperty("BonusCriticalResistance");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(namePlayerProp);
        EditorGUILayout.PropertyField(IDCharacterProp);
        EditorGUILayout.PropertyField(CoolDownTimeProp);
        EditorGUILayout.PropertyField(DetailCharacter1Prop);
        EditorGUILayout.PropertyField(DetailCharacter2Prop);

        EditorGUILayout.PropertyField(ImageCharacterProp);
        EditorGUILayout.PropertyField(GachaCharacterProp);

        EditorGUILayout.PropertyField(levelProp);
        EditorGUILayout.PropertyField(maxLevelProp);
        EditorGUILayout.PropertyField(expProp);
        EditorGUILayout.PropertyField(currentExpProp);

        EditorGUILayout.PropertyField(objectTypeProp);
        EditorGUILayout.PropertyField(characterTierProp);

        if (((CharacterStat.Type)objectTypeProp.enumValueIndex) == CharacterStat.Type.Player)
        {
            DisplayPlayerStatus();
        }
        if (((CharacterStat.Type)objectTypeProp.enumValueIndex) == CharacterStat.Type.Character)
        {
            DisplayCharacterStatus();
        }
        else if (((CharacterStat.Type)objectTypeProp.enumValueIndex) == CharacterStat.Type.Pet)
        {
            DisplayPetStatus();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayPlayerStatus()
    {
        GUILayout.Space(8);
        EditorGUILayout.LabelField("Player Status", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(maxHealthPointsProp);

        EditorGUILayout.PropertyField(attackPointsProp);
        EditorGUILayout.PropertyField(defensePointsProp);
    }

    private void DisplayCharacterStatus()
    {
        EditorGUILayout.PropertyField(CostProp);

        EditorGUILayout.PropertyField(characterElementProp);
        EditorGUILayout.PropertyField(careerProp);

        GUILayout.Space(8);
        EditorGUILayout.PropertyField(CharacterProfileProp);
        EditorGUILayout.PropertyField(CharacterPrefabProp);

        GUILayout.Space(8);  
        EditorGUILayout.LabelField("Character Status", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(maxHealthPointsProp);

        EditorGUILayout.PropertyField(attackPointsProp);
        EditorGUILayout.PropertyField(defensePointsProp);
        EditorGUILayout.PropertyField(attackResistanceProp);

        EditorGUILayout.PropertyField(speedWalkProp);
        EditorGUILayout.PropertyField(speedAttackProp);

        EditorGUILayout.PropertyField(maxEnergyUniqSkillProp);
        EditorGUILayout.PropertyField(energyRegenRateProp);

        EditorGUILayout.PropertyField(effectHitRateProp);
        EditorGUILayout.PropertyField(effectResistanceProp);

        EditorGUILayout.PropertyField(criticalRateProp);
        EditorGUILayout.PropertyField(criticalDamageProp);
        EditorGUILayout.PropertyField(criticalResistanceProp);
    }

    private void DisplayPetStatus()
    {
        GUILayout.Space(8);  
        EditorGUILayout.LabelField("Pet Status", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(bonusMaxHealthPointsProp);

        EditorGUILayout.PropertyField(bonusAttackPointsProp);
        EditorGUILayout.PropertyField(bonusDefensePointsProp);
        EditorGUILayout.PropertyField(bonusAttackResistanceProp);

        EditorGUILayout.PropertyField(bonusSpeedWalkProp);
        EditorGUILayout.PropertyField(bonusSpeedAttackProp);

        EditorGUILayout.PropertyField(bonusMaxEnergyUniqSkillProp);
        EditorGUILayout.PropertyField(bonusEnergyRegenRateProp);

        EditorGUILayout.PropertyField(bonusEffectHitRateProp);
        EditorGUILayout.PropertyField(bonusEffectResistanceProp);

        EditorGUILayout.PropertyField(bonusCriticalRateProp);
        EditorGUILayout.PropertyField(bonusCriticalDamageProp);
        EditorGUILayout.PropertyField(bonusCriticalResistanceProp);
    }
}
