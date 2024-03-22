using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerButton : MonoBehaviour
{
    public PoolCharacter character;

    public string ID;

    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform parentObject;

    [SerializeField] Image imageWarrior;
    [SerializeField] Image imageWarriorCoolDown;

    [SerializeField] TextMeshProUGUI costManaText;
    private int costMana;
    private float coolDownTime;
    bool isCooldown;

    private Button spawnButton;
    private Dictionary<string, GameObject> characterPrefabMap;
    private Dictionary<string, CharacterStat> characterStatMap;

    private void Start()
    {
        spawnButton = GetComponent<Button>();
        imageWarriorCoolDown.fillAmount = 0f;
        SetGame();
    }
    private void Update()
    {
        if(costMana > GameManager.Instance.Mana)
            spawnButton.interactable = false;
        else spawnButton.interactable = true;
    }
    private void SetGame()
    {
        if (ID != null && ID != "")
        {
            InitializeCharacterPrefabMap();
            InitializeCharacterStatMap();

            spawnButton.onClick.AddListener(() => Spawn(ID));

            SetWarriorImage(ID);
            SetWarriorCost(ID);
            SetWarriorCoolDownTime(ID);
        }else
        {
            gameObject.SetActive(false);
        }
    }
    public void Init(string id)
    {
        ID = id;
    }


    private void InitializeCharacterPrefabMap()
    {
        characterPrefabMap = new Dictionary<string, GameObject>();
        foreach (CharacterStat characterStat in character.Characters)
        {
            characterPrefabMap.Add(characterStat.IDCharacter, characterStat.CharacterPrefab);
        }
    }
    private void InitializeCharacterStatMap()
    {
        characterStatMap = new Dictionary<string, CharacterStat>();
        foreach (CharacterStat characterStat in character.Characters)
        {
            characterStatMap.Add(characterStat.IDCharacter, characterStat);
        }
    }

    private void SetWarriorImage(string characterID)
    {
        if (characterStatMap.ContainsKey(characterID))
        {
            CharacterStat characterStat = characterStatMap[characterID];
            if (characterStat != null)
            {
                imageWarrior.sprite = characterStat.ImageCharacter;
            }
            else
            {
                Debug.LogWarning("CharacterStat not found for ID: " + characterID);
            }
        }
        else
        {
            Debug.LogWarning("Character ID not found: " + characterID);
        }
    }
    private void SetWarriorCost(string characterID)
    {
        if (characterStatMap.ContainsKey(characterID))
        {
            CharacterStat characterStat = characterStatMap[characterID];
            if (characterStat != null)
            {
                costMana = characterStat.Cost;
                costManaText.text = costMana.ToString();
            }
            else
            {
                Debug.LogWarning("CharacterStat not found for ID: " + characterID);
            }
        }
        else
        {
            Debug.LogWarning("Character ID not found: " + characterID);
        }
    }
    private void SetWarriorCoolDownTime(string characterID)
    {
        if (characterStatMap.ContainsKey(characterID))
        {
            CharacterStat characterStat = characterStatMap[characterID];
            if (characterStat != null)
            {
                coolDownTime = characterStat.CoolDownTime;
            }
            else
            {
                Debug.LogWarning("CharacterStat not found for ID: " + characterID);
            }
        }
        else
        {
            Debug.LogWarning("Character ID not found: " + characterID);
        }
    }
    public void Spawn(string characterID)
    {
        if (isCooldown)
            return;

        GameManager.Instance.Mana -= costMana;

        if (characterPrefabMap.ContainsKey(characterID))
        {
            CharacterStat characterStat = characterStatMap[characterID];

            if (characterStat != null)
            {
                GameObject characterPrefab = characterPrefabMap[characterID];
                GameObject characterInstance = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
                characterInstance.transform.SetParent(parentObject);

                if(characterID == "CHA10" || characterID == "CHA10")
                {
                    characterInstance.transform.localEulerAngles = new Vector2(0,180);
                }
                StartCoroutine(StartCoolDown(characterStat.CoolDownTime));
            }
            else
            {
                Debug.LogWarning("CharacterStat not found for ID: " + characterID);
            }
        }
        else
        {
            Debug.LogWarning("Character ID not found: " + characterID);
        }
    }

    private IEnumerator StartCoolDown(float coolDownTime)
    {
        float timer = coolDownTime; 
        imageWarriorCoolDown.fillAmount = 1f; 

        while (timer > 0f)
        {
            isCooldown = true;
            yield return null; 
            timer -= Time.deltaTime; 
            imageWarriorCoolDown.fillAmount = timer / coolDownTime; 
        }

        isCooldown = false;
        imageWarriorCoolDown.fillAmount = 0f;
    }

}
